using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;

namespace ALE_MexJet.Presenter
{
    public class ResumenContrato_Presenter : BasePresenter<IViewResumenContrato>
    {
        private readonly DBResumenContrato oIGestCat;

        public ResumenContrato_Presenter(IViewResumenContrato oView, DBResumenContrato oGC)
            : base(oView)
        {
            oIGestCat = oGC;

            oIView.eSearchContracts += SearchContracts_Presenter;
            oIView.eSearchTraspasos += SearchTraspasos_Presenter;
            oIView.eSearchReport += SearchReport_Presenter;
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadClientes(oIGestCat.DBGetObtieneClientesActivosInactivos);
        }

        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            oIView.LoadReport(oIGestCat.DBGetObtieneReporteResumen(oIView.iIdContrato));
        }
        protected void SearchContracts_Presenter(object sender, EventArgs e)
        {
            oIView.LoadContracts(new DBConsultaContrato().DBGetContrato(oIView.iIdCliente));
        }
        protected void SearchTraspasos_Presenter(object sender, EventArgs e)
        {
            oIView.LoadTransferenciasPorPeriodo(oIGestCat.DBGetObtieneTransferenciasPorPeriodo(oIView.iIdResumen));
        }
        protected void SearchReport_Presenter(object sender, EventArgs e)
        {
            oIView.dsResumenPres = oIGestCat.DBGetObtieneResumenContratoRepor(oIView.iIdContrato);
        }
        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            DataTable dtResumen = oIView.ObtieneResumenContrato();

            List<ResumenContrato> oLst = new List<ResumenContrato>();
            foreach(DataRow row in dtResumen.Rows)
            {
                ResumenContrato oRes = new ResumenContrato();
                oRes.iIdResumen = row["IdResumen"].S().I();
                oRes.dAnualidades = row["Anualidades"].S().D();
                oRes.sFacturas = row["NoFactura"].S();

                oLst.Add(oRes);
            }

            oIGestCat.DBSetActualizaAnualidadesFacturas(oLst);
        }
    }
}