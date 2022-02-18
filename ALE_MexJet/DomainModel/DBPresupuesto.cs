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
    public class DBPresupuesto : DBBase
    {
        public DataTable DBViabilidad(params object[] oViabilidad)
        {
            try
            {
                return oDB_SP.EjecutarDT("Principales.SpS_MXJ_Viabilidades", ((object[])(((object[])(oViabilidad[0]))[0])));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtCliente
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
        public DataTable GetContratos(int idCliente)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_ConsultaContratosporCliente]", "@IdCliente", idCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable GetContactos(int idCliente)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaContactosCliente]", "@IdCliente", idCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetGrupoModelo(int IdContrato)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaGrupoModeloContrato]", "@IdContrato", IdContrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtGrupoModelos
        {
            get
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCatalogoGrupoModelo]");
            }
        }
        public DataTable GetAeropuertosOrigen(string sFiltroO, int iTipoFiltro)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaAeropuertosICAOyIATA]", "@Aeropuerto", sFiltroO + "%",
                                                                                                "@TipoFiltro", iTipoFiltro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetConsultaCobrosServiciosPresupuestos(string sXML)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaDatosCobrosServiciosPresupuestos]", "@doc", sXML);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetAeropuertosDestino(string sFiltroD, string iAeropuertoO, int iTipoFiltro)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaAeropuertoIATAeICAODestino]", "@Aeropuerto", sFiltroD + "%",
                                                                                                    "@IdOrigen", iAeropuertoO,
                                                                                                    "@TipoFiltro", iTipoFiltro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int GetIdAeropuertoPresupuestos(string sAeropuerto)
        {
            try
            {
                DataTable dt = oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaAeropuertoPresupuesto]", "@Aeropuerto", sAeropuerto);

                if (dt.Rows.Count > 0)
                    return dt.Rows[0][0].S().I();
                else
                    return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // INSERCION
        public void DBSetInsertaPresupuesto(Presupuesto oPres)
        {
            try
            {
                object oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaPresupuesto]", "@FechaPresupuesto", oPres.dtFechaPresupuesto,
                                                                                                    "@DiasVigencia", oPres.iDiasVigencia,
                                                                                                    "@IdContrato", oPres.iIdContrato,
                                                                                                    "@CompaniaImpresion", oPres.sCompaniaImpresion,
                                                                                                    "@IdSolicitante", oPres.iIdSolicitante,
                                                                                                    "@NombreSolicitante", oPres.sNombreSolicitante,
                                                                                                    "@Telefono", oPres.sTelefono,
                                                                                                    "@Email", oPres.sEmail,
                                                                                                    "@IdGrupoModeloSol", oPres.iIdGrupoModeloSol,
                                                                                                    "@MonedaPresupuesto", oPres.iIdMonedaPresupuesto,
                                                                                                    "@VueloNacional", oPres.dVueloNal,
                                                                                                    "@VueloInternacional", oPres.dVueloInt,
                                                                                                    "@EsperaNacional", oPres.dEsperaNal,
                                                                                                    "@EsperaInternacional", oPres.dEsperaInt,
                                                                                                    "@PernoctaNacional", oPres.dPernoctaNal,
                                                                                                    "@PernoctaInternacional", oPres.dPernoctaInt,
                                                                                                    "@IdSiglasAeropuerto", oPres.iIdSiglasAeropuerto,
                                                                                                    "@FactorIntercambio", oPres.bFactorIntercambio,
                                                                                                    "@FactorGiraEspera", oPres.bFactorGiraEspera,
                                                                                                    "@FactorGiraHorario", oPres.bFactorGiraHorario,
                                                                                                    "@FactorFechaPico", oPres.bFactorFechaPico,
                                                                                                    "@FactorTramoNal", oPres.dFactorTramoNal,
                                                                                                    "@FactorTramoInt", oPres.dFactorTramoInt,

                                                                                                    "@FactorIntercambioD", oPres.dFactorIntercambio,
                                                                                                    "@FactorGiraEsperaD", oPres.dGiraEspera,
                                                                                                    "@FactorGiraHorarioD", oPres.dGiraHorario,
                                                                                                    "@FactorFechaPicoD", oPres.dFactorFechaPico,
                                                                                                    "@FactorTramoNalD", oPres.dFactorTramoNal,
                                                                                                    "@FactorTramoIntD", oPres.dFactorTramoInt,

                                                                                                    "@Observaciones", oPres.sObservaciones,
                                                                                                    "@UsuarioCreacion", Utils.GetUser,
                                                                                                    "@IP", Utils.GetIPAddress());

                if (oResult != null)
                {
                    oPres.iIdPresupuesto = oResult.S().I();

                    DBSetInsertaTramosPresupuesto(oPres);
                    DBSetInsertaServiciosCargoPresupuesto(oPres);
                    DBSetInsertaConceptosPresupuesto(oPres);
                    DBSetInsertaConceptosPresupuestoDetail(oPres);

                    new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.CotizacionesVuelo), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se creó el presupuesto: " + oResult.S());
                }
                else
                    oPres.iIdPresupuesto = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DBSetInsertaTramosPresupuesto(Presupuesto oPres)
        {
            try
            {
                object oResult = new object();
                foreach (DataRow row in oPres.dtTramos.Rows)
                {
                   oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaTramosPresupuesto]", "@IdPresupuesto", oPres.iIdPresupuesto,
                                                                                            "@IdOrigen", row.S("IdOrigen").I(),
                                                                                            "@IdDestino", row.S("IdDestino").I(),
                                                                                            "@AeropuertoO", row.S("Origen"),
                                                                                            "@AeropuertoD", row.S("Destino"),
                                                                                            "@CantPax", row.S("CantPax").I(),
                                                                                            "@FechaSalida", row.S("FechaSalida").Dt(),
                                                                                            "@FechaLlegada", row.S("FechaLlegada").Dt(),
                                                                                            "@TiempoVueloReal", row.S("TiempoVuelo"),
                                                                                            "@TiempoEspera", row.S("TiempoEspera"),
                                                                                            "@TiempoCobrar", row.S("TiempoCobrar"),
                                                                                            "@RealVirtual", row.S("RealVirtual"),
                                                                                            "@SeCobra", row.S("SeCobra").I(),
                                                                                            "@UsuarioCreacion", Utils.GetUser,
                                                                                            "@IP", Utils.GetIPAddress());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DBSetInsertaServiciosCargoPresupuesto(Presupuesto oPres)
        {
            try
            {
                foreach (DataRow row in oPres.dtServicios.Rows)
                {
                    if (row.S("IdServicioConCargo") != "999999")
                    {
                        oDB_SP.EjecutarSP("[Principales].[spI_MXJ_ServiciosCargoPresupuesto]", "@IdPresupuesto", oPres.iIdPresupuesto,
                                                                                                "@IdServicioCargo", row.S("IdServicioConCargo").I(),
                                                                                                "@Importe", row.S("Importe").D(),
                                                                                                "@UsuarioCreacion", Utils.GetUser,
                                                                                                "@IP", Utils.GetIPAddress());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DBSetInsertaConceptosPresupuesto(Presupuesto oPres)
        {
            try
            {
                decimal dSubTotalSV = 0;
                decimal dPorDescSV = 0;
                decimal dImpDescSV = 0;
                decimal dTotalSV = 0;
                decimal dPorIvaSV = 0;
                decimal dImpIvaSV = 0;
                decimal dTotalSC = 0;
                decimal dPorIvaSC = 0;
                decimal dImpIvaSC = 0;
                decimal dTotalPres = 0;
                string sHrDescontar = string.Empty;

                decimal dCombustibleNal = 0;
                decimal dCombustibleInt = 0;

                foreach (DataRow row in oPres.dtConceptos.Rows)
                {
                    if (row.S("IdConcepto") == "1")
                    {
                        dCombustibleNal = row.S("CombustibleAumento").D();
                    }
                    if (row.S("IdConcepto") == "2")
                    {
                        dCombustibleInt = row.S("CombustibleAumento").D();
                    }
                    if (row.S("Concepto") == "SUBTOTAL SERVICIOS DE VUELO")
                    {
                        sHrDescontar = row.S("HrDescontar");
                        dSubTotalSV = row.S("Importe").D();
                    }
                    if (row.S("Concepto") == "% DESC SERVICIOS VUELO")
                    {
                        dPorDescSV = row.S("Unidad").D();
                        dImpDescSV = row.S("Importe").D();
                    }
                    if (row.S("Concepto") == "TOTAL SERVICIOS VUELO")
                        dTotalSV = row.S("Importe").D();
                    if (row.S("Concepto") == "IVA DE VUELO")
                    {
                        dPorIvaSV = row.S("Unidad").D();
                        dImpIvaSV = row.S("Importe").D();
                    }
                    if (row.S("Concepto") == "TOTAL SERVICIOS CON CARGO")
                        dTotalSC = row.S("Importe").D();
                    if (row.S("Concepto") == "IVA SERVICIOS CON CARGO")
                    {
                        dPorIvaSC = row.S("Unidad").D();
                        dImpIvaSC = row.S("Importe").D();
                    }
                    if (row.S("Concepto") == "TOTAL PRESUPUESTO")
                        dTotalPres = row.S("Importe").D();
                }

                oDB_SP.EjecutarSP("[Principales].[spI_MXJ_InsertaConceptosPresupuestos_H]", "@IdPresupuesto", oPres.iIdPresupuesto,
                                                                                            "@SubTotalSV", dSubTotalSV,
                                                                                            "@PorDescSV", dPorDescSV,
                                                                                            "@ImpDescSV", dImpDescSV,
                                                                                            "@TotalSV", dTotalSV,
                                                                                            "@PorIvaSV", dPorIvaSV,
                                                                                            "@ImpIvaSV", dImpIvaSV,
                                                                                            "@TotalSC", dTotalSC,
                                                                                            "@PorIvaSC", dPorIvaSC,
                                                                                            "@ImpIvaSC", dImpIvaSC,
                                                                                            "@TotalPres", dTotalPres,
                                                                                            "@TipoCambioPres", oPres.dTipoCambio,
                                                                                            "@HrDescontar", sHrDescontar,
                                                                                            "@UsuarioCreacion", Utils.GetUser,
                                                                                            "@IP", Utils.GetIPAddress());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DBSetInsertaConceptosPresupuestoDetail(Presupuesto oPres)
        {
            try
            {
                foreach (DataRow row in oPres.dtConceptos.Rows)
                {
                    if (row.S("IdConcepto") == "1" || row.S("IdConcepto") == "2" || row.S("IdConcepto") == "3" || row.S("IdConcepto") == "4" || row.S("IdConcepto") == "5" || row.S("IdConcepto") == "6")
                    {
                        oDB_SP.EjecutarSP("[Principales].[spI_MXJ_InsertaConceptosPresupuestos_D]", "@IdPresupuesto", oPres.iIdPresupuesto,
                                                                                                    "@IdConcepto", row.S("IdConcepto").I(),
                                                                                                    "@Cantidad", row.S("Cantidad"),
                                                                                                    "@Unidad", row.S("Unidad"),
                                                                                                    "@Importe", row.S("Importe").D(),
                                                                                                    "@HrsDescontar", row.S("HrDescontar"),
                                                                                                    "@CostoDirecto", row.S("CostoDirecto").D(),
                                                                                                    "@CostoComb", row.S("CostoComb").D(),
                                                                                                    "@CombustibleAumento", row.S("CombustibleAumento").D(),
                                                                                                    "@UsuarioCreacion", Utils.GetUser,
                                                                                                    "@IP", Utils.GetIPAddress());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // CONSULTA
        public Presupuesto DBGetConsultaPresupuestoId(int iIdPresupuesto)
        {
            try
            {
                Presupuesto oPre = null;
                DataSet dsPres = oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaPresupuestoId]", "@IdPresupuesto", iIdPresupuesto);

                if (dsPres.Tables.Count > 0)
                {
                    if (dsPres.Tables[0].Rows.Count > 0)
                    {
                        oPre = new Presupuesto();

                        DataRow r = dsPres.Tables[0].Rows[0];

                        oPre.iIdPresupuesto = r["IdPresupuesto"].S().I();
                        oPre.dtFechaPresupuesto = r["FechaPresupuesto"].S().Dt();
                        oPre.iDiasVigencia = r["DiasVigencia"].S().I();
                        oPre.iIdCliente = r["IdCliente"].S().I();
                        oPre.iIdContrato = r["IdContrato"].S().I();
                        oPre.sCompaniaImpresion = r["CompaniaImpresion"].S();
                        oPre.iIdSolicitante = r["IdSolicitante"].S().I();
                        oPre.sNombreSolicitante = r["NombreSolicitante"].S();
                        oPre.sTelefono = r["Telefono"].S();
                        oPre.sEmail = r["Email"].S();
                        oPre.iIdGrupoModeloSol = r["IdGrupoModeloSol"].S().I();
                        oPre.iIdMonedaPresupuesto = r["MonedaPresupuesto"].S().I();
                        oPre.dVueloNal = r["VueloNacional"].S().D();
                        oPre.dVueloInt = r["VueloInternacional"].S().D();
                        oPre.dEsperaNal = r["EsperaNacional"].S().D();
                        oPre.dEsperaInt = r["EsperaInternacional"].S().D();
                        oPre.dPernoctaNal = r["PernoctaNacional"].S().D();
                        oPre.dPernoctaInt = r["PernoctaInternacional"].S().D();
                        oPre.iIdSiglasAeropuerto = r["IdSiglasAeropuerto"].S().I();
                        oPre.bFactorIntercambio = r["FactorIntercambio"].S().B();
                        oPre.bFactorGiraEspera = r["FactorGiraEspera"].S().B();
                        oPre.bFactorGiraHorario = r["FactorGiraHorario"].S().B();
                        oPre.bFactorFechaPico = r["FactorFechaPico"].S().B();
                        oPre.bFactorTramoNal = r["FactorTramoNal"].S().B();
                        oPre.bFactorTramoInt = r["FactorTramoInt"].S().B();

                        oPre.dFactorIntercambio = r["FactorIntercambioD"].S().D();
                        oPre.dFactorFechaPico = r["FactorFechaPicoD"].S().D();
                        oPre.dGiraEspera = r["FactorGiraEsperaD"].S().D();
                        oPre.dGiraHorario = r["FactorGiraHorarioD"].S().D();
                        oPre.dFactorTramoNal = r["FactorTramoNalD"].S().D();
                        oPre.dFactorTramoInt = r["FactorTramoIntD"].S().D();

                        oPre.sObservaciones = r["Observaciones"].S();
                        oPre.iIdSolicitudVuelo = r["IdSolicitud"].S().I();
                    }

                    if (dsPres.Tables[4].Rows.Count > 0)
                    {
                        DataRow r = dsPres.Tables[4].Rows[0];

                        oPre.dSubTotalSV = r["SubTotalSV"].S().D();
                        oPre.dSubTotalSC = r["TotalSC"].S().D();
                    }

                    oPre.dtTramos = dsPres.Tables[1];
                    oPre.dtServicios = dsPres.Tables[2];
                    oPre.dtConceptos = dsPres.Tables[3];
                }

                return oPre;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // ACTUALIZACION
        public bool DBSetActualizaPresupuesto(Presupuesto oPres)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaPresupuesto]", "@IdPresupuesto", oPres.iIdPresupuesto,
                                                                                                          "@FechaPresupuesto", oPres.dtFechaPresupuesto,
                                                                                                          "@DiasVigencia", oPres.iDiasVigencia,
                                                                                                          "@IdContrato", oPres.iIdContrato,
                                                                                                          "@CompaniaImpresion", oPres.sCompaniaImpresion,
                                                                                                          "@IdSolicitante", oPres.iIdSolicitante,
                                                                                                          "@NombreSolicitante", oPres.sNombreSolicitante,
                                                                                                          "@Telefono", oPres.sTelefono,
                                                                                                          "@Email", oPres.sEmail,
                                                                                                          "@IdGrupoModeloSol", oPres.iIdGrupoModeloSol,
                                                                                                          "@MonedaPresupuesto", oPres.iIdMonedaPresupuesto,
                                                                                                          "@VueloNacional", oPres.dVueloNal,
                                                                                                          "@VueloInternacional", oPres.dVueloInt,
                                                                                                          "@EsperaNacional", oPres.dEsperaNal,
                                                                                                          "@EsperaInternacional", oPres.dEsperaInt,
                                                                                                          "@PernoctaNacional", oPres.dPernoctaNal,
                                                                                                          "@PernoctaInternacional", oPres.dPernoctaInt,
                                                                                                          "@IdSiglasAeropuerto", oPres.iIdSiglasAeropuerto,
                                                                                                          
                                                                                                          "@FactorIntercambio", oPres.bFactorIntercambio,
                                                                                                          "@FactorGiraEspera", oPres.bFactorGiraEspera,
                                                                                                          "@FactorGiraHorario", oPres.bFactorGiraHorario,
                                                                                                          "@FactorFechaPico", oPres.bFactorFechaPico,
                                                                                                          "@FactorTramoNal", oPres.bFactorTramoNal,
                                                                                                          "@FactorTramoInt", oPres.bFactorTramoInt,

                                                                                                          "@FactorIntercambioD", oPres.dFactorIntercambio,
                                                                                                          "@FactorGiraEsperaD", oPres.dGiraEspera,
                                                                                                          "@FactorGiraHorarioD", oPres.dGiraHorario,
                                                                                                          "@FactorFechaPicoD", oPres.dFactorFechaPico,
                                                                                                          "@FactorTramoNalD", oPres.dFactorTramoNal,
                                                                                                          "@FactorTramoIntD", oPres.dFactorTramoInt,
                                                                                                          
                                                                                                          "@Observaciones", oPres.sObservaciones,
                                                                                                          "@IdSolicitud", oPres.iIdSolicitudVuelo,
                                                                                                          "@UsuarioModificacion", Utils.GetUser,
                                                                                                          "@IP", Utils.GetIPAddress());


                DBSetEliminaTramosPresupuesto(oPres.iIdPresupuesto);
                DBSetInsertaTramosPresupuesto(oPres);
                DBSetEliminaServiciosPresupuesto(oPres.iIdPresupuesto);
                DBSetInsertaServiciosCargoPresupuesto(oPres);
                DBSetInsertaConceptosPresupuesto(oPres);
                DBSetInsertaConceptosPresupuestoDetail(oPres);

                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.CotizacionesVuelo), Convert.ToInt32(Enumeraciones.TipoOperacion.Actualizar), "Se modificó el presupuesto: " + oPres.iIdPresupuesto.S());

                if (oRes != null)
                    return oRes.S().I() > 0 ? true : false;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DBSetEliminaTramosPresupuesto(int iIdPresupuesto)
        {
            try
            {
                oDB_SP.EjecutarSP("[Principales].[spD_MXJ_EliminaTramosPresupueto]", "@IdPresupuesto", iIdPresupuesto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DBSetEliminaServiciosPresupuesto(int iIdPresupuesto)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[Principales].[spD_MXJ_EliminaServiciosPresupueso]", "@IdPresupuesto", iIdPresupuesto);

                if (oRes != null)
                    return oRes.S().I() > 0 ? true : false;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // SOLICITUD
        //public int DBInsertaSolicitud(SolicitudVuelo oSolicitud)
        //{
        //    try
        //    {
        //        object oResult;
        //        oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaSolicitudVuelo]", "@IdContrato", oSolicitud.iIdContrato,
        //                                                                            "@IdContacto", oSolicitud.iIdContacto,
        //                                                                            "@IdMotivo", oSolicitud.iIdMotivo,
        //                                                                            "@IdOrigen", oSolicitud.iIdOrigen,
        //                                                                            "@IdTipoEquipo", oSolicitud.iIdEquipo,
        //                                                                            "@NotasVuelo", oSolicitud.sNotasVuelo,
        //                                                                            "@Status", oSolicitud.iStatus,
        //                                                                            "@IP", oSolicitud.sIP,
        //                                                                            "@Usuario", oSolicitud.sUsuarioCreacion,
        //                                                                            "@IdSolicitud", oSolicitud.iIdSolicitud,
        //                                                                            "@Matricula", oSolicitud.sMatricula,
        //                                                                            "@Notas", oSolicitud.sNotaSolVuelo
        //                                                                            );

        //        new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Cliente.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se insertó el registro: " + oResult.S());

        //        return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public DataTable DBSaveTramo(TramoSolicitud oTramo)
        {
            try
            {
                DataTable oResult;
                oResult = oDB_SP.EjecutarDT("[Principales].[spI_MXJ_InsertaTramos]", "@IdSolicitud", oTramo.iIdSolicitud,
                                                                                    "@IdAeropuertoO", oTramo.iIdAeropuertoO,
                                                                                    "@IdAeropuertoD", oTramo.iIdAeropuertoD,
                                                                                    "@FechaVuelo", oTramo.dFechaVuelo.S().Dt(),
                                                                                    "@HoraVuelo", oTramo.sHoraVuelo,
                                                                                    "@Transportacion", oTramo.sTransportacion,
                                                                                    "@Status", oTramo.iStatus,
                                                                                    "@IP", oTramo.sIP,
                                                                                    "@UsuarioCreacion", oTramo.sUsuarioCreacion);


                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.SolicitudVuelo.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se insertó el registro: " + oResult.S());

                return oResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSaveSeguimiento(SolicitudVuelo oTramo)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaHistorico]", "@IdSolicitud", oTramo.iIdSolicitud,
                                                                                    "@idAutor", oTramo.iIdAutor,
                                                                                    "@Nota", oTramo.sNotasVuelo,
                                                                                    "@Status", oTramo.iStatus,
                                                                                    "@IP", oTramo.sIP,
                                                                                    "@Usuario", oTramo.sUsuarioCreacion);

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Cliente.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se insertó el registro: " + oResult.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBGurdaMonitorDespacho(SolicitudVuelo oSolV)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_MonitorDespacho]", "@IdSolicitud", oSolV.iIdSolicitud,
                                                                                        "@Dictamen", oSolV.idictamen,
                                                                                        "@OrigenSolicitud", "Mex Jet 360",
                                                                                        "@Usuario", oSolV.sUsuarioCreacion,
                                                                                        "@IP", oSolV.sIP);

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBGuardaSeguimiento(params object[] oGuardaSeguimiento)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaHistorico]", oGuardaSeguimiento
                                                                                    );

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetTramosSolicitudes(SolicitudVuelo oSol)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultarTramoSolicitud]", "@IdSolicitud", oSol.iIdSolicitud);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBEliminaTramoSol(int iIdTramo)
        {
            try
            {
                DataTable oResult;

                oResult = oDB_SP.EjecutarDT("[Principales].[spD_MXJ_EliminaTramoSol]", "@IdTramo", iIdTramo,
                                                                                            "@IP", "",
                                                                                            "@Usuario", Utils.GetUser);

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.CotizacionesVuelo.S().I(), Enumeraciones.TipoOperacion.Eliminar.S().I(), "Se eliminó el registro: " + oResult.S());

                return oResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetConsultaPresupuestoSolicitud(int iIdSolicitud)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaCotizacionSolicitud]", "@IdSolicitud", iIdSolicitud);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        


    }
}