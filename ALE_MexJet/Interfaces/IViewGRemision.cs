using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewGRemision : IBaseView
    {
        int IdCliente { get; }
        int IdContrato { get; }
        long iIdRemision { get; set; }
        Remision oRemision { get; set;}
        DataTable dtTiposPierna { set; get; }
        DataTable dtOrigenDestino { set; get; }
        DataTable dtOrigenDestino2 { set; get; }
        Enumeraciones.TipoOperacion eCrud { set; get; }
        List<BitacoraRemision> oLstBit { get;}
        List<ImportesRemision> oLstImp { get; }
        DataTable dtConceptosR { set; get; }
        DataTable dtServCargo { set; get; }
        ServiciosVueloH oServiciosV { get; }
        List<ServiciosVueloD> oLstSV { get; }
        ServiciosCargoH oServiciosC { get; }
        List<ServiciosCargoD> oLstD { get; }
        RemisionDatosGrals oRemGrals { get; }
        DataTable dtTramosRem { set; get; }
        DataTable dtTramosRemOpc2 { set; get; }
        Enumeraciones.SeCobraFerrys eSeCobraFerrys { set; get; }
        DataSet dsNotas { set; get; }
        string sTipoPierna { set; get; }
        int iNoContrato { set; get; }
        decimal iFactorIVASV { set; get; }
        DatosRemision oDatosFactor { set; get; }
        int iIdPresupuesto { set; get; }

        List<KardexRemision> OLstKardex { get; set; }

        void LoadObjects(DataTable dtObjCat);
        void LoadContracts(DataTable dtCont);
        void RedireccionWizard(int iIndex);
        void MostrarMensaje(string sMensaje, string sCaption);
        void CargaHeaders(string sGrupoModelo);
        void CargaTramos(DataTable dtTramos);
        void LoadHeaders(DataSet ds, DataTable dtTramos, DatosRemision oRem);
        void LoadServiciosV(DataTable dt, DataTable dtSV, DataTable dtSCC);
        void CargaGridDoblePresupuesto(DataTable dtTramos, DataTable dtConceptos2);
        void LlenaModalCaracteristicasContrato(DatosRemision oRem);
        void LlenaNotasYCasos();
        void LoadRemisionesTerminadas(DataSet ds, DatosRemision odRem);


        event EventHandler eGetContracts;
        event EventHandler eGetTiposPierna;
        event EventHandler eSetTramosRem;
        event EventHandler eSaveImportesR;
        event EventHandler eGetServiciosC;
        event EventHandler eSetFinalizaR;
        event EventHandler eGetPasoUno;
        event EventHandler eLoadOrigDestFiltro;
        event EventHandler eSaveImportesOpc2;
        event EventHandler eGetNotasTrip;
        event EventHandler eGetContractsDates;
        event EventHandler eSetTramosCotizacion;
    }
}
