using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using NucleoBase.Core;
using System.Data;
using ALE_MexJet.Clases;

namespace ALE_MexJet.DomainModel
{
    public class DBUtils : DBBase
    {
        public long DBSaveBitacora(int IdModulo, int IdAccion, string sDescripcion)
        {
            try
            {
                IPHostEntry host;
                string localIP = "?";
                host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily.S() == "InterNetwork")
                    {
                        localIP = ip.ToString();
                    }
                }

                object oBit = oDB_SP.EjecutarValor("[Seguridad].[spI_MXJ_InsertaBitacoraMovimiento]",   "@IdModulo", IdModulo,
                                                                                                        "@IdAccion", IdAccion,
                                                                                                        "@Descripcion", sDescripcion,
                                                                                                        "@UsuarioCreacion", Utils.GetUser,
                                                                                                        "@IP", localIP);
                return oBit != null ? oBit.S().L() : 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBSaveBitacoraError(object[] oArr)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Seguridad].[spI_MXJ_InsertaLogError]", oArr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DBGetPromedioVuelo(string sOrigen, string sDestino)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[Principales].[spS_MXJ_ConsultaPromedioTiempoVuelo]", "@Origen", sOrigen, 
                                                                                                "@Destino", sDestino);

                return oRes == null ? "00:00:00" : oRes.S();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string DBGetPromedioVueloID(int iIdOrigen, int iIdDestino)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[Principales].[spS_MXJ_ConsultaPromedioTiempoVueloId]", "@IdOrigen", iIdOrigen,
                                                                                                            "@IdDestino", iIdDestino);

                return oRes == null ? "00:00:00" : oRes.S();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DateTime DBGetFechaServidor
        {
            get
            {
                try
                {
                    object o = oDB_SP.EjecutarValor("[Principales].[spS_MXJ_ConsultaFechaHoraServidor]");

                    return o != null ? o.S().Dt() : DateTime.Now;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public decimal DBGettipoCambio
        {
            get
            {
                try
                {
                    object oRespuesta = new DBSAP().DBGetTipoCambio;
                    decimal dTipoCambio = oRespuesta.S().D();

                    return decimal.Round(dTipoCambio, 4);
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }

        }

        public void DBInicioSistemaBitacora()
        {
            try
            {
                oDB_SP.EjecutarSP("[Seguridad].[spI_MXJ_BitacoraInicioSistema]", "@Usuario", Utils.GetUser,
                                       "@IP", Utils.GetIPAddress());
            }
            catch(Exception)
            {
                throw;
            }
        }

        public long DBSaveBitacoraModulos(int IdModulo, int IdAccion, string sDescripcion)
        {
            try
            {
                object oBit = oDB_SP.EjecutarValor("[Seguridad].[spI_MXJ_InsertaBitacoraModulos]", "@IdModulo", IdModulo,
                                                                                                   "@IdAccion", IdAccion,
                                                                                                   "@Descripcion", sDescripcion,
                                                                                                   "@UsuarioCreacion", Utils.GetUser,
                                                                                                   "@IP", Utils.GetIPAddress());

                return oBit != null ? oBit.S().L() : 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }        
    }
}