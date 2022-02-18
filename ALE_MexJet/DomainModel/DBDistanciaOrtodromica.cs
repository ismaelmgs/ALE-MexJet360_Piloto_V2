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
    public class DBDistanciaOrtodromica : DBBase
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
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaDistanciaOrtodromica]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSave(DistanciaOrtodromica oDistanciaOrtodromica)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaDistanciaOrtodromica]", "@IdOrigen", oDistanciaOrtodromica.iIdOrigen,
                                                                                                    "@IdDEstino", oDistanciaOrtodromica.iIdDestino,
                                                                                                    "@DistanciaKms", oDistanciaOrtodromica.dcDistancia,
                                                                                                    "@Status", oDistanciaOrtodromica.iStatus,
                                                                                                    "@IP", oDistanciaOrtodromica.sIP,
                                                                                                    "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.DistanciasOrtodromicas), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se insertó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdate(DistanciaOrtodromica oDistanciaOrtodromica)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spu_MXJ_ActualizaDistanciaOrtodromica]", "@Id", oDistanciaOrtodromica.iId,
                                                                                                    "@IdOrigen", oDistanciaOrtodromica.iIdOrigen,
                                                                                                    "@IdDEstino", oDistanciaOrtodromica.iIdDestino,
                                                                                                    "@DistanciaKms",oDistanciaOrtodromica.dcDistancia,
                                                                                                    "@Status", oDistanciaOrtodromica.iStatus,
                                                                                                    "@IP", null,
                                                                                                    "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.DistanciasOrtodromicas), Convert.ToInt32(Enumeraciones.TipoOperacion.Actualizar), "Se actualizó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBDelete(DistanciaOrtodromica oDistanciaOrtodromica)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaDistanciaOrtodromica]", "@Id", oDistanciaOrtodromica.iId,
                                                                                      "@IP", null,
                                                                                      "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.DistanciasOrtodromicas), Convert.ToInt32(Enumeraciones.TipoOperacion.Eliminar), "Se eliminó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtGetAeropuerto
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCatalogoAeropuerto]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
        public int DBValida(DistanciaOrtodromica oDistanciaOrtodromica)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_ConsultaDistanciaOrtodromicaExiste]", "@IdOrigen", oDistanciaOrtodromica.iIdOrigen,
                                                                                                    "@IdDEstino", oDistanciaOrtodromica.iIdDestino);
                                                                                                              
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
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
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCatalogoAeropuertoIATA]", "@IATA", sAeropuerto +"%");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}