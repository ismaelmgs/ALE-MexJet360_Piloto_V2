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
    public class DBPrefactura : DBBase
    {
        public DataTable dtObjsCatCliente
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaClientesConContrato]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Obtiene los facturantes de SC por moneda y contrato de Syteline
        /// </summary>
        /// <param name="sTipo">Moneda del facturante</param>
        /// <param name="sContrato">Clave del contrato</param>
        /// <returns></returns>
        public DataTable DBGetFacturanteSC(string sTipo, string sContrato)
        {
            try
            {
                ALE_MexJet.wsSyteline.Iws_SyteLineClient wssyte = new wsSyteline.Iws_SyteLineClient();
                return wssyte.GetFacturantes(sTipo,sContrato).Tables[0];
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        /// <summary>
        /// Obtiene los facturantes de SV por moneda y contrato de Syteline
        /// </summary>
        /// <param name="sTipo">Moneda del facturante</param>
        /// <param name="sContrato">Clave del contrato</param>
        /// <returns></returns>
        public DataTable DBGetFacturanteSV(string sTipo, string sContrato)
        {
            try
            {
                ALE_MexJet.wsSyteline.Iws_SyteLineClient wssyte = new wsSyteline.Iws_SyteLineClient();
                return wssyte.GetFacturantes(sTipo, sContrato).Tables[0];
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public DataTable DBGetFacturantesSAP(int iIdCliente)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaFacturantesDeCliente]", "@IdCliente", iIdCliente);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable DBGetContratoCliente(int iIdCliente)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_ConsultaContratosporCliente]", "@IdCliente", iIdCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetRemisionesContrato(int idContrato)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaRemisionesContratoPrefactura]", "@Contrato", idContrato);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        public DataTable DBGetDetalleRemisiones(string sId)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaRemisionesDetallePrefactura]", "@Remisiones", sId);  
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public int DBAsignarPrefacturaRemisiones(string sId,int iIdPrefactura)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spu_MXJ_ActualizaPrefacturaRemision]","@IdPrefactura",iIdPrefactura,
                                                                                                    "@Remisiones", sId);
                return oResult.S().I();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetServiciosCargo(string sId, decimal dTipoCambio)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaServicoCargoPrefactura]", "@Remisiones", sId,
                                                                                                    "@TipoCambioa", dTipoCambio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetServicioVuelo(string sId,decimal dTipoCambio)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaServicoVueloPrefactura]", "@Remisiones", sId,
                                                                                                    "@TipoCambioa", dTipoCambio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSavePrefacturaBasica(Prefactura objPref)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaPrefactura]",     "@IdContrato", objPref.iIdContrato,
                                                                                                 "@IdMonedaVuelo", objPref.iIdMonedaVuelo,
                                                                                                 "@CveFacturanteVuelo",objPref.sClaveFacturanteVuelo,
                                                                                                 "@IdMonedaServicioCargo", objPref.iIdMonedaSCC,
                                                                                                 "@CveFacturanteServicio", objPref.sClaveFacturanteSCC,
                                                                                                 "@SubTotalDLLV", objPref.dSubDllV,
                                                                                                 "@SubTotalMXNV", objPref.dSubMXNV,
                                                                                                 "@SubTotalHrsV", objPref.dSubHorasV,
                                                                                                 "@IVAV", objPref.dIVAV,
                                                                                                 "@IVADLLSV", objPref.dIVADllV,
                                                                                                 "@IVAMXNV", objPref.dIVAMXNV,
                                                                                                 "@TotalDLLSV", objPref.dTotalDllV,
                                                                                                 "@TotalMXNV",objPref.dTotalMXNV,
                                                                                                 "@SubTotalDLLSC",objPref.dSubDllC,
                                                                                                 "@SubTotalMXNC",objPref.dSubMXNC,
                                                                                                 "@IVAC",objPref.dIVAC,
                                                                                                 "@IVADLLSC", objPref.dIVADllC,
                                                                                                 "@IVAMXNC", objPref.dIVAMXNC,
                                                                                                 "@TotalDLLSC", objPref.dTotalDllC,
                                                                                                 "@TotalMXNC", objPref.dTotalMXNC,
                                                                                                 "@CobroVuelo", objPref.bCobroV,
                                                                                                 "@CobroServiciCargo", objPref.bCobroSCC,
                                                                                                 "@CobroAmbos",objPref.bCobroAmbos,
                                                                                                 "@IdFactura", objPref.sFacturaVuelo,
                                                                                                 "@IdFacturaSCC", objPref.sFacturaSCC,
                                                                                                 "@Porcentaje",objPref.iIdPorcentaje,
                                                                                                 "@TipoCambio", objPref.dTipoCambio,
                                                                                                 "@UnaSolaFactura", objPref.UnaFactura,
                                                                                                 "@Status",objPref.iStatus,
                                                                                                 "@UsuarioCreacion",objPref.sUsuarioCreacion,
                                                                                                 "@IP",objPref.sIP);

                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Prefactura), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se insertó el registro: " + oResult.S());
                return oResult.I();
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        public int DBUpdatePrefacturaBasica(Prefactura objPref, int iIdprefactura)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaPrefactura]",   "@IdPrefactura", iIdprefactura,
                                                                                                 "@IdContrato", objPref.iIdContrato,
                                                                                                 "@IdMonedaVuelo", objPref.iIdMonedaVuelo,
                                                                                                 "@CveFacturanteVuelo", objPref.sClaveFacturanteVuelo,
                                                                                                 "@IdMonedaServicioCargo", objPref.iIdMonedaSCC,
                                                                                                 "@CveFacturanteServicio", objPref.sClaveFacturanteSCC,
                                                                                                 "@SubTotalDLLV", objPref.dSubDllV,
                                                                                                 "@SubTotalMXNV", objPref.dSubMXNV,
                                                                                                 "@SubTotalHrsV", objPref.sSubHorasC,
                                                                                                 "@IVAV", objPref.dIVAV,
                                                                                                 "@IVADLLSV", objPref.dIVADllV,
                                                                                                 "@IVAMXNV", objPref.dIVAMXNV,
                                                                                                 "@TotalDLLSV", objPref.dTotalDllV,
                                                                                                 "@TotalMXNV", objPref.dTotalMXNV,
                                                                                                 "@SubTotalDLLSC", objPref.dSubDllC,
                                                                                                 "@SubTotalMXNC", objPref.dSubMXNC,
                                                                                                 "@IVAC", objPref.dIVAC,
                                                                                                 "@IVADLLSC", objPref.dIVADllC,
                                                                                                 "@IVAMXNC", objPref.dIVAMXNC,
                                                                                                 "@TotalDLLSC", objPref.dTotalDllC,
                                                                                                 "@TotalMXNC", objPref.dTotalMXNC,
                                                                                                 "@CobroVuelo", objPref.bCobroV,
                                                                                                 "@CobroServiciCargo", objPref.bCobroSCC,
                                                                                                 "@CobroAmbos", objPref.bCobroAmbos,
                                                                                                 "@IdFactura", objPref.sFacturaVuelo,
                                                                                                 "@IdFacturaSCC", objPref.sFacturaSCC,
                                                                                                 "@Porcentaje", objPref.iIdPorcentaje,
                                                                                                 "@UnaSolaFactura", objPref.UnaFactura,
                                                                                                 "@Status", objPref.iStatus,
                                                                                                 "@UsuarioModificacion", objPref.sUsuarioCreacion,
                                                                                                 "@IP", objPref.sIP);

                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Prefactura), Convert.ToInt32(Enumeraciones.TipoOperacion.Actualizar), "Se actualizó el registro: " + oResult.S());
                return oResult.I();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        } 
        public  void DBEliminaServicios(int idPrefactura)
        {
            try
            {
                oDB_SP.EjecutarValor("Principales.spD_MXJ_EliminaServiciosPrefactura", "@IdPrefactura", idPrefactura);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void DBSaveSV(DataTable dt, int iIDPrefactura)
        {
            try
            {
                
                int icont = 0;

                foreach (DataRow row in dt.Rows)
                {
                    icont += 1;
                    
                    oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaServicioVueloPrefactura]", "@IdPrefactura", iIDPrefactura,
                                                                                                    "@Cantidad", row["Cantidad"].S(),
                                                                                                    "@IdConcepto",row["IdConcepto"].S().I(),
                                                                                                    "@ImporteDll", row["ImporteDlls"].S().D(),
                                                                                                    "@IVADll", row["IVADll"].S().D(),
                                                                                                    "@TotallDll", row["TotalDLL"].S().D(),
                                                                                                    "@ImporteMXN", row["ImporteMXN"].S().D(),
                                                                                                    "@IVAMXN", row["IVAMXN"].S().D(),
                                                                                                    "@TotalMXN", row["TotalMXN"].S().D(),
                                                                                                    "@HrsDescontar", row["HRDescontar"].S(),
                                                                                                    "@Status", 1,
                                                                                                    "@UsuarioCreacion", Utils.GetUser,
                                                                                                    "@IP",Utils.GetIPAddress());
                }
               
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void DBSaveSCC(DataTable dt, int iIDPrefactura)
        {
            try
            {

                int icont = 0;

                foreach (DataRow row in dt.Rows)
                {
                    icont += 1;
                    oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaServicioCargoPrefactura]", "@IdPrefatura", iIDPrefactura,
                                                                                                    "@IdConcepto", row["IdServicioConCargo"].S().I(),
                                                                                                    "@ImporteDLLS", row["SubtotalUSD"].S().D(),
                                                                                                    "@IVADLLS", row["IVAUSD"].S().D(),
                                                                                                    "@TOTALDLLS", row["TotalUSD"].S().D(),
                                                                                                    "@ImporteMXN", row["SubtotalMXN"].S().D(),
                                                                                                    "@IVAMXN", row["IVAMXN"].S().D(),
                                                                                                    "@TotalMXN", row["TotalMXN"].S().D(),
                                                                                                    "@Status", 1,
                                                                                                    "@UsuarioCreacion", Utils.GetUser,
                                                                                                    "@IP", Utils.GetIPAddress());
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Prefactura GetPrefactura(int idPrefactura)
        {
            try
            {
                
                DataTable dtResult = oDB_SP.EjecutarDT("[Principales].[spS_MXJ_RecuperaPrefactura]", "@IdPrefactura", idPrefactura);
                Prefactura oPref = new Prefactura();
                foreach(DataRow dr in dtResult.Rows)
                {
                    oPref.iIdCliente = dr["IdCliente"].S().I();
                    oPref.iIdContrato = dr["IdContrato"].S().I();
                    oPref.iIdMonedaVuelo = dr["IdMonedaVuelo"].S().I();
                    oPref.sClaveFacturanteVuelo = dr["CveFacturanteVuelo"].S();
                    oPref.iIdMonedaSCC = dr["IdMonedaServicioCargo"].S().I();
                    oPref.sClaveFacturanteSCC = dr["CveFacturanteServicio"].S();
                    oPref.bCobroAmbos = dr["CobroAmbos"].S().B();
                    oPref.bCobroV = dr["CobroVuelo"].S().B();
                    oPref.bCobroSCC = dr["CobroServiciCargo"].S().B();
                    oPref.dTipoCambio = dr["TipoCambio"].S().D();
                    oPref.sFacturaVuelo = dr["IdFactuRa"].S();
                    oPref.sFacturaSCC = dr["IdFactuRaSCC"].S();
                    oPref.UnaFactura = dr["UnaFactura"].S().I();

                }

                if(ValidaEstadoFacturaCancelacion(oPref.sFacturaSCC))
                {
                    oPref.sFacturaSCC = string.Empty;
                }
                if (ValidaEstadoFacturaCancelacion(oPref.sFacturaVuelo))
                {
                    oPref.sFacturaVuelo = string.Empty;
                }

                return oPref;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetPRefacturasSeleccionadasyPendientes(int iIdPrefactura, int IdContrato)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_RecuperaRemisionesContratoPrefactura]", "@Contrato", IdContrato,
                                                                                                        "@IdPrefactura", iIdPrefactura);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetServiciosCargoPrefactura(int iIdPRefactura)
        { 
            try
            {
                // 
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_RecuperaServiciosCargoPrefactura]", "@IdPrefactura", iIdPRefactura);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        public DataTable GetServiciosVueloPrefactura(int iIdPRefactura)
        {
            try
            {
                //Principales.spS_MXJ_RecuperaServiciosVueloPrefactura 
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_RecuperaServiciosVueloPrefactura]", "@IdPrefactura", iIdPRefactura);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataSet GetInformacionFactura(int idPrefactura)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaInformacionParaFacturacion]", "@IdPrefactura", idPrefactura);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        public string GetNumFactura(string sClaveFacturante)
        {
            try
            {
                string sRespuestaWs = string.Empty;
                ALE_MexJet.wsSyteline.Iws_SyteLineClient wssyte = new wsSyteline.Iws_SyteLineClient();
                sRespuestaWs = wssyte.GetNumFactura(sClaveFacturante);
                return sRespuestaWs;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool ValidaFacturantes(string sClaveFacturante)
        {
            try
            {
                string sRespuestaWs = string.Empty;
                ALE_MexJet.wsSyteline.Iws_SyteLineClient wssyte = new wsSyteline.Iws_SyteLineClient();
                sRespuestaWs = wssyte.ValidaFacturante(sClaveFacturante);

                return sRespuestaWs.I() > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public string SaveArinV(FacturaPaso1 Paso)
        {
            try
            {
                
                string sRespuestaWs = string.Empty;
                wsSyteline.FacturaPaso1 Pasos = new wsSyteline.FacturaPaso1();
                ALE_MexJet.wsSyteline.Iws_SyteLineClient wssyte = new wsSyteline.Iws_SyteLineClient();
                Pasos.sFacturante = Paso.sFacturante;
                Pasos.sInvNum = Paso.sInvNum;
                Pasos.dtInvDate = Paso.dtInvDate.ToString("MM/dd/yyyy");
                Pasos.dtDueDate = Paso.dtDueDate.ToString("MM/dd/yyyy");
                Pasos.sRef = Paso.sRef;
                Pasos.sDescription = Paso.sDescription;
                Pasos.sAcct = Paso.sAcct;
                Pasos.dAmount = Paso.dAmount.S();
                Pasos.sTax_code1 = Paso.sTax_code1;
                Pasos.dSales_tax = Paso.dSales_tax.S();
                Pasos.dExch_rate = Paso.dExch_rate.S();
                Pasos.sAcct_unit3 = Paso.sAcct_unit3;
                Pasos.sAcct_unit4 = Paso.sAcct_unit4;
                Pasos.sUsuario = Paso.sUsuario;
                Pasos.Ufserie = Paso.Ufserie;
                Pasos.Ufruta = Paso.Ufruta;
                Pasos.UfModelo = Paso.UfModelo;
                Pasos.UfMarca = Paso.UfMarca;
                Pasos.Ufremision = Paso.Ufremision.S();
                Pasos.TipoFactura = Paso.TipoFactura;
                //Pasos.DtFechaRegreso = Paso.DtFechaRegreso;
                Paso.DtFechaSalida = Paso.DtFechaSalida;

                Pasos.sTipo = Paso.sTipo;
                Pasos.iEmpresa = Paso.iEmpresa;
                Pasos.sSucursal = Paso.sSucursal;
                Pasos.sSerie = Paso.sSerie;
                Pasos.sMoneda = Paso.sMoneda;

                Pasos.sMetodoPago = Paso.sMetodoPago;
                Pasos.sFormaPago = Paso.sFormaPago;
                Pasos.sUsoCFDI = Paso.sUsoCFDI;

                return wssyte.SaveArinV(Pasos);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public string SaveArinVD(FacturaPaso2 Paso)
        {
            try
            {
                string sRespuestaWs = string.Empty;
                wsSyteline.FacturaPaso2 Pasos = new wsSyteline.FacturaPaso2();
                Pasos.sFacturante = Paso.sFacturante;
                Pasos.sInvNum = Paso.sInvNum;
                Pasos.iDistSeq = Paso.iDistSeq.S();
                Pasos.sAcct = Paso.sAcct;
                Pasos.dAmount = Paso.dAmount.S();
                Pasos.sTax_code1 = Paso.sTax_code1;
                Pasos.iTaxSystem = Paso.iTaxSystem.S();
                Pasos.sAcct_unit1 = Paso.sAcct_unit1;
                Pasos.sAcct_unit2 = Paso.sAcct_unit2;
                Pasos.sAcct_unit3 = Paso.sAcct_unit3;
                Pasos.sAcct_unit4 = Paso.sAcct_unit4;
                Pasos.sUsuario = Paso.sUsuario;

                Pasos.sItem = Paso.sItem;
                Pasos.iEmpresa = Paso.iEmpresa;
                Pasos.iIdFactura = Paso.iIdFactura;
                Pasos.sConceptoUsuario = Paso.sConceptoUsuario;
                Pasos.iCantidad = Paso.iCantidad;
                Pasos.sCodigoBarras = Paso.sCodigoBarras;
                Pasos.sAlmacen = Paso.sAlmacen;
                Pasos.sProyecto = Paso.sProyecto;
                Pasos.sAcct_unit5 = Paso.sAcct_unit5;

                ALE_MexJet.wsSyteline.Iws_SyteLineClient wssyte = new wsSyteline.Iws_SyteLineClient();
                return wssyte.SaveArinVD(Pasos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string SaveConcepto(FacturaPaso3 Paso)
        {
            try
            {
                string sRespuestaWs = string.Empty;
                wsSyteline.FacturaPaso3 Pasos = new wsSyteline.FacturaPaso3();
                Pasos.sInvNum = Paso.sInvNum;
                Pasos.sdescription = Paso.sdescription;
                Pasos.iLine = Paso.iLine.S();
                Pasos.dQty = Paso.dQty.S();
                Pasos.sUM = Paso.sUM;
                Pasos.dPrice = Paso.dPrice.S();
                Pasos.iIsWorkflow = Paso.iIsWorkflow.S();
                Pasos.NoteExist = Paso.NoteExist.S();
                Pasos.sUsuario = Paso.sUsuario;

                ALE_MexJet.wsSyteline.Iws_SyteLineClient wssyte = new wsSyteline.Iws_SyteLineClient();
                return wssyte.SaveConceptoFacFinanciera(Pasos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool MasDeUnaFactura(int iIdContrato)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spS_MXJ_ConsultaTiposFacturas]", "@IdContrato", iIdContrato);
                bool bResult = oResult.S().B();
                return bResult; 
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool ValidaEstadoFacturaCancelacion(string sInvNum)
        {
            try
            {
                ALE_MexJet.wsSyteline.Iws_SyteLineClient wssyte = new wsSyteline.Iws_SyteLineClient();
                return wssyte.ValidaEstatusCancelacionFactura(sInvNum).I() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}