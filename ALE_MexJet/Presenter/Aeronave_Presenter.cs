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
    public class Aeronave_Presenter: BasePresenter<IViewAeronave>
    {
        private readonly DBAeronave oIGestCat;

        public Aeronave_Presenter(IViewAeronave oView, DBAeronave oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eGetMarca += GetMarca_Presenter;
            oIView.eGetFlota += GetFlota_Presenter;
            oIView.eGetModelo += GetModelo_Presenter ;
            oIView.eGetBase += GetBase_Presenter;
            oIView.eGetMatriculaInfor += GetMatriculaInfor_Presenter;
            oIView.eGetBaseInfor += GetBaseInfor_Presenter;
            oIView.eGetUnidadNegocioInfor +=  GetUnidadNegocioInfor_Presenter;
            oIView.eGetCodigoUnidadDos += oIView_eGetCodigoUnidadDos;
        }

        void oIView_eGetCodigoUnidadDos(object sender, EventArgs e)
        {
            oIView.LoadCodigoUnidadDos(oIGestCat.DBSearchCodigoUnidadDosFlota(oIView.oArrFiltrosCodigoUnidadDos));
        }

        public void LoadObjects_Presenter()
        {
            oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }

        public void GetModelo_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCatalogoModelo(oIGestCat.dtObjModelo(oIView.oArrFiltrosModelo));
        }

        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBUpdate(oCatalogo);
                if (id > 0)
                {
                    oIView.ObtieneValores();
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Modificacion._I()].sMensaje, 
                        Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);

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
                int id = oIGestCat.DBSave(oCatalogo);
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
                int id = oIGestCat.DBDelete(oCatalogo);
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

        public void GetMarca_Presenter(object sender, EventArgs e)
        {

            oIView.LoadCatalogoMarca(oIGestCat.dtObjMarca);
        }

        public void GetFlota_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCatalogoFlota(oIGestCat.dtObjFlota);
        }

        

        public void GetBase_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCatalogoBase(oIGestCat.dtObjBase);
        }

        public void GetMatriculaInfor_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCatalogoMatriculaInfor(oIGestCat.dtObjMatriculaInfor);
        }

        public void GetBaseInfor_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCatalogoBaseInfor(oIGestCat.dtObjBaseInfor);
        }

        public void GetUnidadNegocioInfor_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCatalogoUnidadNegocioInfor(oIGestCat.dtObjUnidadNegocioInfor);
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

        private Aeronave oCatalogo
        {
            get
            {
                Aeronave oAeronave = new Aeronave();
                string sFechaDefault = "01/01/1900";
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oAeronave.iId = 0;
                        oAeronave.sMatricula = eI.NewValues["Matricula"].S();
                        oAeronave.sSerie = eI.NewValues["Serie"].S();
                        oAeronave.iIdMarca = eI.NewValues["IdMarca"].S().I();
                        oAeronave.iIFlota = eI.NewValues["IdFlota"].S().I();
                        oAeronave.iIdModelo = eI.NewValues["IdModelo"].S().I();
                        oAeronave.iAñoFabricacion = eI.NewValues["AñoFabricacion"].S().I();
                        oAeronave.iCapacidadPasajero= eI.NewValues["CapacidadPasajeros"].S().I();
                        oAeronave.iIdAeropuertoBase = eI.NewValues["IdAeropuertoBase"].S().I();
                        oAeronave.sMAtriculaInfo = eI.NewValues["MatriculaInfo"].S();
                        //oAeronave.sMAtriculaInfo = oIGestCat.DBGetCodUnidad2(oAeronave.sIdMAtriculaInfo);
                        char[] chi = {' '};
                        string[] sMatriculaInforDivididai = oAeronave.sMAtriculaInfo.Split(chi);
                        oAeronave.sIdMAtriculaInfo = sMatriculaInforDivididai[0];
                        oAeronave.sIdBaseInfo = eI.NewValues["IdBaseInfo"].S();
                        oAeronave.sBaseInfo = oIGestCat.DBGetCodUnidad3(oAeronave.sIdBaseInfo);
                        oAeronave.sIdUnidadNegocio = eI.NewValues["IdUnidadNegocioInfo"].S();
                        oAeronave.sUnidadNegocio = oIGestCat.DBGetCodUnidad4(oAeronave.sIdUnidadNegocio);
                        oAeronave.iIdTipo = eI.NewValues["IdTipo"].S().I();
                        
                        if (eI.NewValues["FechaInicio"] == null)                        
                            oAeronave.dtFechaInicio = sFechaDefault.S().Dt();                        
                        else
                            oAeronave.dtFechaInicio = eI.NewValues["FechaInicio"].S().Dt();

                        if (eI.NewValues["FechaFin"] == null)                                                    
                            oAeronave.dtFechaFin = sFechaDefault.S().Dt();                                   
                        else
                            oAeronave.dtFechaFin = eI.NewValues["FechaFin"].S().Dt();
                        
                        
                            oAeronave.bReporteSENEAM = eI.NewValues["ReporteSENEAM"].S().I();
                        oAeronave.iStatus = 1;

                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;
                        oAeronave.iId = 0;
                        oAeronave.sSerie = eV.NewValues["Serie"].S();
                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                        oAeronave.iId = eU.Keys[0].S().I();
                        oAeronave.sMatricula = eU.NewValues["Matricula"].S();
                        oAeronave.sSerie = eU.NewValues["Serie"].S();
                        oAeronave.iIdMarca = eU.NewValues["IdMarca"].S().I();
                        oAeronave.iIFlota = eU.NewValues["IdFlota"].S().I();
                        oAeronave.iIdModelo = eU.NewValues["IdModelo"].S().I();
                        oAeronave.iAñoFabricacion = eU.NewValues["AñoFabricacion"].S().I();
                        oAeronave.iCapacidadPasajero = eU.NewValues["CapacidadPasajeros"].S().I();
                        oAeronave.iIdAeropuertoBase = eU.NewValues["IdAeropuertoBase"].S().I();                        
                        
                        oAeronave.sIdBaseInfo = eU.NewValues["IdBaseInfo"].S();
                        oAeronave.sIdUnidadNegocio = eU.NewValues["IdUnidadNegocioInfo"].S();
                        oAeronave.iIdTipo = eU.NewValues["IdTipo"].S().I();
                        oAeronave.dtFechaInicio = eU.NewValues["FechaInicio"].S().Dt();

                        if (eU.NewValues["FechaInicio"] == null)
                            oAeronave.dtFechaInicio = sFechaDefault.S().Dt();
                        else
                            oAeronave.dtFechaInicio = eU.NewValues["FechaInicio"].S().Dt();

                        if (eU.NewValues["FechaFin"] == null)                                                   
                            oAeronave.dtFechaFin = sFechaDefault.S().Dt();                        
                        else
                            oAeronave.dtFechaFin = eU.NewValues["FechaFin"].S().Dt();

                        oAeronave.bReporteSENEAM = eU.NewValues["ReporteSENEAM"].S().I();
                        oAeronave.iStatus = eU.NewValues["Status"].S().I();
                        //oAeronave.sMAtriculaInfo = oIGestCat.DBGetCodUnidad2(oAeronave.sIdMAtriculaInfo);
                        oAeronave.sMAtriculaInfo = eU.NewValues["MatriculaInfo"].S();
                        char[] ch = {' '};
                        string[] sMatriculaInforDividida = oAeronave.sMAtriculaInfo.Split(ch);
                        oAeronave.sIdMAtriculaInfo = sMatriculaInforDividida[0];
                        oAeronave.sBaseInfo = oIGestCat.DBGetCodUnidad3(oAeronave.sIdBaseInfo);
                        oAeronave.sUnidadNegocio = oIGestCat.DBGetCodUnidad4(oAeronave.sIdUnidadNegocio);
                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oAeronave.iId = eD.Keys[0].S().I();
                        break;
                }

                return oAeronave;
            }
        }

        private bool bValidaActualizacion
        {
            get
            {
                bool bValida = true;
                ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;
                bValida = (eV.NewValues["Serie"].S().ToUpper() != eV.OldValues["Serie"].S().ToUpper());
                return bValida;
            }
        }
        
    }
}