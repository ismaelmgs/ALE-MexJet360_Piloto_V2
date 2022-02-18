using ALE_MexJet.Objetos;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewConsultaVuelos : IBaseView
    {
        void LoadVuelos(DataTable dt);
        void LoadParametros(DataTable dt);
        string sFecha { get; set; }

        event EventHandler eSearchVuelos;
        event EventHandler eSearchParametros;
    }
}
