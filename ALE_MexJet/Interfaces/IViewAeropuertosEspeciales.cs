using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewAeropuertosEspeciales : IBaseView
    {
        void LoadAeropuertos(DataTable dt);
        void LoadAeropuertosEspeciales(DataTable dt);
        string sPOA { get; set; }
        int iOK { get; set; }
        int iIdEspecial { set; get; }

        event EventHandler eSearchAeropuertos;
        event EventHandler eSearchAeropuertosEspeciales;
    }
}
