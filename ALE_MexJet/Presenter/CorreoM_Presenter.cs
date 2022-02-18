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
    public class CorreoM_Presenter : BasePresenter<IVIewCorreoM>
    {
        private readonly DBCorreoM oIGestCat;

        public CorreoM_Presenter(IVIewCorreoM oView, DBCorreoM oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oView.eSearchObj += GetContactos_Presenter;
            oView.eSaveObj += GuardaCorreo_Presenter;
            oView.eSearchCorreo += GetCorreo_Presenter;
        }
        public void GetContactos_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.LoadContactos(oIGestCat.DBGetContacto());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GuardaCorreo_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBSave(oIView.oCorreo);
                if (id > 0)
                {
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        public void GetCorreo_Presenter(object sender, EventArgs e)
        {
            try
            {
                int z = sender.S().I();
                oIView.LoadCorreo(oIGestCat.DBGetCorreo(z));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}