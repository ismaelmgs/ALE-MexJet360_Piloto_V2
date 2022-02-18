using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using DevExpress.Web.Data;

namespace ALE_MexJet.Presenter
{
    public class Audit_Presenter : BasePresenter<IViewAudit>
    {
        private readonly DBAudit oIGestCat;

        public Audit_Presenter(IViewAudit oView, DBAudit oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eGetSearchActividadUsuario +=oIView_eGetSearchActividadUsuario;
            oIView.eGetSearchUsuario += oIView_eGetSearchUsuario;
            oIView.eGetSearchIndicadorFinanzas += oIView_eGetSearchIndicadorFinanzas;
            oIView.eGetSearchIndicadorOperaciones += oIView_eGetSearchIndicadorOperaciones;
            oIView.eGetSearchModulos += oIView_eGetSearchModulos;

            oIView.eGetSearchReporteadorContrato += oIView_eGetSearchReporteadorContrato;
            oIView.eGetSearchReporteadorRemisiones += oIView_eGetSearchReporteadorRemisiones;
            oIView.eGetSearchReporteadorSolicitudVuelo += oIView_eGetSearchReporteadorSolicitudVuelo;
            oIView.eGetSearchReporteadorBitacora += oIView_eGetSearchReporteadorBitacora;
            oIView.eGetSearchReporteadorPrefactura += oIView_eGetSearchReporteadorPrefactura;
            oIView.eGetSearchReporteadorComisariato += oIView_eGetSearchReporteadorComisariato;

            oIView.eGetSearchIndicadoresFinanzasVuelosSinBitacora += oIView_eGetSearchIndicadoresFinanzasVuelosSinBitacora;
            oIView.eGetSearchIndicadoresFinanzasVencimientoDeContratos += oIView_eGetSearchIndicadoresFinanzasVencimientoDeContratos;
            oIView.eGetSearchIndicadoresFinanzasBitacorasSinRemisionar += oIView_eGetSearchIndicadoresFinanzasBitacorasSinRemisionar;
            oIView.eGetSearchIndicadoresFinanzasRemisionesSinPrefacturar += oIView_eGetSearchIndicadoresFinanzasRemisionesSinPrefacturar;
            oIView.eGetSearchIndicadoresFinanzasPrefacturasSinFacturar += oIView_eGetSearchIndicadoresFinanzasPrefacturasSinFacturar;

            oIView.eGetSearchFinanzasDescuentos += oIView_eGetSearchFinanzasDescuentos;
            oIView.eGetSearchEstatusContratoDetalle += oIView_eGetSearchEstatusContratoDetalle;

            oIView.eGetSearchIndicadoresOperacionIntercambios += oIView_eGetSearchIndicadoresOperacionIntercambios;
            oIView.eGetSearchIndicadoresOperacionRentaAeronaves += oIView_eGetSearchIndicadoresOperacionRentaAeronaves;
            oIView.eGetSearchIndicadoresOperacionVuelosRentas += oIView_eGetSearchIndicadoresOperacionVuelosRentas;                                                            
        }

        

        void oIView_eGetSearchIndicadoresOperacionVuelosRentas(object sender, EventArgs e)
        {
            oIView.LoadIndicadoresOperacionVuelosRentas(oIGestCat.dtIndicadoresOperacionVuelosRentas(oIView.oArrFiltrosSearchIndicadoresOperacionDetalles));
        }

        void oIView_eGetSearchIndicadoresOperacionRentaAeronaves(object sender, EventArgs e)
        {
            oIView.LoadIndicadoresOperacionRentaAeronaves(oIGestCat.dtIndicadoresOperacionRentaAeronaves(oIView.oArrFiltrosSearchIndicadoresOperacionDetalles));
        }

        void oIView_eGetSearchIndicadoresOperacionIntercambios(object sender, EventArgs e)
        {
            oIView.LoadIndicadoresOperacionIntercambios(oIGestCat.dtIndicadoresOperacionIntercambios(oIView.oArrFiltrosSearchIndicadoresOperacionDetalles));            
        }

        void oIView_eGetSearchFinanzasDescuentos(object sender, EventArgs e)
        {
            oIView.LoadFinanzasDescuentos(oIGestCat.dtFinanzasDescuentos(oIView.oArrFiltrosSearchFinanzaDescuentos));
        }

        void oIView_eGetSearchEstatusContratoDetalle(object sender, EventArgs e)
        {
            oIView.LoadEstatusContratoDetalle(oIGestCat.dtEstatusContratoDetalle(oIView.oArrFiltrosSearchEstatusContrato));
        }

