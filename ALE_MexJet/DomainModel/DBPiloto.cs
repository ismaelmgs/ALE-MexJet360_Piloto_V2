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
    public class DBPiloto : DBBase
    {
        public DataTable dtObjsCat
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaPilotos]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaPilotos]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSave(Piloto piloto)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaPilotos]", "@CREWCODE", piloto.sCrewCode,
                                                                                        "@PilotoNombre", piloto.sPilotoNombre,
                                                                                        "@PilotoApPat", piloto.sPilotoApPaterno,
                                                                                        "@PilotoApMat", piloto.sPilotoApMaterno,
                                                                                        "@Status", piloto.iStatus,
                                                                                        "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Piloto), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se insertó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)            {
                throw ex;
            }
        }

        public int DBUpdate(Piloto piloto)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spU_MXJ_ActualizaPilotos]", "@IdPiloto", piloto.iIdPiloto,
                                                                                    "@CREWCODE", piloto.sCrewCode,
                                                                                    "@PilotoNombre", piloto.sPilotoNombre,
                                                                                    "@PilotoApPat", piloto.sPilotoApPaterno,
                                                                                    "@PilotoApMat", piloto.sPilotoApMaterno,
                                                                                    "@Status", piloto.iStatus,
                                                                                    "@IP", null,
                                                                                    "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Piloto), Convert.ToInt32(Enumeraciones.TipoOperacion.Actualizar), "Se actualizó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBDelete(Piloto piloto)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaPilotos]", "@IdPiloto", piloto.iIdPiloto,
                                                                                    "@IP", null,
                                                                                    "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Piloto), Convert.ToInt32(Enumeraciones.TipoOperacion.Eliminar), "Se eliminó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}