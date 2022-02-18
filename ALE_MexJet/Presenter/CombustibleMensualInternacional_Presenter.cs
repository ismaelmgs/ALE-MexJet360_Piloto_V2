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
    public class CombustibleMensualInternacional_Presenter : BasePresenter<IViewCombustibleMensualInternacional>
    {
        private readonly DBCombustibleMensualInternacional oIGestCat;

        public CombustibleMensualInternacional_Presenter(IViewCombustibleMensualInternacional oView, DBCombustibleMensualInternacional oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oView.eSearchObjMes += eGetMes_Presenter;
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
                    oIView.ObtieneValoresCombustible();
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Modificacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);

                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detecto el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
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
                    oIView.ObtieneValoresCombustible();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detecto el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
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
                    oIView.ObtieneValoresCombustible();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detecto el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
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
       
        protected void eGetMes_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjectsMes(oIGestCat.DBSearchObjMes(oIView.oArrFiltrosMes));
        }
        private CombustibleMensualInternacional oCatalogo
        {
            get
            {
                CombustibleMensualInternacional oCatalogo = new CombustibleMensualInternacional();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oCatalogo.iAnio = eI.NewValues["Anio"].S().I();
                        oCatalogo.iIdMes = eI.NewValues["IdMes"].S().I();
                        oCatalogo.dImporte = eI.NewValues["Importe"].S().D();
                        oCatalogo.iStatus = 1;
                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;
                        oCatalogo.iIdCombustibleMenInt = eU.Keys["IdCombustibleMenInt"].S().I();
                        oCatalogo.iAnio = eU.NewValues["Anio"].S().I(); ;
                        oCatalogo.iIdMes = eU.NewValues["IdMes"].S().I();
                        oCatalogo.dImporte = eU.NewValues["Importe"].S().D();
                        oCatalogo.iStatus = eU.NewValues["Status"].S().I();
                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;
                        oCatalogo.iAnio = eV.NewValues["Anio"].S().I();
                        oCatalogo.iIdMes = eV.NewValues["IdMes"].S().I();
                        oCatalogo.iStatus = 1;
                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oCatalogo.iIdCombustibleMenInt = eD.Keys["IdCombustibleMenInt"].S().I();
                        break;
                }
                return oCatalogo;
            }
        }
        private bool bValidaActualizacion
        {
            get
            {
                bool bValida = true;
                ASPxDataValidationEventArgs eU = (ASPxDataValidationEventArgs)oIView.oCrud;
                bValida = ((eU.NewValues["Anio"].S().I() != eU.OldValues["Anio"].S().I()) || (eU.NewValues["IdMes"].S().I() != eU.OldValues["IdMes"].S().I()));
                return bValida;
            }


        }
    }
}