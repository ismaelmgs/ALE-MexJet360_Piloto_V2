using ALE_MexJet.Objetos;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewAudit : IBaseView
    {
        void LoadUsuarioAudit(DataSet dtObjCat);

        void LoadActividadUsuario(DataTable dtObj);
        
        void LoadUsuario(DataTable dtObj);
        void LoadModulos(DataTable dtObj);

        void LoadReporteadorContrato(DataTable dtObj);
        void LoadReporteadorRemisiones(DataTable dtObj);
        void LoadReporteadorSolicitudVuelo(DataTable dtObj);
        void LoadReporteadorBitacora(DataTable dtObj);
        void LoadReporteadorPrefactura(DataTable dtObj);
        void LoadReporteadorComisariato(DataTable dtObj);


        void LoadIndicadoresFinanzas(DataSet dsIndicadorFinanzas);
        void LoadIndicadoresFinanzasVuelosSinBitacora(DataTable dsIndicadorFinanzas);
        void LoadIndicadoresFinanzasVencimientoDeContratos(DataTable dsIndicadorFinanzas);
        void LoadIndicadoresFinanzasBitacorasSinRemisionar(DataTable dsIndicadorFinanzas);
        void LoadIndicadoresFinanzasRemisionesSinPrefacturar(DataTable dsIndicadorFinanzas);
        void LoadIndicadoresFinanzasPrefacturasSinFacturar(DataTable dsIndicadorFinanzas);
        void LoadFinanzasDescuentos(DataTable dtIndicadorFinanzas);

        void LoadEstatusContratoDetalle(DataTable dsEstatusContratoDetalle);        
        void LoadIndicadoresOperaciones(DataSet dsIndicadorOperaciones);

        void LoadIndicadoresOperacionIntercambios(DataTable dtIndicadorOperacion);        
        void LoadIndicadoresOperacionRentaAeronaves(DataTable dtIndicadorOperacion);
        void LoadIndicadoresOperacionVuelosRentas(DataTable dtIndicadorOperacion);        


        object[] oArrFiltrosActividadUsuario { get; }
        object[] oArrFiltrosIndicadorOperaciones { get; }
        object[] oArrFiltrosSearchReporteador { get; }

        object[] oArrFiltrosSearchFinanza { get; }
        object[] oArrFiltrosSearchFinanzaDescuentos { get; }

        object[] oArrFiltrosSearchEstatusContrato { get; }

        object[] oArrFiltrosSearchIndicadoresOperacionDetalles { get; }

        event EventHandler eGetSearchModulos;
        event EventHandler eGetSearchUsuario;
        event EventHandler eGetSearchActividadUsuario;        
        event EventHandler eGetSearchIndicadorFinanzas;
        event EventHandler eGetSearchIndicadorOperaciones;

        event EventHandler eGetSearchReporteadorContrato;
        event EventHandler eGetSearchReporteadorRemisiones;
        event EventHandler eGetSearchReporteadorSolicitudVuelo;
        event EventHandler eGetSearchReporteadorBitacora;
        event EventHandler eGetSearchReporteadorPrefactura;
        event EventHandler eGetSearchReporteadorComisariato;

        event EventHandler eGetSearchIndicadoresFinanzasVuelosSinBitacora;
        event EventHandler eGetSearchIndicadoresFinanzasVencimientoDeContratos;
        event EventHandler eGetSearchIndicadoresFinanzasBitacorasSinRemisionar;
        event EventHandler eGetSearchIndicadoresFinanzasRemisionesSinPrefacturar;
        event EventHandler eGetSearchIndicadoresFinanzasPrefacturasSinFacturar;

        event EventHandler eGetSearchFinanzasDescuentos;
        event EventHandler eGetSearchEstatusContratoDetalle;

        event EventHandler eGetSearchIndicadoresOperacionIntercambios;
        event EventHandler eGetSearchIndicadoresOperacionRentaAeronaves;
        event EventHandler eGetSearchIndicadoresOperacionVuelosRentas;
    }
}