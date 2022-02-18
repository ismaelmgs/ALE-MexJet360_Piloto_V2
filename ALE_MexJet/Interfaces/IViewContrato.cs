using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
   public interface IViewContrato : IBaseView
    {
        bool bActualizaGenerales { set; get; }
        bool bActualizaTarifa { set; get; }
        bool bActualizaCobro { set; get; }
        bool bActualizaIntercambio { set; get; }
        bool bActualizaGiras { set; get; }
        bool bActualizaRangos { set; get; }
        bool bActualizaCaracteristicas { set; get; }
        bool bActualizaPreferencias { set; get; }

        int iIdContrato { set; get; }

        object oCrud { get; set; }

        bool bDuplicado { get; set; }
        Enumeraciones.TipoOperacion eCrud { set; get; }

        object[] oArrFiltros { get; }
        
        void ObtieneValores();
        void LoadObjects(DataTable dtObjCat);
        void MostrarMensaje(string sMensaje, string sCaption);
        void HabilitaPermisosContrato(DataTable dt);
        

        event EventHandler eGetContratoEdicion;

        event EventHandler eGetServicioConCargo;
        event EventHandler eGetEjecutivo;
        event EventHandler eGetVendedor;
        event EventHandler eGetPaquete;
        event EventHandler eGetGrupoModelo;
        event EventHandler eGetBase;
        
        event EventHandler eGetAeropuertoOrigen;
        event EventHandler eGetAeropuertoDestino;
        event EventHandler eGetAeropuertoOrigenFiltrado;
        event EventHandler eGetAeropuertoDestinoFiltrado;
        event EventHandler eValidaContrato;

        event EventHandler eSaveGenerales;
        event EventHandler eUpdateGenerales;

        event EventHandler eSaveBases;
        event EventHandler eUpdateBases;

        event EventHandler eSearchCombustibleInternacional;
        event EventHandler eSaveCombustibleInternacional;
        event EventHandler eUpdateCombustibleInternacional;
        event EventHandler eDeleteCombustibleInternacional;
        event EventHandler eValidaCombustible;


        event EventHandler eSaveTarifas;
        event EventHandler eSaveTarifa;
        event EventHandler eUpdateTarifas;

        event EventHandler eSearchTramo;
        event EventHandler eSaveTramo;
        event EventHandler eUpdateTramo;
        event EventHandler eDeleteTramo;
        event EventHandler eValidaTramo;


        event EventHandler eSaveCobros;
        event EventHandler eUpdateCobros;


        event EventHandler eSearchIntercambio;
        event EventHandler eSaveIntercambio;
        event EventHandler eUpdateIntercambio;
        event EventHandler eDeleteIntercambio;
        event EventHandler eValidaIntercambio;


        event EventHandler eSaveGiras;
        event EventHandler eUpdateGiras;

        event EventHandler eSearchRangos;
        event EventHandler eSaveRangos;
        event EventHandler eUpdateRangos;
        event EventHandler eDeleteRangos;
        event EventHandler eValidaRangos;

        

        event EventHandler eSaveCaracteristicas;
        event EventHandler eUpdateCaracteristicas;
        event EventHandler eSearchModelos;

        event EventHandler eSavePreferencias;
        event EventHandler eUpdatePreferencias;
        event EventHandler eGetDatosFacturacion;

        event EventHandler eGetPermisosContrato;
        



        int iIdMarca { get; set; }

        object oCrudTarifa { get; set; }
        object oCrudTamoPactado { get; set; }
        object oCrudIntercambio { get; set; }
        object oCrudRangos { get; set; }
        object oCrudCombustible { get; set; }

        object oCrudBases { get; set; }

        bool bDuplicaClaveContrato { get; set; }
        bool bDuplicaCombustibleInternacional { get; set; }
        bool bDuplicaTramo { get; set; }
        bool bDuplicaIntercambio { get; set; }
       
        DataTable dtTarifaCombustible { get; set; }
        DataTable dtTramosPactados { get; set; }
        DataTable dtCliente { get; set; }
        DataTable dtEjecutivo { get; set; }
        DataTable dtBasesSeleccionadas { get; set; }
        DataTable dtVendedor { get; set; }
        DataTable dtServicioConCargo { get; set; }
        DataTable dtPaquete { get; set; }
        DataTable dtGrupoModelo { get; set; }
        DataTable dtBase { get; set; }
        DataTable dtInflacion { get; set; }
        DataTable dtIntercambios { get; set; }
        DataTable dtEstatusContratos { get; set; }
        DataSet dsCatFacturacion { set; get; }

       
        Contrato_Generales objContratosGenerales { get; set; }
        Contrato_Tarifas objContratosTarifas { get; set; }

        Contrato_CobrosDescuentos objCobrosDesc { get; set; }
        Contrato_GirasFechasPico objGirasFechas { get; set; }
        Contrato_CaracteristicasEspeciales objCaracteristicasEspeciales { get; set; }
        Contrato_Preferencias objPreferencias { get; set; }

        //Enumeraciones.TipoOperacion eCrud { set; get; }

        DataTable dtTarifaOrigen { get; set; }
        DataTable dtTarifaDestino { get; set; }

        DataTable dtMarca { get; set; }
        DataTable dtModelo { get; set; }

        DataTable dtRangos { get; set; }
        bool bduplicaRango { get; set; }

        string sOrigen { get; set; }
        string sFiltroAeropuerto { get; set; }

        void LlenaControlesFacturacion();
    }
}

