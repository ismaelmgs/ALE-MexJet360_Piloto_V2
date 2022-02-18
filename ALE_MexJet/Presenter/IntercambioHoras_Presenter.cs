using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using DevExpress.Web.Data;
using DevExpress.Web;

namespace ALE_MexJet.Presenter
{
    public class IntercambioHoras_Presenter:BasePresenter<IViewIntercambioHoras>
    {        
        private readonly DBIntercambioHoras oIDBHoras;

        public IntercambioHoras_Presenter(IViewIntercambioHoras oView, DBIntercambioHoras oDato)
            : base(oView)
        {
            oIDBHoras = oDato;
            oView.eGetCliente += oView_eGetCliente;
            oView.eGetIntercambioHoras += oView_eGetIntercambioHoras;
            oView.eGetContrato += oView_eGetContrato;
            oView.eGetHorasDisponiblesOrigen += oView_eGetHorasDisponiblesOrigen;
            oView.eGetHorasDisponiblesDestino += oView_eGetHorasDisponiblesDestino;
        }

        void oView_eGetContrato(object sender, EventArgs e)
        {
            oIView.LoadContrato(oIDBHoras.DBSearchContrato(oIView.oArrFiltroContrato));
        }

        private void oView_eGetIntercambioHoras(object sender, EventArgs e)
        {            
            oIView.LoadIntercambioHoras(oIDBHoras.DBSearchIntercambioHoras(oIView.oArrFiltroIntercambioHoras));
        }

        void oView_eGetCliente(object sender, EventArgs e)
        {
            oIView.LoadClientes(oIDBHoras.DBSearchCliente(oIView.oArrFiltroCliente));
        }

        void oView_eGetHorasDisponiblesOrigen(object sender, EventArgs e)
        {
            oIView.LoadHorasDisponiblesOrigen(oIDBHoras.DBSearchHorasDisponibles(oIView.oArrFiltroHorasDisponiblesOrigen));
        }

        void oView_eGetHorasDisponiblesDestino(object sender, EventArgs e)
        {
            oIView.LoadHorasDisponiblesDestino(oIDBHoras.DBSearchHorasDisponibles(oIView.oArrFiltroHorasDisponiblesDestino));
        }

        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            try 
            {                
                int id = oIDBHoras.DBInsertaTraspasoHoras(oIView.oTraspasoHoras);
                if (id > 0)
                {
                    oIView.MostrarMensaje("Se guardó el registro", " REGISTRO GENERADO");
                    oIView.ObtieneValores();
                }
                else if (id == -1)
                {
                    oIView.MostrarMensaje("No cuenta con horas o sobre pasa las horas permitidas", " REGISTRO GENERADO");
                    oIView.ObtieneValores();
                }
                else if (id == -2)
                {
                    oIView.MostrarMensaje("No esta permitido el intercambio de horas con el mismo contrato", " REGISTRO GENERADO");
                    oIView.ObtieneValores();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }

		protected override void SaveObj_Presenter(object sender, EventArgs e)
		{
			try
			{
				int id = oIDBHoras.DBUpdateTraspasoHoras(oIView.oTraspasoHoras);
				if (id > 0)
				{
					oIView.MostrarMensaje("Se guardó el registro", " REGISTRO GENERADO");
					oIView.ObtieneValores();
				}
				else if (id == -1)
				{
					oIView.MostrarMensaje("No cuenta con horas o sobre pasa las horas permitidas", " REGISTRO GENERADO");
					oIView.ObtieneValores();
				}
				else if (id == -2)
				{
					oIView.MostrarMensaje("No esta permitido el intercambio de horas con el mismo contrato", " REGISTRO GENERADO");
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
				int id = oIDBHoras.DBDelete(oCatalogo);
				if (id > 0)
				{
					oIView.MostrarMensaje(
						Mensajes.LMensajes[(int)Enumeraciones.Mensajes.Eliminacion].sMensaje, 
						Mensajes.LMensajes[(int)Enumeraciones.Mensajes.Aviso].sMensaje
					);
					oIView.ObtieneValores();
				}
			}
			catch (Exception ex)
			{
				oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
			}
		}

		private TraspasoHoras oCatalogo
		{
			get
			{
				TraspasoHoras oTraspasoHoras = new TraspasoHoras();
				switch (oIView.eCrud)
				{
					case Enumeraciones.TipoOperacion.Insertar:
						ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
						oTraspasoHoras.iIdIntercambioHoras = 0;
						oTraspasoHoras.iIdClienteOrigen = eI.NewValues["IdClienteOrigen"].S().I();
						oTraspasoHoras.iIdContratoOrigen = eI.NewValues["IdContratoOrigen"].S().I();
						oTraspasoHoras.iIdClienteDestino = eI.NewValues["IdClienteDestino"].S().I();
						oTraspasoHoras.iIdContratoDestino = eI.NewValues["IdContratoDestino"].S().I();
						oTraspasoHoras.sHorasIntercambiadas = eI.NewValues["HorasIntercambiadas"].S();
						oTraspasoHoras.sObservaciones = eI.NewValues["Observaciones"].S();
						oTraspasoHoras.iStatus = 1;
						break;
					case Enumeraciones.TipoOperacion.Actualizar:
						ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;
						oTraspasoHoras.iIdIntercambioHoras = 0;
						oTraspasoHoras.iIdClienteOrigen = eU.NewValues["IdClienteOrigen"].S().I();
						oTraspasoHoras.iIdContratoOrigen = eU.NewValues["IdContratoOrigen"].S().I();
						oTraspasoHoras.iIdClienteDestino = eU.NewValues["IdClienteDestino"].S().I();
						oTraspasoHoras.iIdContratoDestino = eU.NewValues["IdContratoDestino"].S().I();
						oTraspasoHoras.sHorasIntercambiadas = eU.NewValues["HorasIntercambiadas"].S();
						oTraspasoHoras.sObservaciones = eU.NewValues["Observaciones"].S();
						oTraspasoHoras.iStatus = eU.NewValues["Status"].S().I();
						break;
					case Enumeraciones.TipoOperacion.Eliminar:
						ASPxGridViewRowCommandEventArgs eD = (ASPxGridViewRowCommandEventArgs)oIView.oCrud;
						oTraspasoHoras.iIdIntercambioHoras = eD.KeyValue.S().I();
						break;
				}
				return oTraspasoHoras;
			}
		}
	}
}