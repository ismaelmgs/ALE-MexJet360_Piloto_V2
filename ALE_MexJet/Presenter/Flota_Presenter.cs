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
    public class Flota_Presenter : BasePresenter<IViewFlota>
    {
        private readonly DBFlota oIGestCat;

        public Flota_Presenter(IViewFlota oView, DBFlota oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eGetCodigoUnidadDosUnion += oIView_eGetCodigoUnidadDos;
        }

        void oIView_eGetCodigoUnidadDos(object sender, EventArgs e)
        {
            oIView.LoadCodigoUnidadDos_Union(oIGestCat.DBGetCodUnidad2());
        }

        public void LoadObjects_Presenter()
        {
            oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }

        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                Flota oFlota = oCatalogo;
                int id = oIGestCat.DBUpdate(oFlota);
                if (id > 0)
                {
                    oIView.ObtieneValores();
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Modificacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje); 	

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
                Flota oFlota = oCatalogo;
                int id = oIGestCat.DBSave(oFlota);
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

        protected override void DeleteObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                Flota oFlota = oCatalogo;
                int id = oIGestCat.DBDelete(oFlota);
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

        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            int existe = 0;
            if (oIView.eCrud == Enumeraciones.TipoOperacion.Actualizar)
            {
                if (bValidaActualizacion)
                {
                    oIView.eCrud = Enumeraciones.TipoOperacion.Validar;
                    existe = oIGestCat.DBValida(oCatalogo);
                }

            }
            else
            {
                oIView.eCrud = Enumeraciones.TipoOperacion.Validar;
                existe = oIGestCat.DBValida(oCatalogo);
            }
            oIView.bDuplicado = existe > 0;
        }

        private Flota oCatalogo
        {
            get
            {
                Flota oFlota = new Flota();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oFlota.iId = 0;
                        oFlota.sDescripcion = eI.NewValues["DescripcionFlota"].S();
                        oFlota.iFlotaCU2 = eI.NewValues["FlotaCU2"].S();

                        if (oFlota.iFlotaCU2 != "")
                            oFlota.sDescripcionFlotaCU2 = oIGestCat.DBGetCodUnidad2(oFlota.iFlotaCU2);
                        else
                            oFlota.sDescripcionFlotaCU2 = "";

                        oFlota.iFlotaSENEAM = eI.NewValues["FlotaSENEAM"].S().I();
                        oFlota.iFlotaAPHIS = eI.NewValues["FlotaAPHIS"].S().I();
                        oFlota.iStatus = 1;

                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                        oFlota.iId = eU.Keys[0].S().I();                                                                  
                        oFlota.sDescripcion = eU.NewValues["DescripcionFlota"].S();
                        oFlota.iFlotaCU2 = eU.NewValues["FlotaCU2"].S();
                       
                        if (oFlota.iFlotaCU2 != "")
                            oFlota.sDescripcionFlotaCU2 = oIGestCat.DBGetCodUnidad2(oFlota.iFlotaCU2);
                        else
                            oFlota.sDescripcionFlotaCU2 = "";  

                        oFlota.iFlotaSENEAM = eU.NewValues["FlotaSENEAM"].S().I();
                        oFlota.iFlotaAPHIS = eU.NewValues["FlotaAPHIS"].S().I();
                        oFlota.iStatus = eU.NewValues["Status"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;

                        oFlota.sDescripcion = eV.NewValues["DescripcionFlota"].S();
                        oFlota.iStatus = eV.NewValues["Status"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oFlota.iId = eD.Keys[0].S().I();
                        oFlota.sDescripcion = eD.Values["DescripcionFlota"].S();
                        break;
                }

                return oFlota;
            }
        }

        private bool bValidaActualizacion
        {
            get
            {
                bool bValida = true;
                ASPxDataValidationEventArgs eU = (ASPxDataValidationEventArgs)oIView.oCrud;

                bValida = eU.NewValues["DescripcionFlota"].S().ToUpper() != eU.OldValues["DescripcionFlota"].S().ToUpper();

                return bValida;
            }

        }
    }
}