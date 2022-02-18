using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using DevExpress.Web.Data;
using ALE_MexJet.Clases;

namespace ALE_MexJet.Presenter
{
    public class FacturaProveedor_Presenter : BasePresenter<IVIewFacturaProveedor>
    {
        private readonly DBFacturaProveedor oIGestCat;

        public FacturaProveedor_Presenter(IVIewFacturaProveedor oView, DBFacturaProveedor oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eSearchObj += CargaFacturas_Presenter;
            oIView.eSearCliente += CargaCliente_Presenter;
            oIView.eSearMatricula += CargaMatricula_Presenter;
            oIView.eSearTipoMoneda += CargaTipoMoneda_Presenter;
            oIView.eSearBitacora += CargaBitacora_Presenter;
            oIView.eSaveProveedor += InsertaPoveedor_Presenter;
            oIView.eSaveProveedorDetalle += InsertaProveedorDetalle_Presenter;
            oIView.eSearchProvDetalle += CargaFactProvDetalle_Presenter;
            oIView.eSaveObj += ActualizaProveedorDetalle_Presenter;
            oIView.eDeleteObj += EliminaProveedorDetalle_Presenter;
            oIView.eEliminaProv += EliminaFactura_Presenter;
            oIView.eSearProvED += CargaProvHead_Presenter;
            oIView.eSearPiernaRent += CargaProvED_Presenter;
        }

        public void CargaFacturas_Presenter(object sender, EventArgs e)
        {
            oIView.LoadFactura(oIGestCat.dtObjsFacProveedor(oIView.oArrFiltros));
        }
        public void CargaCliente_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCliente(oIGestCat.dtObjCliente());
        }
        public void CargaMatricula_Presenter(object sender, EventArgs e)
        {
            oIView.LoadMatricla(oIGestCat.dtObjMatricula());
        }
        public void CargaTipoMoneda_Presenter(object sender, EventArgs e)
        {
            oIView.LoadTipoMoneda(oIGestCat.dtObjTipoMoneda());
        }
        public void CargaBitacora_Presenter(object sender, EventArgs e)
        {
            oIView.LoadBitacora(oIGestCat.dtObjBitacoraMatricula(oIView.oArrMatricula));
        }
        public void InsertaPoveedor_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBSave(oIView.oProveedor);
                if (id > 0)
                {
                    oIView.GuardoProveedor(id);
                    oIView.LoadPiernaRent(oIGestCat.dtObjsPiernaRentadas());
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        public void InsertaProveedorDetalle_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBSaveFacturaDetalle(oIView.OArrProvDet);
                if (id > 0)
                {
                    //oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        public void CargaFactProvDetalle_Presenter(object sender, EventArgs e)
        {
            oIView.LoadProvDetalle(oIGestCat.dtSearchFacProvDetalle(oIView.oArrDetalle));
        }
        public void ActualizaProveedorDetalle_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBUpdateFacturaDetalle(oFacturaProveedor);
                if (id > 0)
                {
                    //oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        public void EliminaProveedorDetalle_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBEliminaFacturaDetalle(oFacturaProveedor);
                if (id > 0)
                {
                    //oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        public void EliminaFactura_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBEliminaFactura(oIView.oArrDeleteFac);
                if (id > 0)
                {
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Eliminacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        public void CargaProvED_Presenter(object sender, EventArgs e)
        {
            oIView.LoadPiernaRent(oIGestCat.dtObjsPiernaRentadas());
        }

        public void CargaProvHead_Presenter(object sender, EventArgs e)
        {
            oIView.LoadControlesProvED(oIGestCat.dtObjProvED(oIView.oArrDetalle));
        }

        private FacturaProveedor oFacturaProveedor
        {
            get
            {
                FacturaProveedor ofactura = new FacturaProveedor();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;

                        ofactura.IdFaturaProveedor = eI.NewValues["iIdFaturaProveedor"].S().I();
                        ofactura.Matricula = eI.NewValues["Matricula"].S();
                        ofactura.FolioReal = eI.NewValues["FolioReal"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                        ofactura.IdFaturaProveedorDetalle= eU.Keys[0].S().I();
                        ofactura.Matricula = eU.NewValues["Matricula"].S().I() == 0 ? eU.Keys[1].S() : eU.NewValues["Matricula"].S();
                        ofactura.FolioReal = eU.NewValues["FolioReal"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        
                        ofactura.IdFaturaProveedorDetalle = eD.Keys[0].S().I();

                        break;
                }

                return ofactura;
            }
        }
    }
}