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
    public class ConsultaCasosPresenter : BasePresenter <IViewConsultaCasos>
    {
        private readonly DBConsultaCasos oIGestCat;

        public ConsultaCasosPresenter(IViewConsultaCasos oView, DBConsultaCasos oGC)
            : base(oView)
        {
            oIGestCat = oGC;

            oIView.eGetObjects += eGetObjects_Presenter;
            oIView.eObjConsultaCasos += SearchObjConsultaCasos_Presenter;
            oIView.eGetMotivos += eGetMotivos_Presenter;
            oIView.eObjConsultaTop += SearchObjConsultaTop_Presenter;
        }

        protected void eGetObjects_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.LoadCliente(new DBCliente().DBSearchObj("@CodigoCliente", string.Empty,
                                                                "@Nombre", string.Empty,
                                                                "@TipoCliente", string.Empty,
                                                                "@estatus", 1));

                oIView.LoadTipoCaso(oIGestCat.dtGetTipoCasos);
                oIView.LoadArea(oIGestCat.dtGetArea);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void eGetMotivos_Presenter(object sender, EventArgs e)
        {
            
            oIView.LoadMotivo(oIGestCat.dtGetMotivos(oIView.iIdTipoCaso));
        }

        protected void SearchObjConsultaCasos_Presenter(object sender, EventArgs e)
        {
            oIView.LoadConsultaCasos(oIGestCat.dtObjConsultaCasos(oIView.oConsultaCasosConsultaCasos));
        }

        protected void SearchObjConsultaTop_Presenter(object sender, EventArgs e)
        {
            oIView.LoadConsultaTop(oIGestCat.dtObjConsultaCasos(oIView.oConsultaCasosConsultaTop));
        }

        }

}