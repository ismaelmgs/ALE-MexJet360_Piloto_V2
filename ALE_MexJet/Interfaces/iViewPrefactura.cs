using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ALE_MexJet.Objetos;

namespace ALE_MexJet.Interfaces
{
    public interface iViewPrefactura : IBaseView
    {

        DataTable dtClientes { get; set; }
        DataTable dtFacturantesSAP { set; get; }
        DataTable dtFacturanteSV { get; set; }
        DataTable dtFacturanteSC { get; set; }
        DataTable dtContrato { get; set; }
        DataTable dtRemisiones { get; set; }
        DataTable dtDetalleRemisiones { get; set; }


        int iIdCliente{get;}
        int iIdContrato { get; }
        int iIdMonedaSC{get;}
        int iIdMonedaSV { get; }
        Prefactura objPrefactura { get; set; }
        string sClaveContrato { get;}
        string sClaveCliente { get; }
        string sRemisiones{ get; }
        DataTable dtSC { get; set; }
        DataTable dtSV { get; set; }
        int iIdPrefactura { get; set; }
        Decimal TipoCambioPrefactura { get; set; }

        event EventHandler eGetFacturanteSV;
        event EventHandler eGetFacturanteSC;
        event EventHandler eGetContratos;
        event EventHandler eGetRemisiones;
        event EventHandler eSaveBasePrefactura;
        event EventHandler eSaveServicos;
        event EventHandler eGetDetalle;
        event EventHandler eGetServiciosVuelo;
        event EventHandler eGetServiciosCargo;
        event EventHandler eUpdateBasePrefactura;
        event EventHandler eGetPrefacturas;
        event EventHandler eGetRecuperaRemisionesPrefactura;
        event EventHandler eGetRecuperaPRefacturaServicios;
        event EventHandler eGetInformacionFactura;
        event EventHandler eValidaFacturante;
        event EventHandler eGeneraFacturaVuelo;
        event EventHandler eGeneraFacturaSCC;
        event EventHandler eGeneraUnaFactura;
        event EventHandler eVerificaPaquete;
        event EventHandler eGetFacturantesSAP;

        string sIdFacturaSCC{ get; set; }

        string sIdFacturaVuelo { get; set; }
        bool bExisteFacturanteVuelo { get; set; }
        bool bExisteFacturanteSCC { get; set; }

        DataSet dsInformacionContrato{get;set;}

        bool bActualizaGeneral {get;set;}
        bool bActualizaSeleccion{get;set;}
        bool bActualizaFinal { get; set; }
        bool bUnaFactura { get; set; }
        string sFacturaVuelo { get; set; }
        string sFacturaSCC { get; set; }
        decimal dFactorIVAV { get; set; }
    }
}
