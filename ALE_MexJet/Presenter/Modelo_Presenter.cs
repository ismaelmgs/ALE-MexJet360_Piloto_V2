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
    public class Modelo_Presenter: BasePresenter<IViewModelo>
    {
        private readonly DBModelo oIGestCat;

        public Modelo_Presenter(IViewModelo oView, DBModelo oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eGetMarca += GetMarca_Presenter;
            oIView.eGetGrupoModelo += GetGrupoModelo_Presenter;
            oIView.eGetGrupoEspacio += GetGrupoEspacio_Presenter;
            oIView.eGetDesignador += GetDesignador_Presenter;
            oIView.eGetTipo += GetTipo_Presenter;
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
                int id = oIGestCat.DBUpdate(oCatalogo);
                if (id > 0)
                {
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Modificacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    oIView.ObtieneValores();
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

        protected void GetMarca_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCatalogoMarca(oIGestCat.dtObjsCatMarca);
        }
        
        protected void GetGrupoModelo_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCatalogoGrupoModelo(oIGestCat.dtObjsCatGrupoModelo);
        }
        protected void GetGrupoEspacio_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCatalogoGrupoEspacio(oIGestCat.dtObjsCatEspacio);
        }
        protected void GetDesignador_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCatalogoDesignador(oIGestCat.dtObjsCatDesignador);
        }
        protected void GetTipo_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCatalogoTipo(oIGestCat.dtObjsCatTipo);
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

        private Modelo oCatalogo
        {
            get
            {
                Modelo oModelo = new Modelo();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oModelo.iId = 0;
                        oModelo.sDescripcion = eI.NewValues["DescripcionModelo"].S();
                        oModelo.iMarca = eI.NewValues["IdMarca"].S().I();
                        oModelo.iGrupoModelo = eI.NewValues["IdGrupoModelo"].S().I();
                        oModelo.iTipo = eI.NewValues["IdTipo"].S().I();
                        oModelo.dVelocidad = eI.NewValues["Velocidad"].S().D();
                        oModelo.iGrupoTamaño = eI.NewValues["IdGrupoTamaño"].S().I();
                        oModelo.iHorasAño = eI.NewValues["IdHorasAño"].S().I();
                        oModelo.dPesoMaximo = eI.NewValues["PesoMaximo"].S().D();
                        oModelo.iDesignador = eI.NewValues["IdDesignador"].S().I();
                        oModelo.iStatus = 1;

                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                        oModelo.iId = eU.Keys[0].S().I();
                        oModelo.sDescripcion = eU.NewValues["DescripcionModelo"].S();
                        oModelo.iMarca = eU.NewValues["IdMarca"].S().I();
                        oModelo.iGrupoModelo = eU.NewValues["IdGrupoModelo"].S().I();
                        oModelo.iTipo = eU.NewValues["IdTipo"].S().I();
                        oModelo.dVelocidad = eU.NewValues["Velocidad"].S().D();
                        oModelo.iGrupoTamaño = eU.NewValues["IdGrupoTamaño"].S().I();
                        oModelo.iHorasAño = eU.NewValues["IdHorasAño"].S().I();
                        oModelo.dPesoMaximo = eU.NewValues["PesoMaximo"].S().D();
                        oModelo.iDesignador = eU.NewValues["IdDesignador"].S().I();
                        oModelo.iStatus = eU.NewValues["Status"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;
                        oModelo.sDescripcion = eV.NewValues["DescripcionModelo"].S();

                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oModelo.iId = eD.Keys[0].S().I();
                        break;
                }

                return oModelo;
            }
        }
        private bool bValidaActualizacion
        {
            get
            {
                bool bValida = true;
                ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;

                bValida = eV.NewValues["DescripcionMotivo"].S().ToUpper() != eV.OldValues["DescripcionMotivo"].S().ToUpper();

                return bValida;
            }
        }
    }
}