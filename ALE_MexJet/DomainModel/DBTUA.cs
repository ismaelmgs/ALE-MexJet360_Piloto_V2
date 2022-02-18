using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ALE_MexJet.Clases;
using NucleoBase.Core;
using ALE_MexJet.Objetos;

namespace ALE_MexJet.DomainModel
{
    public class DBTUA : DBBase
    {
        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaTUA]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchObjMes(params object[] oArrFiltrosMes)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaMes]", oArrFiltrosMes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSave(TUA oTUA)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaTUA]", "@IdAeropuerto", oTUA.iIdAeropuerto,
                                                                                    "@IdMes", oTUA.iIdMes,
                                                                                    "@Anio", oTUA.iAnio,
                                                                                    "@Nacional", oTUA.dNacional,
                                                                                    "@Internacional", oTUA.dInternacional,
                                                                                    "@Status", oTUA.iStatus,
                                                                                    "@IP", oTUA.sIP,
                                                                                    "@Usuario", Utils.GetUser);

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Cliente.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se insertó el registro: " + oResult.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdate(TUA oTUA)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spU_MXJ_ActualizaTUA]", "@IdTUA", oTUA.iIdTUA,
                                                                                        "@IdAeropuerto", oTUA.iIdAeropuerto,
                                                                                        "@IdMes", oTUA.iIdMes,
                                                                                        "@Anio", oTUA.iAnio,
                                                                                        "@Nacional", oTUA.dNacional,
                                                                                        "@Internacional", oTUA.dInternacional,
                                                                                        "@Status", oTUA.iStatus,
                                                                                        "@Usuario", Utils.GetUser,
                                                                                        "@IP", oTUA.sIP
                                                                                        );
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Cliente.S().I(), Enumeraciones.TipoOperacion.Actualizar.S().I(), "Se actualizó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBDelete(TUA oTUA)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaTUA]", "@IdTUA", oTUA.iIdTUA,
                                                                                        "@IP", oTUA.sIP,
                                                                                        "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Cliente.S().I(), Enumeraciones.TipoOperacion.Eliminar.S().I(), "Se eliminó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchObjAero(params object[] oArrFiltrosAereo)
        {
            try
            {
                return oDB_SP.EjecutarDT("Catalogos.spS_MXJ_ConsultaOrigenAeropuertoTua");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBFiltraAeropuertos(string sAeropuerto)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCatalogoAeropuertoIATA]", "@IATA", sAeropuerto + "%");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBValida(TUA oTua)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_ConsultaTUAExiste]", "@IdAeropuerto", oTua.iIdAeropuerto,
                                                                                            "@IdMes", oTua.iIdMes,
                                                                                            "@Anio", oTua.iAnio);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}