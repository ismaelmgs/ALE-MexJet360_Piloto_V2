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
    public class Contacto_Presenter : BasePresenter<IViewContacto>
    {
        private readonly DBContacto oIGestCont;

        public Contacto_Presenter(IViewContacto oView, DBContacto oGC)
            :base(oView)
        {
            oIGestCont = oGC;
            oView.eGetTipoTitulo += eGetTipoTitulo_Presenter;
            oIView.eUpdateObservacion += eUpdateObservacion;
        }

        public void LoadObjects_Presenter()
        {
            oIView.LoadObjects(oIGestCont.DBSearchObj(oIView.iIdCliente));
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjects(oIGestCont.DBSearchObj(oIView.iIdCliente));
        }
        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCont.BDUpdate(oCatalogo);
                if (id > 0)
                {
                    oIView.ObtieneValores();
                    oIView.MuestraMensg(Mensajes.LMensajes[Enumeraciones.Mensajes.Modificacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);

                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCont.DBSave(oCatalogo);
                if (id > 0)
                {
                    oIView.MuestraMensg(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    oIView.ObtieneValores();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        protected override void DeleteObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCont.DBDelete(oCatalogo);
                if (id > 0)
                {
                    oIView.MuestraMensg(Mensajes.LMensajes[Enumeraciones.Mensajes.Eliminacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    oIView.ObtieneValores();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        protected void eGetTipoTitulo_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCatalogoTipocliente(oIGestCont.dtTipoTitulo);
        }

        protected void eUpdateObservacion(object sender, EventArgs e)
        {
            try 
            {
                int id = oIGestCont.DBUpdateComentarios(oIView.oCliente);
                if (id > 0)
                {
                    oIView.MostrarMensaje("Se guardo la información.",Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);

                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        private Contacto oCatalogo
        {
            get
            {
                Contacto oContacto = new Contacto();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
//                        oContacto.iIdContacto =;
                        oContacto.iIdCliente = oIView.iIdCliente;
                        oContacto.sNombre = eI.NewValues["Nombre"].S();
                        oContacto.iIdTitulo = eI.NewValues["IdTitulo"].S().I();
                        oContacto.sTelOficina = eI.NewValues["TelOficina"].S();
                        oContacto.sTelMovil = eI.NewValues["TelMovil"].S();
                        oContacto.sOtroTel = eI.NewValues["OtroTel"].S();
                        oContacto.sCorreoElectronico = eI.NewValues["CorreoElectronico"].S();


                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;


                        oContacto.iIdContacto = eU.Keys[0].S().I();
                        oContacto.iIdCliente = oIView.iIdCliente;
                        oContacto.sNombre = eU.NewValues["Nombre"].S();
                        oContacto.iIdTitulo = eU.NewValues["IdTitulo"].S().I();
                        oContacto.sTelOficina = eU.NewValues["TelOficina"].S();
                        oContacto.sTelMovil = eU.NewValues["TelMovil"].S();
                        oContacto.sOtroTel = eU.NewValues["OtroTel"].S();
                        oContacto.sCorreoElectronico = eU.NewValues["CorreoElectronico"].S();
                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oContacto.iIdContacto = eD.Keys[0].S().I();
                        break;
                }

                return oContacto;
            }
        }

      
    }
}
