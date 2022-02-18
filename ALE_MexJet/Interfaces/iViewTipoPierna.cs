﻿using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface iViewTipoPierna : IBaseView
    {
        object oCrud { get; set; }
        DataTable dtPaquetesActivos { get; set; }

        bool bDuplicado { get; set; }
        Enumeraciones.TipoOperacion eCrud { set; get; }

        object[] oArrFiltros { get; }

        void ObtieneValores();
        void LoadObjects(DataTable dtObjCat);
        void MostrarMensaje(string sMensaje, string sCaption);

        event EventHandler eGetPaquetes;
    }
}
