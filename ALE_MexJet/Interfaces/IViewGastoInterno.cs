using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALE_MexJet.Objetos;

namespace ALE_MexJet.Interfaces
{
    public interface IViewGastoInterno:IBaseView
    {                                
        void LoadTipoMoneda(DataTable dtObjCat);
        void LoadObjectsConcepto(DataTable dtObjCat);
        void LoadClientes(DataTable dtObjCat);
        void LoadContrato(DataTable dtObjCat);
        void LoadDetalleGastoInterno(DataTable dtObjCat);
        void LoadMatricula(DataTable dtObjMatricula);
        void LoadTipoFactura(DataTable dtTipoFactura);
        void LoadPaqueteGrupoModelo(DataTable dtPaqueteGrupoModelo);
        

        void ObtieneValores();
        void ObtieneTipoFactura();
        void ObtienePaqueteGrupoModelo();
        void MostrarMensaje(string sMensaje, string sCaption);
        void MostrarMensajeError(string sMensaje, string sCaption);
        
        object[] oArrFiltroConepto { get; }                
        object[] oArrFiltroCliente { get; }                        
        object[] oArrFiltroContrato { get; }               
        object[] oArrFiltroDetalleGastoInterno { get; }        
        object[] oArrFiltroTipoMoneda { get; }                
        object[] oArrFiltroMatricula { get; }
        object[] oArrFiltroPaqueteGrupoModelo { get; }

        object oCrud { get; set; }
        string oContrato { get; }
        string oNombreCliente { get; }
        Enumeraciones.TipoOperacion eCrud { set; get; }

        event EventHandler eGetConcepto;
        event EventHandler eGetCliente;
        event EventHandler eGetContrato;
        event EventHandler eGetDetalleGastoInterno;
        event EventHandler eGetTipoMoneda;
        event EventHandler eGetMatricula;
        event EventHandler eGetTipoFactura;
        event EventHandler eGetPaqueteGrupoModelo;
       
    }
}
