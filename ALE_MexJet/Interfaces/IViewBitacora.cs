using ALE_MexJet.Objetos;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewBitacora : IBaseView
    {
        event EventHandler eObjCliente;
        event EventHandler eObjConrato;
        event EventHandler eObjBitacora;
        event EventHandler eObjBitacoraDuplicada;
        event EventHandler eObjBitacoraCobrada;
        event EventHandler eObjBitacoraPorCobrar;
        event EventHandler eObjNumeroBitacorasCobradas;
        event EventHandler eObjNumeroBitacorasPorCobrar;
        event EventHandler eObjNumeroTotalRegistros;
        event EventHandler eObjNumeroTotalDuplicadas;

        void MostrarMensaje(string sMensaje, string sCaption);

        void LoadCliente(DataTable dtObjCat);

        void LoadContrato(DataTable dtObjCat);

        void LoadBitacora(DataTable dtObjCat);

        void LoadBitacoraDuplicada(DataTable dtObjCat);
        void CargaBitacorasDuplicadas();

        void LoadBitacoraCobrada(DataTable dtObjCat);
        void CargaBitacorasCobradas();

        void LoadBitacoraPorCobrar(DataTable dtObjCat);
        void CargaBitacorasPorCobrar();

        void LoadNumeroBitacorasCobradas(DataTable dtObjCat);
        void CargaNumeroBitacorasCobradas();

        void LoadNumeroBitacorasPorCobrar(DataTable dtObjCat);
        void CargaNumeroBitacorasPorCobrar();

        void LoadNumeroTotalRegistros(DataTable dtObjCat);
        void CargaNumeroTotalRegistros();

        void LoadNumeroTotalDuplicadas(DataTable dtObjCat);
        void CargaNumeroTotalDuplicadas();

        int NumeroBitacorasCobradas { get; set; }

        int NumeroBitacorasPorCobrar { get; set; }

        int NumeroTotalRegistros { get; set; }

        int NumeroTotalDuplicadas { get; set; }

        object oCrud { get; set; }
        
        Enumeraciones.TipoOperacion eCrud { set; get; }

        Bitacora oBitacoraContrato { get; }

        Bitacora oBitacoraBitacora { get; }

        Bitacora oBitacoraBitacoraCobrada { get; }

        Bitacora oBitacoraBitacoraNumeroCobrada { get; }

        Bitacora oBitacoraBitacoraPorCobrar { get; }

        Bitacora oBitacoraBitacoraNumeroPorCobrar { get; }

        Bitacora oNumeroTotalRegistros { get; }
    }
}