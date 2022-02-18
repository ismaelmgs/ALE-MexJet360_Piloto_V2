using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using System.Data;
using ALE_MexJet.Objetos;
using ALE_MexJet.Clases;
using System.Configuration;
using System.Data.SqlClient;

namespace ALE_MexJet.DomainModel
{
    public class DBTripGuide : DBBase
    {
        SqlConnection _SqlConnection;
        private SqlCommand _SqlCommand = new SqlCommand();
        private SqlParameter _SqlParameter = new SqlParameter();
        private SqlDataAdapter _SqlDataAdapter = new SqlDataAdapter();

        public int DBSave(TripGuide oTripGuide)
        {
            try
            {
                object oResult = new object();

                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaTripGuide]", "@IdSolicitud", oTripGuide.iIdSolicitud
                                                                                     , "@IdTrip", oTripGuide.iIdTrip
                                                                                     , "@IdPierna", oTripGuide.iIdPierna
                                                                                     , "@NombreArchivoPDF", oTripGuide.sNombreArchivoPDF
                                                                                     , "@PDF", oTripGuide.bPDF
                                                                                     , "@UsuarioCreacion", oTripGuide.sUsuarioCreacion
                                                                                     , "@IP", oTripGuide.sIP
                                                                                     , "@Observaciones", oTripGuide.sObservaciones
                                                                                     , "@NombreContacto", oTripGuide.sNombreContacto
                                                                                     , "@ICAOOrigen", oTripGuide.sICAOOrigen
                                                                                     , "@NombreAeropuertoOrigen", oTripGuide.sNombreAeropuertoOrigen
                                                                                     , "@ICAODestino", oTripGuide.sICAODestino
                                                                                     , "@NombreAeropuertoDestino", oTripGuide.sNombreAeropuertoDestino
                                                                                     , "@FechaSalida", oTripGuide.dtFechaSalida
                                                                                     , "@NumeroPasajero", oTripGuide.iNumeroPasajero
                                                                                     , "@Aeronave", oTripGuide.sAeronave
                                                                                     , "@Piloto", oTripGuide.sPiloto
                                                                                     , "@PilotoTelefono", oTripGuide.sCoPilotoTelefono
                                                                                     , "@CoPiloto", oTripGuide.sCoPiloto
                                                                                     , "@CoPilotoTelefono", oTripGuide.sCoPilotoTelefono
                                                                                     );
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBConsultaContactoSolicitud(int iIdSolicitud)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaContactoSolicitud]", "@IdSolicitud", iIdSolicitud);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable DBConsultaVendedorSolicitud(int iIdSolicitud)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaVendedorSolicitud]", "@IdSolicitud", iIdSolicitud);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





    }
}