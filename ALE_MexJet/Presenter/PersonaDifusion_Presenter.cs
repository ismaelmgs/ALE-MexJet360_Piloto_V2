using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using DevExpress.Web.Data;
using DevExpress.Web;
using ALE_MexJet.Clases;

namespace ALE_MexJet.Presenter
{
    public class PersonaDifusion_Presenter : BasePresenter<IViewPersonaDifusion>
    {
        private readonly DBPersonaDifusion oIGestCat;

        public PersonaDifusion_Presenter(IViewPersonaDifusion oView, DBPersonaDifusion oGC)
            : base(oView)
        {
            oIGestCat = oGC;

            oView.eDeletePersonaListaDifusion += DeletePersonaListaDifusion_Presenter;
            oView.eObtieneTitulo += ObtieneTitulo_Presenter;
            oView.eObtieneTipoPersona += ObtieneTipoPersona_Presenter;
            oView.eObtieneTipoContacto += ObtieneTipoContacto_Presenter;
            oView.eObtienePersonaListaDifusion += ObtienePersonaListaDifusion_Presenter;
            oView.eObtieneDatosPersona += ObtienePersonaDifusion_Presenter;
            oView.eSavePersonaListaDifusion += DBSaveObjPersonaLista_Presenter;
            oView.eUpdateObj += DBUpdate_Presenter;
            
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.CargarGridPersona(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }
        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBSave(oIView.oPersona);
                if (id > 0)
                {
                    //oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    //oIView.ObtieneValores();
                }
            }
            catch (Exception ex)
            {
                //oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
                throw ex;
            }
        }
        protected override void DeleteObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = (int)sender;

                id = oIGestCat.DBDelete(id);
                
                if (id > 0)
                {
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Eliminacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    oIView.ObtieneValores();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        protected void DBUpdate_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBUpdate(oIView.oPersona);

                //if (id > 0)
                //{
                //    oIView.ObtieneValores();
                //}
                
            }
            catch (Exception ex)
            {
                
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");

            }
        }
        protected void DBSaveObjPersonaLista_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBSavePersonaListaDifusion(oIView.oArrFiltrosPersonaListaDifusion);
                if (id > 0)
                {
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    oIView.ObtieneValores();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        protected void DeletePersonaListaDifusion_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBDeletePersonaListaDifusion(oIView.oArrFiltrosPersonaListaDifusion);
                
                if (id > 0)
                {
                    //oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Eliminacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    //oIView.ObtieneValores();
                }
            }
            catch (Exception ex)
            {
                
               oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        protected void ObtieneTitulo_Presenter(object sender, EventArgs e)
        {
            oIView.CargaTitulo(oIGestCat.DBObtieneTitulos());
        }
        protected void ObtieneTipoPersona_Presenter(object sender, EventArgs e)
        {
            oIView.CargaTipoPersona(oIGestCat.DBObtieneTipoPersona());
        }
        protected void ObtieneTipoContacto_Presenter(object sender, EventArgs e)
        {
            oIView.CargaTipoContacto(oIGestCat.DBObtieneTipoContacto());
        }
        protected void ObtienePersonaDifusion_Presenter(object sender, EventArgs e)
        {
            int iIdPersonaDifusion = (int)sender;

            oIView.oPersona = oIGestCat.DBObtienePersonaDifusion(iIdPersonaDifusion);
        }
        protected void ObtienePersonaListaDifusion_Presenter(object sender, EventArgs e)
        {
            int iIdPersonaDifusion = (int)sender;

            oIView.CargaPersonaListaDifusion(oIGestCat.DBObtienePersonaListaDifusion(iIdPersonaDifusion));
        }

    }
}