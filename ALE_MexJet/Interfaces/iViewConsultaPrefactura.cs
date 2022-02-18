using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using ALE_MexJet.Presenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;

using DevExpress.Export;
using DevExpress.XtraPrinting;
using System.ComponentModel;
using ALE_MexJet.ControlesUsuario;
using DevExpress.Web.Data;
using DevExpress.Web;
using ALE_MexJet.Clases;
using DevExpress.Utils;
using System.Reflection;
using System.Collections.Specialized;
using System.Text;


namespace ALE_MexJet.Interfaces
{
    public interface iViewConsultaPrefactura : IBaseView
    {
        object[] oArrFiltros { get; }
        int iIdCliente { get; }
        int iIdContrato { get; }
        int iIdPrefactura { get; set; }

        DataTable dtClientes { get; set; }
        DataTable dtContratosCliente { get; set; }
        DataTable dtPrefactura { get; set; }
        bool bFacturaSccCancelda { get; set; }
        bool bFacturaVueloCancelada { get; set; }
        string sFacturaVuelo { get; set; }
        string sFacturaScc { get; set; }

        event EventHandler eValidaFacturasCanceladas;
        event EventHandler eSearchContratosCliente;
        event EventHandler eGetPrefacturas;
        event EventHandler eCancelaPrefactura;


    }
}
