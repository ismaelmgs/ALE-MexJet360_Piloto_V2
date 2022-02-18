using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using DevExpress.Web.Data;
using DevExpress.Web;
using ALE_MexJet.Clases;


namespace ALE_MexJet.Presenter
{
    public class ReporteTabular_Presenter : BasePresenter<IViewReporteTabular>
    {
        private readonly DBReporteTabular oIGestCat;
        public ReporteTabular_Presenter(IViewReporteTabular oView, DBReporteTabular oGC)
            : base(oView)
        {
            oIGestCat = oGC;

            oIView.eGeneraRep += eGeneraRep_Presente;
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.dtMatricula = oIGestCat.dtMatricula;
            oIView.dtCliente = oIGestCat.dtCliente;
            oIView.dtContrato = oIGestCat.dtContrato;
            oIView.dtGrupoModelo = oIGestCat.dtGrupoModelo;
            oIView.dtBase = oIGestCat.dtBases;
            oIView.sTiposFactura = oIGestCat.DBGetTiposFactura;
        }

        protected void eGeneraRep_Presente(object sender, EventArgs e)
        {
            oIView.LoadGrid(oIGestCat.DBGetConsultaReporteTabulado(oIView.oArrFiltros));
        }

    }
}