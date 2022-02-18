using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ALE_MexJet.Interfaces;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Objetos;
using DevExpress.Web.Data;
using NucleoBase.Core;
using System.Data;
using ALE_MexJet.Clases;
using System.Net;

namespace ALE_MexJet.Presenter
{
    public class GastoInterno_Presenter:BasePresenter<IViewGastoInterno>
    {        
        private readonly DBGastoInterno oIGestCat;

        public GastoInterno_Presenter(IViewGastoInterno oView, DBGastoInterno oGC)
            : base(oView)
        {
            try
            {
                oIGestCat = oGC;
                oIView.eGetCliente += eGetCliente_Presenter;
                oIView.eGetContrato += eGetContrato_Presenter;
                oIView.eGetDetalleGastoInterno += eGetDetalleGastoInterno_Presenter;
                oView.eGetConcepto += eGetConcepto_Presenter;
                oIView.eGetTipoMoneda += eGetTipoMoneda_Presenter;
                oIView.eGetMatricula += oIView_eGetMatricula;
                oIView.eGetTipoFactura += oIView_eGetTipoFactura;
                oIView.eGetPaqueteGrupoModelo += oIView_eGetPaqueteGrupoModelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void oIView_eGetPaqueteGrupoModelo(object sender, EventArgs e)
        {
            oIView.LoadPaqueteGrupoModelo(oIGestCat.DBSearchPaqueteGrupomodelo(oIView.oArrFiltroPaqueteGrupoModelo));
        }

        void oIView_eGetTipoFactura(object sender, EventArgs e)
        {
            oIView.LoadTipoFactura(oIGestCat.DBSearchTipoFactura());
        }

        void oIView_eGetMatricula(object sender, EventArgs e)
        {
            try
            {
                oIView.LoadMatricula(oIGestCat.DBSearchMatriculaAeronave(oIView.oArrFiltroMatricula));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void eGetCliente_Presenter(object sender, EventArgs e)
        {
            oIView.LoadClientes(oIGestCat.DBSearchCliente(oIView.oArrFiltroCliente));
        }
        private void eGetContrato_Presenter(object sender, EventArgs e)
        {
            oIView.LoadContrato(oIGestCat.DBSearchContrato(oIView.oArrFiltroContrato));
        }
        private void eGetDetalleGastoInterno_Presenter(object sender, EventArgs e)
        {
            oIView.LoadDetalleGastoInterno(oIGestCat.DBSearchDetalleGastoInterno(oIView.oArrFiltroDetalleGastoInterno));
        }
        protected void eGetConcepto_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjectsConcepto(oIGestCat.DBSearchObjConcepto(oIView.oArrFiltroConepto));
        }
        private void eGetTipoMoneda_Presenter(object sender, EventArgs e)
        {
            oIView.LoadTipoMoneda(oIGestCat.DBSearchTipoMoneda(oIView.oArrFiltroTipoMoneda));
        }

        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            oIView.eCrud = Enumeraciones.TipoOperacion.Validar;
            int id = oIGestCat.DBValidaImporteGastoInterno(oGastoInterno);

            if (id > 0)            
                iValorInsertado = id;            
            else if (id == -1)                            
                iValorInsertado = id;            
            else if (id == -2)                         
                iValorInsertado = id;            
        }
           
        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            try 
            {                
                GastoInterno g = oGastoInterno;

                int id = oIGestCat.DBInsertaGastoInterno(g);
                if (id > 0)
                {                    
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    oIView.ObtieneValores();
                    iValorInsertado = id;
                }
                else if (id == -1)                                    
                    iValorInsertado = id;                
                else if (id == -2)                                    
                    iValorInsertado = id;                
            }
            catch(Exception ex) 
            {
                throw ex;                
            }
        }
        protected override void DeleteObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                GastoInterno gi = oGastoInterno;
                int id = oIGestCat.DBEliminaGastoInterno(gi);
                if (id > 0)
                {                    
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Eliminacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    oIView.ObtieneValores();
                }
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
        public static int iValorInsertado;
        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                GastoInterno g = oGastoInterno;

                    int id = oIGestCat.DBActualizaGastoInterno(g);
                    if (id > 0)
                    {
                        oIView.ObtieneValores();                     
                        oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Modificacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);                     
                    }                   
            }
            catch (Exception ex)
            {               
                throw ex;
            }
        }
        private GastoInterno oGastoInterno
        {            
            get 
            {                
                GastoInterno oGasto = new GastoInterno();                         

                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;                                                
                        oGasto.sClaveContrato = oIView.oContrato;
                        oGasto.sNombreCliente = oIView.oNombreCliente;                       
                        oGasto.iIdTipoMovimiento = eI.NewValues["IdTipoMovimiento"].I();
                        oGasto.sDescripcionConcepto = eI.NewValues["GastoInternoConcepto"].S();
                        oGasto.iIdAeronaveMatricula = eI.NewValues["IdMatricula"].I();
                        oGasto.iIdTipoMoneda = eI.NewValues["IdTipoMoneda"].I();
                        oGasto.dImporte = eI.NewValues["GastoInternoImporte"].D();
                        oGasto.dIVA = eI.NewValues["IVA"].D();
                        oGasto.dTotal = eI.NewValues["Total"].D();
                        oGasto.dtFechaGasto = eI.NewValues["FechaGasto"].Dt();
                        oGasto.sIP = obtieneIP();                            
                        break;

                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;                       
                        oGasto.iIdGastoInterno = eU.Keys[0].S().I();
                        oGasto.iIdTipoMovimiento = eU.NewValues["IdTipoMovimiento"].I();
                        oGasto.sDescripcionConcepto = eU.NewValues["GastoInternoConcepto"].S();
                        oGasto.iIdAeronaveMatricula = eU.NewValues["IdMatricula"].I();
                        oGasto.iIdTipoMoneda = eU.NewValues["IdTipoMoneda"].I();
                        oGasto.dImporte = eU.NewValues["GastoInternoImporte"].D();
                        oGasto.dIVA = eU.NewValues["IVA"].D();
                        oGasto.dTotal = eU.NewValues["Total"].D();
                        oGasto.dtFechaGasto = eU.NewValues["FechaGasto"].Dt();
                        oGasto.sIP = obtieneIP();
                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;
                        oGasto.sClaveContrato = oIView.oContrato;
                        oGasto.sNombreCliente = oIView.oNombreCliente;
                        oGasto.dImporte = eV.NewValues["GastoInternoImporte"].D();
                        break;

                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;                        
                        oGasto.iIdGastoInterno = eD.Keys[0].S().I();                       
                        break;
                }

                return oGasto;
            }
        }

        private string obtieneIP()
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }
    }    
}





