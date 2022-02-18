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
using System.Data;

namespace ALE_MexJet.Presenter
{
    public class ServicioConCargo_Presenter : BasePresenter<IViewServicioCargo>
    {
        private readonly DBServicioConCargo oIGestCat;

        public ServicioConCargo_Presenter(IViewServicioCargo oView, DBServicioConCargo oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eGetCuentas += GetCuentas_Presenter;
            oIView.eGetArticulos += eGetArticulos_Presenter;
        }

        public void GetCuentas_Presenter(object sender, EventArgs e)
        {
            oIView.dtCuentas = oIGestCat.dtCuenta;
            oIView.dtCodUnidadUno = oIGestCat.GetCodigoUnidadUno;
        }

        public void eGetArticulos_Presenter(object sender, EventArgs e)
        {
            oIView.dtArticulos = new DBSAP().DBGetObtieneArticulos;
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
                ServicioConCargo oServicioConCargo = oCatalogo;
                int id = oIGestCat.DBUpdate(oServicioConCargo);
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
                ServicioConCargo oServicioConCargo = oCatalogo;

                int id = oIGestCat.DBSave(oServicioConCargo);
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
                ServicioConCargo oServicioConCargo = oCatalogo;

                int id = oIGestCat.DBDelete(oServicioConCargo);
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

        private ServicioConCargo oCatalogo
        {
            get
            {
                ServicioConCargo oServicioConCargo = new ServicioConCargo();
                
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oServicioConCargo.sDescripcion = eI.NewValues["ServicioConCargoDescripcion"].S();
                        oServicioConCargo.dImporte = eI.NewValues["Importe"].S().D();

                        //oServicioConCargo.sCveCuenta = eI.NewValues["cveCuenta"].S();
                        //DataRow[] drResultsI= oIView.dtCuentas.Select("acct = " + oServicioConCargo.sCveCuenta);
                        //oServicioConCargo.sDescripcionCuenta = drResultsI[0].ItemArray[2].S();

                        oServicioConCargo.sCveCodUnitUno = eI.NewValues["CveCodigoUnidad1"].S();
                        DataRow[] drResultsI = oIView.dtCodUnidadUno.Select("unit1 = '" + oServicioConCargo.sCveCodUnitUno + "'");
                        oServicioConCargo.sDescripcionCodUnitUno = drResultsI[0].ItemArray[1].S();

                        oServicioConCargo.sCveArticulo = eI.NewValues["CveArticulo"].S();
                        DataRow[] drResults = oIView.dtArticulos.Select("ItemCode = '" + eI.NewValues["CveArticulo"].S() +"'");
                        oServicioConCargo.sArticuloDescripcion = drResults[0]["ItemName"].S();

                        oServicioConCargo.bPasajero = eI.NewValues["PorPasajero"].S().B();
                        oServicioConCargo.bPierna = eI.NewValues["PorPierna"].S().B();
                        oServicioConCargo.iId = 0;
                        oServicioConCargo.iStatus = 1;

                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                        oServicioConCargo.iId = eU.Keys[0].S().I();
                        oServicioConCargo.sDescripcion = eU.NewValues["ServicioConCargoDescripcion"].S();

                        //oServicioConCargo.sCveCuenta = eU.NewValues["cveCuenta"].S();
                        //DataRow[] drResultsU = oIView.dtCuentas.Select("acct = " + oServicioConCargo.sCveCuenta);
                        //oServicioConCargo.sDescripcionCuenta = drResultsU[0].ItemArray[2].S();
                        oServicioConCargo.sCveCodUnitUno = eU.NewValues["CveCodigoUnidad1"].S();
                        DataRow[] drResultsU = oIView.dtCodUnidadUno.Select("unit1 = '" + oServicioConCargo.sCveCodUnitUno + "'");
                        oServicioConCargo.sDescripcionCodUnitUno = drResultsU[0].ItemArray[1].S();

                        oServicioConCargo.sCveArticulo = eU.NewValues["CveArticulo"].S();
                        drResultsU = oIView.dtArticulos.Select("ItemCode = '" + eU.NewValues["CveArticulo"].S() + "'");
                        oServicioConCargo.sArticuloDescripcion = drResultsU[0]["ItemName"].S();

                        oServicioConCargo.dImporte = eU.NewValues["Importe"].S().D();
                        oServicioConCargo.bPasajero = eU.NewValues["PorPasajero"].S().B();
                        oServicioConCargo.bPierna = eU.NewValues["PorPierna"].S().B();
                        oServicioConCargo.iStatus = eU.NewValues["Status"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;

                        oServicioConCargo.sDescripcion = eV.NewValues["ServicioConCargoDescripcion"].S();
                        oServicioConCargo.bPasajero = eV.NewValues["PorPasajero"].S().B();
                        oServicioConCargo.bPierna = eV.NewValues["PorPierna"].S().B();
                        oServicioConCargo.dImporte = eV.NewValues["Importe"].S().D();
                        oServicioConCargo.iStatus = eV.NewValues["Status"].S().I();
                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oServicioConCargo.sDescripcion = eD.Values["ServicioConCargoDescripcion"].S();
                        oServicioConCargo.iId = eD.Keys[0].S().I();
                        break;
                }

                return oServicioConCargo;
            }
        }
        private bool bValidaActualizacion
        {
            get
            {
                bool bValida = true;
                ASPxDataValidationEventArgs eU = (ASPxDataValidationEventArgs)oIView.oCrud;

                bValida = eU.NewValues["ServicioConCargoDescripcion"].S().ToUpper() != eU.OldValues["ServicioConCargoDescripcion"].S().ToUpper();

                return bValida;
            }


        }
    }
}