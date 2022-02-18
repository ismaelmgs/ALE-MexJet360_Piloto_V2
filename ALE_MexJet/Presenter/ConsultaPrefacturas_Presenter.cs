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
    public class ConsultaPrefacturas_Presenter : BasePresenter<iViewConsultaPrefactura>
    {
        private readonly DBConsultaPrefactura oIGestCat;

        public ConsultaPrefacturas_Presenter(iViewConsultaPrefactura oView, DBConsultaPrefactura oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eSearchContratosCliente += GetContratoCliente_Presenter;
            oIView.eGetPrefacturas += GetPrefacturas;
            oIView.eValidaFacturasCanceladas += ValidaPrefacturas;
            oIView.eCancelaPrefactura += CancelaPrefactura;
        }

        private void GetContratoCliente_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.dtContratosCliente = oIGestCat.DBGetContratosCliente(oIView.iIdCliente);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.dtClientes = oIGestCat.dtClientes;   
            }
            catch(Exception ex)
            { 
                throw ex;
            }
        }
        protected void GetPrefacturas(object sender, EventArgs e)
        {
            try 
            {
                oIView.dtPrefactura = oIGestCat.DBSearchObj(oIView.oArrFiltros);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        protected void ValidaPrefacturas(object sender, EventArgs e)
        {
            try
            {
                string sIdFact = oIView.iIdPrefactura.S();
                if (!string.IsNullOrEmpty(oIView.sFacturaScc))
                {

                    oIView.bFacturaSccCancelda = oIGestCat.ValidaEstadoFacturaCancelacion(sIdFact + "2");
                    //oIView.bFacturaSccCancelda = oIGestCat.ValidaEstadoFacturaCancelacion(oIView.sFacturaScc);
                }
                else
                {
                    oIView.bFacturaSccCancelda = true;
                }

                if (!string.IsNullOrEmpty(oIView.sFacturaVuelo))
                {
                    oIView.bFacturaVueloCancelada = oIGestCat.ValidaEstadoFacturaCancelacion(sIdFact + "1");
                    //oIView.bFacturaVueloCancelada = oIGestCat.ValidaEstadoFacturaCancelacion(oIView.sFacturaVuelo);
                }
                else
                {
                    oIView.bFacturaVueloCancelada = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void CancelaPrefactura(object sender, EventArgs e)
        {
            try
            {
                oIGestCat.CancelaPrefactura(oIView.iIdPrefactura);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}