        void oIView_eGetSearchIndicadorFinanzas(object sender, EventArgs e)
        {
            oIView.LoadIndicadoresFinanzas(oIGestCat.dsIndicadoresFinanzas(oIView.oArrFiltrosSearchFinanza));
        }

        void oIView_eGetSearchIndicadoresFinanzasPrefacturasSinFacturar(object sender, EventArgs e)
        {
            oIView.LoadIndicadoresFinanzasPrefacturasSinFacturar(oIGestCat.dtIndicadoresFinanzasPrefacturasSinFacturar(oIView.oArrFiltrosSearchFinanza));
        }

        void oIView_eGetSearchIndicadoresFinanzasRemisionesSinPrefacturar(object sender, EventArgs e)
        {
            oIView.LoadIndicadoresFinanzasRemisionesSinPrefacturar(oIGestCat.dtIndicadoresFinanzasRemisionesSinPrefacturar(oIView.oArrFiltrosSearchFinanza));
        }

        void oIView_eGetSearchIndicadoresFinanzasBitacorasSinRemisionar(object sender, EventArgs e)
        {
            oIView.LoadIndicadoresFinanzasBitacorasSinRemisionar(oIGestCat.dtIndicadoresFinanzasBitacorasSinRemisionar(oIView.oArrFiltrosSearchFinanza));
        }

        void oIView_eGetSearchIndicadoresFinanzasVencimientoDeContratos(object sender, EventArgs e)
        {
            oIView.LoadIndicadoresFinanzasVencimientoDeContratos(oIGestCat.dtIndicadoresFinanzasVencimientoDeContratos(oIView.oArrFiltrosSearchFinanza));
        }

        void oIView_eGetSearchIndicadoresFinanzasVuelosSinBitacora(object sender, EventArgs e)
        {
            oIView.LoadIndicadoresFinanzasVuelosSinBitacora(oIGestCat.dtIndicadoresFinanzasVuelosSinBitacora(oIView.oArrFiltrosSearchFinanza));
        }



        void oIView_eGetSearchReporteadorComisariato(object sender, EventArgs e)
        {
            oIView.LoadReporteadorComisariato(oIGestCat.dtReporteadorComisariato(oIView.oArrFiltrosSearchReporteador));
        }

        void oIView_eGetSearchReporteadorPrefactura(object sender, EventArgs e)
        {
            oIView.LoadReporteadorPrefactura(oIGestCat.dtReporteadorPrefactura(oIView.oArrFiltrosSearchReporteador));            
        }

        void oIView_eGetSearchReporteadorBitacora(object sender, EventArgs e)
        {
            oIView.LoadReporteadorBitacora(oIGestCat.dtReporteadorBitacora(oIView.oArrFiltrosSearchReporteador));            
        }

        void oIView_eGetSearchReporteadorSolicitudVuelo(object sender, EventArgs e)
        {
            oIView.LoadReporteadorSolicitudVuelo(oIGestCat.dtReporteadorSolicitudVuelo(oIView.oArrFiltrosSearchReporteador));            
        }

        void oIView_eGetSearchReporteadorRemisiones(object sender, EventArgs e)
        {
            oIView.LoadReporteadorRemisiones(oIGestCat.dtReporteadorRemisiones(oIView.oArrFiltrosSearchReporteador));           
        }

        void oIView_eGetSearchReporteadorContrato(object sender, EventArgs e)
        {
           oIView.LoadReporteadorContrato(oIGestCat.dtReporteadorContrato(oIView.oArrFiltrosSearchReporteador));       
        }

        void oIView_eGetSearchModulos(object sender, EventArgs e)
        {
            oIView.LoadModulos(oIGestCat.dtModulos());
        }

        void oIView_eGetSearchIndicadorOperaciones(object sender, EventArgs e)
        {
            oIView.LoadIndicadoresOperaciones(oIGestCat.dsIndicadoresOperaciones(oIView.oArrFiltrosIndicadorOperaciones));
        }        

        void oIView_eGetSearchUsuario(object sender, EventArgs e)
        {
            oIView.LoadUsuario(oIGestCat.dtUsuarios());
        }

        private void oIView_eGetSearchActividadUsuario(object sender, EventArgs e)
        {
            oIView.LoadActividadUsuario(oIGestCat.dtActividadUsuarios(oIView.oArrFiltrosActividadUsuario));
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadUsuarioAudit(oIGestCat.dtObjUsuariosReport());
        }
    }
}