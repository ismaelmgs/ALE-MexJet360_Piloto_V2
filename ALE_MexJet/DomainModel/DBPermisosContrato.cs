using ALE_MexJet.Clases;
using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;

namespace ALE_MexJet.DomainModel
{
    public class DBPermisosContrato : DBBase
    {
        public DataTable DBGetObtieneRolesActivos()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaRol]", "@Descripcion", string.Empty,
                                                                                "@ModuloDefault", string.Empty,
                                                                                "@estatus", 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetObtenePestanasContrato()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Seguridad].[spS_MXJ_ObtienePestanasContrato]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetObteneSeccionesPestanasContrato(int iIdRol, int iIdPestana)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Seguridad].[spS_MXJ_ConsultaSeccionContrato]", "@IdRol",iIdRol,
                                                                                            "@IdPestana", iIdPestana);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetObteneCamposSeccionesPestanasContrato(int iIdRol, int iIdPestana)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Seguridad].[spS_MXJ_ConsultaCampoSeccionContrato]", "@IdRol", iIdRol,
                                                                                                "@IdPestana", iIdPestana);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSetInsertaPermisosRolContrato(PermisosContrato oPermisos)
        {
            try
            {
                int iRol = 0;

                object oRes = oDB_SP.EjecutarValor("[Seguridad].[spD_MXJ_EliminaCamposContratoRol]", "@IdPestana", oPermisos.iPestana,
                                                                                                    "@IdSeccion", oPermisos.secciones[0].iSeccion,
                                                                                                    "@IdRol", oPermisos.iRol);

                if (oRes.S().I() > 0)
                {
                    foreach (SeccionesPestana items in oPermisos.secciones)
                    {
                        foreach (CamposPestana itemc in items.campos)
                        {
                            object oValor = oDB_SP.EjecutarValor("[Seguridad].[spI_MXJ_InsertaPermisosRolContrato]", "@IdRol", oPermisos.iRol,
                                                                                                                    "@IdPestana", oPermisos.iPestana,
                                                                                                                    "@IdSeccion", items.iSeccion,
                                                                                                                    "@AccesoSeccion", items.iAcceso,
                                                                                                                    "@IdCampo", itemc.iCampo,
                                                                                                                    "@AccesoCampo", itemc.iAcceso,
                                                                                                                    "@UsuarioCreacion", Utils.GetUser);

                            iRol += oValor.S().I();
                        }
                    }
                }

                return iRol;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetObtienePermisosContratoPorRol(int iRol)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Seguridad].[spS_MXJ_ConsultaPermisosContrato]", "@IdRol", iRol);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet DBGetObtienePermisosContratoParaCheckBox(int iRol)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Seguridad].[spS_MXJ_ConsultaCheckBoxCampoSeccionContrato]", "@IdRol", iRol);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}