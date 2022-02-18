using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class BaseObjeto
    {
        private ALE_MexJet.Objetos.Genericos.ErrorController _oErr = new ALE_MexJet.Objetos.Genericos.ErrorController();
        private bool bDisposed = false;

        ~BaseObjeto()
        {
            Dispose(false);
        }

        [Browsable(false)]
        public ALE_MexJet.Objetos.Genericos.ErrorController oErr { get { return _oErr; } set { _oErr = value; } }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool bDisposing)
        {
            try
            {
                if (!bDisposed)
                {
                    if (bDisposing)
                    {

                    }
                    bDisposed = true;
                }
            }
            catch
            {
            }
        }
    }
}