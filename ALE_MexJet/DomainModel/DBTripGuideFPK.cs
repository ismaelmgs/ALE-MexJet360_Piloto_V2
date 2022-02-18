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
    public class DBTripGuideFPK : DBBaseFPK
    {
        SqlConnection _SqlConnection;
        private SqlCommand _SqlCommand = new SqlCommand();
        private SqlParameter _SqlParameter = new SqlParameter();
        private SqlDataAdapter _SqlDataAdapter = new SqlDataAdapter();

        public DataTable DBGetObtienePiernasPorTrip(int iTrip)
        {
            try
            {
                string sNombre = Utils.ObtieneParametroPorClave("101");

                string sQuery = "SELECT * FROM OPENQUERY(" + sNombre + ", 'SELECT	leg_num, legid, depicao_id, arricao_id, locdep, locarr FROM tripleg WHERE orig_nmbr = " + iTrip.S() + "')";

                DataSet dsResultado = new DataSet();

                object[] oArrFil = { "@iTrip", iTrip, };

                dsResultado = oDB_SP.EjecutarDS_DeQuery(sQuery);
                return dsResultado.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetObtieneDetallePierna(int iPierna)
        {
            try
            {
                string sNombre = Utils.ObtieneParametroPorClave("101");

                string sQuery = "SELECT * FROM OPENQUERY(" + sNombre + ", 'SELECT	TL.locdep AS [HoraOrigenLocal], TL.locarr AS [HoraDestinoLocal], TL.gmtdep AS [HoraOrigenUTC], TL.gmtarr AS [HoraDestinoUTC], TL.homdep	AS [HoraOrigenHome], TL.homarr AS [HoraDestinoHome], PIL.first_name		AS [PILotoNombre], PIL.last_name AS [PILotoApellido], PIL.mobilephn AS [PilotoTelefono], COPIL.first_name	AS [CopilotoNombre], COPIL.last_name		AS [CopilotoApellido], COPIL.mobilephn		AS [CopilotoTelefono], TL.depicao_id		AS [AeropuertoOrigenICAO], AO.airport_nm		AS [AeropuertoOrigenNombre], AO.city				AS [AeropuertoOrigenCiudad], AO.state			AS [AeropuertoOrigenEstado], FBOO.name			AS [FBOOrigenNombre], IIF(FBOO.addr1='''',''Direccion no especificada'',FBOO.addr1)	AS [FBOOrigenDireccion], FBOO.city			AS [FBOOrigenCiudad], FBOO.state			AS [FBOOrigenEstado], FBOO.zip			AS [FBOOrigenCodigoPostal], TL.arricao_id		AS [AeropuertoDestinoICAO], AD.airport_nm		AS [AeropuertoDestinoNombre], AD.city				AS [AeropuertoDestinoCiudad], AD.state			AS [AeropuertoDestinoEstado], FBOD.name			AS [FBODestinoNombre], IIF(FBOD.addr1='''',''Direccion no especificada'',FBOD.addr1)	AS [FBODestinoDireccion], FBOD.city			AS [FBODestinoCiudad], FBOD.state AS [FBODestinoEstado], FBOD.zip	AS [FBODestinoCodigoPostal], TL.distance AS [Distancia], TL.elp_time	AS [TiempoEstimado], TL.pax_total AS [TotalPasajeros], (SELECT comments FROM triplgst WHERE(legid= TL.legid AND rec_type=''PT'')) As [Transportacion], (SELECT comments FROM triplgst WHERE(legid= TL.legid AND rec_type=''CA'')) As [Comisariato] FROM tripleg AS TL LEFT OUTER JOIN triplgst	AS FO		ON (FO.legid= TL.legid AND FO.rec_type=''FD'') LEFT OUTER JOIN triplgst	AS FD		ON (FD.legid= TL.legid AND FD.rec_type=''FB'') LEFT OUTER JOIN fbo			AS FBOO		ON (FO.code=FBOO.code AND FBOO.icao_id=TL.depicao_id) LEFT OUTER JOIN fbo AS FBOD		ON (FD.code=FBOD.code AND FBOD.icao_id=TL.arricao_id) Left Outer Join crew		AS PIL		ON (PIL.crewcode = TL.pic) Left Outer Join crew AS COPIL ON (COPIL.crewcode = TL.sic) Left Outer Join airport AS AO ON (AO.icao_id=TL.depicao_id) Left Outer Join airport AS AD ON (AD.icao_id=TL.arricao_id) WHERE TL.legid =" + iPierna.S() + "')";

                // Llamado al WCF que buscará en el Linked Server

                DataSet dsResultado = new DataSet();

                dsResultado = oDB_SP.EjecutarDS_DeQuery(sQuery);

                return dsResultado.Tables[0];

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable DBGetObtieneDatosTrip(int iTrip)
        {
            try
            {
                string sNombre = Utils.ObtieneParametroPorClave("101");

                string sQuery = "SELECT * FROM OPENQUERY(" + sNombre + ",'SELECT F.type_code, PM.desc, PM.reqname, PM.reqphone FROM tripmain AS PM LEFT OUTER JOIN fleet F ON (F.tail_nmbr=PM.tail_nmbr) WHERE PM.orig_nmbr = " + iTrip.S() + "')";

                // Llamado al WCF que buscará en el Linked Server


                DataSet dsResultado = new DataSet();

                dsResultado = oDB_SP.EjecutarDS_DeQuery(sQuery);

                return dsResultado.Tables[0];

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable DBGetObtienePasajeros(int iPierna)
        {
            DataSet dsResultado = new DataSet();
            try
            {
                string sNombre = Utils.ObtieneParametroPorClave("101");

                string sQuery = "SELECT * FROM OpenQuery(" + sNombre + ", 'SELECT PAX.first_name, PAX.last_name FROM trippax AS TPAX INNER JOIN	passngr	AS PAX	ON (TPAX.paxcode = PAX.paxcode) WHERE TPAX.legid=" + iPierna + "') ";

                dsResultado = oDB_SP.EjecutarDS_DeQuery(sQuery);

                return dsResultado.Tables[0];

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable DBObtieneCoordenadas(string ICAO)
        {
            DataSet dsResultado = new DataSet();
            try
            {
                string sNombre = Utils.ObtieneParametroPorClave("101");

                string sQuery = "SELECT * FROM OpenQuery(FPK_LIVE, 'SELECT lat_deg, lat_min, lat_ns, long_deg, long_min, long_ew FROM airport WHERE ICAO_ID = \"" + ICAO + "\"')";

                dsResultado = oDB_SP.EjecutarDS_DeQuery(sQuery);

                return dsResultado.Tables[0];

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}