using ALE_MexJet.Clases;
using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using NucleoBase.BaseDeDatos;
using System.Web.Script.Serialization;
using DevExpress.XtraPrinting.Native;
using RestSharp;
using Helper = ALE_MexJet.Clases.Helper;

namespace ALE_MexJet.DomainModel
{
    public class DBAjuste : DBBase
    {
        public DataTable DBGetContratos()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaContratosAjuste]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetRemisionesContrato(int iIdContrato)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaRemisionesContrato]", "@IdContrato", iIdContrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSetInsertaAjuste(AjusteRemision oA)
        {
            try
            {
                object oRes = new object();

                oRes = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaAjusteRemision]", "@IdRemision", oA.IIdRemision,
                                                                                             "@Tipo", oA.ITipo,
                                                                                             "@IdMotivo", oA.IIdMotivo,
                                                                                             "@Horas", oA.SHoras,
                                                                                             "@Comentarios", oA.SComentarios,
                                                                                             "@Estatus", oA.IEstatus,
                                                                                             "@Usuario", oA.SUsuario);

                return oRes.I();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public List<Parametros> getParameters()
        {
            try
            {
                JavaScriptSerializer ser = new JavaScriptSerializer();
                List<Parametros> p = new List<Parametros>();

                //TokenWS oToken = Utils.ObtieneToken;
                //var client = new RestClient(Helper.US_UrlObtieneParametros);//Esta no se ocupara xq viene del webservice de portal de clientes
                //var request = new RestRequest(Method.GET);
                //request.AddHeader("Authorization", oToken.token);
                //IRestResponse response = client.Execute(request);
                //var resp = response.Content;
                //p = ser.Deserialize<List<Parametros>>(resp);

                DataTable dtParam = new DataTable();

                dtParam = oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ObtieneParametros]");

                if (dtParam != null && dtParam.Rows.Count > 0)
                {
                    for (int i = 0; i < dtParam.Rows.Count; i++)
                    {
                        Parametros oP = new Parametros();
                        oP.Nombre = dtParam.Rows[i]["Nombre"].S();
                        oP.Valor = dtParam.Rows[i]["Valor"].S();
                        p.Add(oP);
                    }
                }

                return p;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataSet DBGetDatosAutorizador(int iIdContrato)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaAutorizador]", "@iIdContrato", iIdContrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetMotivosAjuste()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaMotivosAjustes]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}