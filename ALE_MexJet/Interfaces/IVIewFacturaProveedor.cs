using ALE_MexJet.Objetos;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IVIewFacturaProveedor : IBaseView
    {
        object oCrud { get; set; }
        Enumeraciones.TipoOperacion eCrud { set; get; }

        object[] oArrFiltros { get; }
        object[] oArrMatricula { get; }
        object[] oArrDetalle { get; }
        object[] oArrDeleteFac { get; }
        object[] oArrProvED { get; }
        object[] OArrProvDet { get; }

        FacturaProveedor oProveedor { get; }
        void LoadControlesProvED(DataTable dtObjCat);
        void LoadFactura(DataTable dtObjCat);
        void LoadCliente(DataTable dtObjCat);
        void LoadMatricla(DataTable dtObjCat);
        void LoadTipoMoneda(DataTable dtObjCat);
        void LoadBitacora(DataTable dtObjCat);
        void LoadProvDetalle(DataTable dtObjCat);
        void LoadPiernaRent(DataTable dtObjCat);
        void GuardoProveedor(int iObjCat);
        void MostrarMensaje(string sMensaje, string sCaption);

        event EventHandler eSearCliente;
        event EventHandler eSearMatricula;
        event EventHandler eSearTipoMoneda;
        event EventHandler eSearBitacora;
        event EventHandler eSaveProveedor;
        event EventHandler eSaveProveedorDetalle;
        event EventHandler eSearchProvDetalle;
        event EventHandler eEliminaProv;
        event EventHandler eSearProvED;
        event EventHandler eSearPiernaRent;
    }
}