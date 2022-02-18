using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ALE_MexJet.Clases;
using NucleoBase.Core;

namespace ALE_MexJet.DomainModel
{
    public class DBConsultaVuelos : DBBase
    {
        public DataTable DBConsultaVuelosUSA(string sFecha)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaVuelosUSA]","@Fecha", sFecha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBConsultaVuelosUSA_FPK(string sFecha)
        {
            try
            {
                DateTime dtIniTrip = DateTime.Now;
                string sQuery = string.Empty;
                string sConexion = string.Empty;
                string sFechaBusqueda = string.Empty;
                string sFechaActual = string.Empty;
                string sFechaManana = string.Empty;

                if (!string.IsNullOrEmpty(sFecha))
                {
                    sFechaBusqueda = sFecha.Dt().ToString("yyyy-MM-dd HH:mm:ss");
                    sFechaActual = sFecha.Dt().ToString("yyyy-MM-dd HH:mm:ss"); //dtIniTrip.ToString("yyyy-MM-dd HH:mm:ss");
                    sFechaManana = sFecha.Dt().AddDays(1).ToString("yyyy-MM-dd HH:mm:ss"); //dtIniTrip.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    sFechaActual = dtIniTrip.ToString("yyyy-MM-dd 00:00:00");
                    sFechaManana = dtIniTrip.AddDays(1).ToString("yyyy-MM-dd 00:00:00");
                }

                //sConexion = Utils.GetParametrosClave("101");
                //sQuery = "SELECT * FROM OPENQUERY([FPK_Live_1], 'SELECT tripleg.orig_nmbr TripNum, "
                //        + "airportO.icao_id AeropuertoICAO, "
                //        + "airportO.state Estado, "
                //        + "tripleg.leg_num, "
                //        + "tripleg.depicao_id Origen, "
                //        + "tripleg.arricao_id Destino, "
                //        + "tripleg.distance, "
                //        + "tripleg.elp_time, "
                //        + "tripleg.homdep OrigenCalzo, "
                //        + "tripleg.homarr DestinoCalzo, "
                //        + "tripleg.requestor, "
                //        + "tripleg.cat_code, "
                //        + "tripleg.pax_total CantPax, "
                //        + "tripleg.client, "
                //        + "tripleg.lastuser, "
                //        + "tripmain.tail_nmbr matricula, "
                //        + "tripleg.pic, "
                //        + "tripleg.sic, "
                //        + "tripleg.legid, "
                //        + "airportO.city ciudadOrigen, "
                //        + "airportO.country paisOrigen, "
                //        + "airportD.city	ciudadDestino, "
                //        + "airportD.country paisDestino, "
                //        + "tripmain.crewcode, "
                //        + "trim(pilots.first_name) + '' '' + trim(pilots.last_name) piloto, "
                //        + "tripleg.client contrato, "
                //        + "aereo.desc tipoavion, "
                //        + "tripleg.fltno NumeroVuelo "
                //        + "FROM tripleg tripleg "
                //        + "LEFT JOIN tripmain tripmain ON tripleg.orig_nmbr = tripmain.orig_nmbr  "
                //        + "INNER JOIN airport airportO ON tripleg.depicao_id = airportO.icao_id "
                //        + "INNER JOIN airport airportD ON tripleg.arricao_id = airportD.icao_id "
                //        + "INNER JOIN crew pilots ON tripleg.pic = pilots.crewcode "
                //        + "INNER JOIN aircraft aereo ON tripmain.type_code = aereo.code "
                //        + "WHERE 1 = 1 "
                //        + "AND LTRIM(RTRIM(tripleg.pic)) <> '''' "
                //        + "AND (airportO.country = ''US'' OR airportD.country = ''US'') "
                //        + "AND (tripleg.homdep >= {ts ''" + sFechaActual + "''}) "
                //        + "AND (tripleg.homdep <= {ts ''" + sFechaManana + "''})') ";
                //return new DBBaseFPK().oDB_SP.EjecutarDT_DeQuery(sQuery);


                return new DBBaseFPK().oDB_SP.EjecutarDT("[ALE].[dbo].[spS_MEXJET_ConsultaVuelosUSA]", "@FechaInicio", sFechaActual, "@FechaFin", sFechaManana);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBConsultaParametros()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaParametrosALE]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}