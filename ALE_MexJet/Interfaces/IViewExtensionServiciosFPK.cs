using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewExtensionServiciosFPK : IBaseView
    {
        string sLicenciaPiloto { get; set; }

        event EventHandler eObtieneLicenciaPiloto;

    }
}
