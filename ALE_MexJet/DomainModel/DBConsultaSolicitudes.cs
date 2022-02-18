using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ALE_MexJet.Objetos;
using System.Data;
using ALE_MexJet.Clases;
using NucleoBase.Core;

namespace ALE_MexJet.DomainModel
{
    public class DBConsultaSolicitudes : DBBase
    {
        public DataTable dtGetEstatusSolicitud
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaEstatusSolicitud]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable dtGetConsultaUltimasSolicitudes 
        {
          get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultarSolicitudesInicialmente]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable dtObjConsultaSolicitudes(ConsultaSolicitudes B)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultarSolicitudes]",
                                                              "@IdCliente", B.IdCliente
                                                            , "@ClaveCliente", B.ClaveCliente
                                                            , "@IdContrato", B.IdContrato
                                                            , "@ClaveContrato", B.ClaveContrato
                                                            , "@IdTrip", B.IdTrip
                                                            , "@Idusuario", B.Idusuario
                                                            , "@UsuarioCreacion", B.UsuarioCreacion
                                                            , "@IdEstatus", B.IdEstatus
                                                            , "@FechaCreacionIni", B.FechaCreacionIni
                                                            , "@FechaCreacionFin", B.FechaCreacionFin
                                                            , "@FechaVueloIni", B.FechaVueloIni
                                                            , "@FechaVueloFin", B.FechaVueloFin
                                                                                 );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}