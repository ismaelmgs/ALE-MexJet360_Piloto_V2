using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using DevExpress.Web.Data;
using ALE_MexJet.Clases;
using System.Data;

namespace ALE_MexJet.Presenter
{
    public class Prefactura_Presenter : BasePresenter<iViewPrefactura>
    {

        private readonly DBPrefactura oIGestCat;

        public Prefactura_Presenter(iViewPrefactura oView, DBPrefactura oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eGetFacturanteSV += GetFacturanteSV_Presenter;
            oIView.eGetFacturanteSC += GetFacturanteSC_Presenter;
            oIView.eGetContratos += GetContraros_Presenter;
            oIView.eGetRemisiones += GetRemisiones_PResenter;
            oIView.eGetDetalle += GetDetalleResmisiones;
            oIView.eGetServiciosCargo += GetServiciosCargo_Presenter;
            oIView.eGetServiciosVuelo += GetServiciosVuelo_Presenter;
            oIView.eSaveBasePrefactura += SavePrefacturaBasica_Presenter;
            oIView.eSaveServicos += SaveServicios_Presenter;
            oIView.eUpdateBasePrefactura += UpdatePrefacturaBasica_Presenter;
            oIView.eGetPrefacturas += RecuperaPRefactura_Presenter;
            oIView.eGetRecuperaRemisionesPrefactura += RecuperaRemisionesPrefactura_Presenter;
            oIView.eGetRecuperaPRefacturaServicios += RecuperaServicios_Presenter;
            oIView.eGetInformacionFactura += RecuperaInformacionfactura_Presenter;
            oIView.eValidaFacturante += ValidaExisteFacturante_Presenter;
            oIView.eGeneraFacturaSCC += GeneraFacturaSCC_Presenter;
            oIView.eGeneraFacturaVuelo += GeneraFacturaVuelo_Presenter;
            oIView.eVerificaPaquete += VerificaCantidadFacuturas;
            oIView.eGeneraUnaFactura += GeneraUnaSolaFactura_Presenter;
            oIView.eGetFacturantesSAP += eGetFacturantesSAP_Presenter;
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.dtClientes = oIGestCat.dtObjsCatCliente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void GetFacturanteSV_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.dtFacturanteSV = oIGestCat.DBGetFacturanteSV(sTipoOperacionSV, oIView.sClaveContrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void GetFacturanteSC_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.dtFacturanteSC = oIGestCat.DBGetFacturanteSC(sTipoOperacionSC, oIView.sClaveContrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void GetContraros_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.dtContrato = oIGestCat.DBGetContratoCliente(oIView.iIdCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void SavePrefacturaBasica_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.iIdPrefactura = oIGestCat.DBSavePrefacturaBasica(oIView.objPrefactura);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void UpdatePrefacturaBasica_Presenter(object sender, EventArgs e)
        {
            try
            {
                int iIdPrefactura = oIGestCat.DBUpdatePrefacturaBasica(oIView.objPrefactura, oIView.iIdPrefactura);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void GetRemisiones_PResenter(object sender, EventArgs e)
        {
            try
            {
                oIView.dtRemisiones = oIGestCat.DBGetRemisionesContrato(oIView.iIdContrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void GetDetalleResmisiones(object sender, EventArgs e)
        {
            try
            {
                int idFactura = oIGestCat.DBAsignarPrefacturaRemisiones(oIView.sRemisiones, oIView.iIdPrefactura);
                oIView.dtDetalleRemisiones = oIGestCat.DBGetDetalleRemisiones(oIView.sRemisiones);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string sTipoOperacionSC
        {
            get
            {
                string stipo = string.Empty;
                switch (oIView.iIdMonedaSC)
                {
                    case 1:
                        stipo = Enumeraciones.TipoMonedaSyteLine.CTEMXN.S();
                        break;
                    case 2:
                        stipo = Enumeraciones.TipoMonedaSyteLine.CTEUSD.S();
                        break;
                }
                return stipo;
            }
        }
        private string sTipoOperacionSV
        {
            get
            {
                string stipo = string.Empty;
                switch (oIView.iIdMonedaSV)
                {
                    case 1:
                        stipo = Enumeraciones.TipoMonedaSyteLine.CTEMXN.S();
                        break;
                    case 2:
                        stipo = Enumeraciones.TipoMonedaSyteLine.CTEUSD.S();
                        break;
                }
                return stipo;
            }
        }
        protected void GetServiciosVuelo_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.dtSV = oIGestCat.DBGetServicioVuelo(oIView.sRemisiones, oIView.TipoCambioPrefactura);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void GetServiciosCargo_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.dtSC = oIGestCat.DBGetServiciosCargo(oIView.sRemisiones, oIView.TipoCambioPrefactura);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void SaveServicios_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIGestCat.DBEliminaServicios(oIView.iIdPrefactura);
                oIGestCat.DBSaveSV(oIView.dtSV, oIView.iIdPrefactura);
                oIGestCat.DBSaveSCC(oIView.dtSC, oIView.iIdPrefactura);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void RecuperaPRefactura_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.objPrefactura = oIGestCat.GetPrefactura(oIView.iIdPrefactura);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void RecuperaRemisionesPrefactura_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.dtRemisiones = oIGestCat.GetPRefacturasSeleccionadasyPendientes(oIView.iIdPrefactura, oIView.iIdContrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void RecuperaServicios_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.dtSC = oIGestCat.GetServiciosCargoPrefactura(oIView.iIdPrefactura);
                oIView.dtSV = oIGestCat.GetServiciosVueloPrefactura(oIView.iIdPrefactura);
                if (oIView.dtSC.Rows.Count > 0 || oIView.dtSV.Rows.Count > 0)
                {
                    oIView.bActualizaFinal = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ValidaExisteFacturante_Presenter(object sender, EventArgs e)
        {
            try
            {
                Prefactura objPref = oIView.objPrefactura;
                oIView.bExisteFacturanteVuelo = oIGestCat.ValidaFacturantes(objPref.sClaveFacturanteVuelo);
                oIView.bExisteFacturanteSCC = oIGestCat.ValidaFacturantes(objPref.sClaveFacturanteSCC);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void RecuperaInformacionfactura_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.dsInformacionContrato = oIGestCat.GetInformacionFactura(oIView.iIdPrefactura);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void GeneraFacturaSCC_Presenter(object sender, EventArgs e)
        {
            try
            {
                #region CODIGO COMENTADO
                //int days_due = 0;
                //FacturaPaso1 objArinv = new FacturaPaso1();
                //Prefactura objPref = oIView.objPrefactura;
                //objArinv.sFacturante = objPref.sClaveFacturanteSCC;
                //DataRow[] drRows = oIView.dtFacturanteSC.Select("cust_num = '" + objArinv.sFacturante + "'");
                //if (drRows.Length > 0)
                //{
                //    days_due = drRows[0].ItemArray[2].S().I();
                //}
                //objArinv.sInvNum = oIGestCat.GetNumFactura(objArinv.sFacturante);
                //objArinv.dtInvDate = DateTime.Now.Date;
                //objArinv.dtDueDate = objArinv.dtInvDate.AddDays(days_due);

                //switch (oIView.iIdMonedaSC)
                //{
                //    case 1:
                //        objArinv.sAcct = Utils.GetParametrosClave("12").S();
                //        objArinv.dAmount = objPref.dSubMXNC;
                //        objArinv.dSales_tax = objPref.dIVAMXNC;
                //        objArinv.dExch_rate = 1;
                //        break;
                //    case 2:
                //        objArinv.sAcct = Utils.GetParametrosClave("13").S();
                //        objArinv.dAmount = objPref.dSubDllC;
                //        objArinv.dSales_tax = objPref.dIVADllC;
                //        objArinv.dExch_rate = oIView.TipoCambioPrefactura.D();
                //        break;
                //}
                //objArinv.sRef = string.Format("(SC) {0}", objArinv.sInvNum);
                //objArinv.sDescription = objArinv.sRef;

                //DataSet ds = oIView.dsInformacionContrato;
                //switch (ds.Tables[2].Rows[0]["FactorIVA"].S().I())
                //{
                //    case 16:
                //        objArinv.sTax_code1 = Utils.GetParametrosClave("15");
                //        break;
                //    case 4:
                //        objArinv.sTax_code1 = Utils.GetParametrosClave("16");
                //        break;
                //}

                //objArinv.sAcct_unit3 = ds.Tables[0].Rows[0]["AeropuertoIATA"].S();
                //objArinv.sAcct_unit4 = ds.Tables[0].Rows[0]["TipoClienteDescripcion"].S();
                //objArinv.TipoFactura = "SC";
                //objArinv.Ufremision = oIView.iIdPrefactura;
                ////string sResultArin = oIGestCat.SaveArinV(objArinv);
                //string sResultadoArinVD = string.Empty;


                //////ArinvD
                //FacturaPaso2 objArinvD = new FacturaPaso2();
                //int iNumeracionCincos = 5;
                //foreach (DataRow row in ds.Tables[2].Rows)
                //{
                //    if (row["SubTotal"].S().D() > 0)
                //    {
                //        objArinvD = new FacturaPaso2();
                //        objArinvD.sFacturante = objArinv.sFacturante;
                //        objArinvD.sInvNum = objArinv.sInvNum;
                //        objArinvD.iDistSeq = iNumeracionCincos;

                //        switch (row["FactorIVA"].S().I())
                //        {
                //            case 16:
                //                objArinvD.sTax_code1 = "NULL";
                //                break;
                //            case 4:
                //                objArinvD.sTax_code1 = "NULL";
                //                break;
                //        }

                //        switch (oIView.iIdMonedaSC)
                //        {
                //            case 1:
                //                objArinvD.dAmount = row["SubTotal"].S().D();
                //                break;
                //            case 2:
                //                objArinvD.dAmount = decimal.Round((row["SubTotal"].S().D() / oIView.TipoCambioPrefactura), 2, MidpointRounding.AwayFromZero);
                //                break;
                //        }
                //        objArinvD.iTaxSystem = 0;
                //        DataRow[] rows = ds.Tables[1].Select("IdRemision = " + row["IdRemision"].S());
                //        objArinvD.sAcct = row["cveCuenta"].S();
                //        objArinvD.sAcct_unit1 = row["CveCodigoUnidad1"].S();
                //        objArinvD.sAcct_unit2 = rows[0]["IdMatriculaInfo"].S();
                //        objArinvD.sAcct_unit3 = objArinv.sAcct_unit3;
                //        objArinvD.sAcct_unit4 = objArinv.sAcct_unit4;

                //        objArinvD.sUsuario = objArinv.sUsuario;

                //        //sResultadoArinVD = oIGestCat.SaveArinVD(objArinvD);

                //        iNumeracionCincos = iNumeracionCincos + 5;
                //    }
                //}


                ///// Registro de IVA
                //objArinvD = new FacturaPaso2();
                //objArinvD.sFacturante = objArinv.sFacturante;
                //objArinvD.sInvNum = objArinv.sInvNum;
                //objArinvD.iDistSeq = iNumeracionCincos;

                //switch (oIView.iIdMonedaSC)
                //{
                //    case 1:
                //        objArinvD.dAmount = objPref.dIVAMXNC;
                //        break;
                //    case 2:
                //        objArinvD.dAmount = objPref.dIVADllC;
                //        break;
                //}

                //switch (ds.Tables[2].Rows[0]["FactorIVA"].S().I())
                //{
                //    case 16:
                //        objArinvD.sTax_code1 = Utils.GetParametrosClave("15");
                //        objArinvD.sAcct = Utils.GetParametrosClave("17");
                //        break;
                //    case 4:
                //        objArinvD.sTax_code1 = Utils.GetParametrosClave("16");
                //        objArinvD.sAcct = Utils.GetParametrosClave("18");
                //        break;
                //}

                //objArinvD.iTaxSystem = 1;
                //objArinvD.sAcct_unit1 = "NA";
                //objArinvD.sAcct_unit2 = "NA";
                //objArinvD.sAcct_unit3 = objArinv.sAcct_unit3;
                //objArinvD.sAcct_unit4 = objArinv.sAcct_unit4;
                //objArinvD.sUsuario = objArinv.sUsuario;
                ////sResultadoArinVD = oIGestCat.SaveArinVD(objArinvD);

                //int iLineFacturacion = 1;

                //////Cim
                //string sResultadoCim = string.Empty;
                //FacturaPaso3 objCim = new FacturaPaso3();
                //objCim.sInvNum = objArinv.sInvNum;
                //objCim.iLine = iLineFacturacion;
                //objCim.dQty = 1;
                //objCim.sUM = "SRV";
                //objCim.sdescription = "SERVICIOS CON CARGO";
                //objCim.dPrice = 0m;
                //objCim.iIsWorkflow = 0;
                //objCim.NoteExist = 0;
                //objCim.sUsuario = objArinv.sUsuario;
                ////sResultadoCim = oIGestCat.SaveConcepto(objCim);
                //iLineFacturacion += 1;

                //foreach (DataRow rowRemision in ds.Tables[1].Rows)
                //{
                //    objCim = new FacturaPaso3();
                //    objCim.sInvNum = objArinv.sInvNum;
                //    objCim.iLine = iLineFacturacion;
                //    objCim.dQty = 1;
                //    objCim.sUM = "SRV";
                //    objCim.sdescription = string.Format("{0} - {1} : {2}", rowRemision["FechaSalida"].S().Dt().ToString("dd/MM/yyyy"), rowRemision["FechaLlegada"].S().Dt().ToString("dd/MM/yyyy"), rowRemision["Ruta"].S());
                //    objCim.dPrice = 0m;
                //    objCim.iIsWorkflow = 0;
                //    objCim.NoteExist = 0;
                //    objCim.sUsuario = objArinv.sUsuario;
                //    //sResultadoCim = oIGestCat.SaveConcepto(objCim);
                //    iLineFacturacion += 1;
                //    DataRow[] rowsServicios = ds.Tables[2].Select("IdRemision = " + rowRemision["IdRemision"].S());
                //    foreach (DataRow rowServicio in rowsServicios)
                //    {
                //        if (rowServicio["SubTotal"].S().D() > 0)
                //        {
                //            objCim = new FacturaPaso3();
                //            objCim.sInvNum = objArinv.sInvNum;
                //            objCim.iLine = iLineFacturacion;
                //            objCim.dQty = 1;
                //            objCim.sUM = "SRV";
                //            objCim.sdescription = rowServicio["ServicioConCargoDescripcion"].S();
                //            switch (oIView.iIdMonedaSC)
                //            {
                //                case 1:
                //                    objCim.dPrice = rowServicio["SubTotal"].S().D();
                //                    break;
                //                case 2:
                //                    objCim.dPrice = decimal.Round((rowServicio["SubTotal"].S().D() / objPref.dTipoCambio), 2, MidpointRounding.AwayFromZero);
                //                    break;
                //            }
                //            objCim.iIsWorkflow = 0;
                //            objCim.NoteExist = 0;
                //            objCim.sUsuario = objArinv.sUsuario;
                //            //sResultadoCim = oIGestCat.SaveConcepto(objCim);
                //            iLineFacturacion += 1;
                //        }
                //    }
                //}
                //oIView.sFacturaSCC = objArinv.sInvNum;
                #endregion

                InsertaHeader("SC", 2);
                InsertaDetalleSC("SERVICIOS CON CARGO");
                oIView.sFacturaSCC = "En proceso";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void GeneraFacturaVuelo_Presenter(object sender, EventArgs e)
        {
            try
            {
                #region CODIGO COMENTADO

                //int days_due = 0;
                //FacturaPaso1 objArinv = new FacturaPaso1();
                //Prefactura objPref = oIView.objPrefactura;
                //objArinv.sFacturante = objPref.sClaveFacturanteVuelo;
                //DataRow[] drRows = oIView.dtFacturanteSV.Select("cust_num = '" + objArinv.sFacturante + "'");
                //if (drRows.Length > 0)
                //{
                //    days_due = drRows[0].ItemArray[2].S().I();
                //}
                //objArinv.sInvNum = oIGestCat.GetNumFactura(objArinv.sFacturante);
                //objArinv.dtInvDate = DateTime.Now.Date;
                //objArinv.dtDueDate = objArinv.dtInvDate.AddDays(days_due);

                //switch (oIView.iIdMonedaSV)
                //{
                //    case 1:
                //        objArinv.sAcct = Utils.GetParametrosClave("12").S();
                //        objArinv.dAmount = objPref.dSubMXNV;
                //        objArinv.dSales_tax = objPref.dIVAMXNV;
                //        objArinv.dExch_rate = 1;
                //        break;
                //    case 2:
                //        objArinv.sAcct = Utils.GetParametrosClave("13").S();
                //        objArinv.dAmount = objPref.dSubDllV;
                //        objArinv.dSales_tax = objPref.dIVADllV;
                //        objArinv.dExch_rate = oIView.TipoCambioPrefactura;
                //        break;
                //}

                //objArinv.sRef = string.Format("(FV) {0}", objArinv.sInvNum);
                //objArinv.sDescription = objArinv.sRef;

                //DataSet ds = oIView.dsInformacionContrato;
                //switch (ds.Tables[5].Rows[0]["FactorIVA"].S().I())
                //{
                //    case 16:
                //        objArinv.sTax_code1 = Utils.GetParametrosClave("15");
                //        break;
                //    case 4:
                //        objArinv.sTax_code1 = Utils.GetParametrosClave("16");
                //        break;
                //}

                //objArinv.sAcct_unit3 = ds.Tables[0].Rows[0]["AeropuertoIATA"].S();
                //objArinv.sAcct_unit4 = ds.Tables[0].Rows[0]["TipoClienteDescripcion"].S();
                //objArinv.TipoFactura = "FV";
                //objArinv.Ufremision = oIView.iIdPrefactura;
                //string sResultArin = oIGestCat.SaveArinV(objArinv);


                //////ArinVD
                //string sResultadoArinVD = string.Empty;
                //FacturaPaso2 objArinvD = new FacturaPaso2();
                //int iNumeracionCincos = 5;

                //foreach (DataRow row in ds.Tables[1].Rows)
                //{
                //    objArinvD = new FacturaPaso2();
                //    objArinvD.sFacturante = objArinv.sFacturante;
                //    objArinvD.sInvNum = objArinv.sInvNum;
                //    objArinvD.iDistSeq = iNumeracionCincos;

                //    switch (oIView.iIdMonedaSV)
                //    {
                //        case 1:
                //            objArinvD.dAmount = row["SubtotalServiciosVuelo"].S().D() * oIView.TipoCambioPrefactura;
                //            break;
                //        case 2:
                //            objArinvD.dAmount = row["SubtotalServiciosVuelo"].S().D();
                //            break;
                //    }
                //    objArinvD.dAmount = decimal.Round(objArinvD.dAmount, 2, MidpointRounding.AwayFromZero);
                //    objArinvD.iTaxSystem = 0;
                //    DataRow[] rowsserviciovuelo = ds.Tables[3].Select("IdRemision = " + row["IdRemision"].S());
                //    objArinvD.sAcct = rowsserviciovuelo[0]["ClaveCuenta"].S();
                //    objArinvD.sAcct_unit1 = "NA";
                //    objArinvD.sAcct_unit2 = row["IdMatriculaInfo"].S();
                //    objArinvD.sAcct_unit3 = objArinv.sAcct_unit3;
                //    objArinvD.sAcct_unit4 = objArinv.sAcct_unit4;
                //    objArinvD.sUsuario = objArinv.sUsuario;
                //    sResultadoArinVD = oIGestCat.SaveArinVD(objArinvD);
                //    iNumeracionCincos = iNumeracionCincos + 5;
                //}


                /////REGISTRO DE IVA
                //objArinvD = new FacturaPaso2();
                //objArinvD.sFacturante = objArinv.sFacturante;
                //objArinvD.sInvNum = objArinv.sInvNum;
                //objArinvD.iDistSeq = iNumeracionCincos;

                //switch (oIView.iIdMonedaSV)
                //{
                //    case 1:
                //        objArinvD.dAmount = objPref.dIVAMXNV;
                //        break;
                //    case 2:
                //        objArinvD.dAmount = objPref.dIVADllV;
                //        break;
                //}

                //switch (ds.Tables[5].Rows[0]["FactorIVA"].S().I())
                //{
                //    case 16:
                //        objArinvD.sTax_code1 = Utils.GetParametrosClave("15");
                //        objArinvD.sAcct = Utils.GetParametrosClave("17");
                //        break;
                //    case 4:
                //        objArinvD.sTax_code1 = Utils.GetParametrosClave("16");
                //        objArinvD.sAcct = Utils.GetParametrosClave("18");
                //        break;
                //}

                //objArinvD.iTaxSystem = 1;
                //objArinvD.sAcct_unit1 = "NA";
                //objArinvD.sAcct_unit2 = "NA";
                //objArinvD.sAcct_unit3 = objArinv.sAcct_unit3;
                //objArinvD.sAcct_unit4 = objArinv.sAcct_unit4;
                //objArinvD.sUsuario = objArinv.sUsuario;
                //sResultadoArinVD = oIGestCat.SaveArinVD(objArinvD);

                //int iLineFacturacion = 1;

                //////Cim
                //string sResultadoCim = string.Empty;
                //FacturaPaso3 objCim = new FacturaPaso3();
                //objCim.sInvNum = objArinv.sInvNum;
                //objCim.iLine = iLineFacturacion;
                //objCim.dQty = 1;
                //objCim.sUM = "SRV";
                //objCim.sdescription = ds.Tables[3].Rows[0]["Descripcion"].S();
                //objCim.dPrice = 0m;
                //objCim.iIsWorkflow = 0;
                //objCim.NoteExist = 0;
                //objCim.sUsuario = objArinv.sUsuario;
                //sResultadoCim = oIGestCat.SaveConcepto(objCim);
                //iLineFacturacion += 1;

                //foreach (DataRow rowRemision in ds.Tables[1].Rows)
                //{
                //    objCim = new FacturaPaso3();
                //    objCim.sInvNum = objArinv.sInvNum;
                //    objCim.iLine = iLineFacturacion;
                //    objCim.dQty = 1;
                //    objCim.sUM = "SRV";
                //    objCim.sdescription = string.Format("{0} - {1} : {2}", rowRemision["FechaSalida"].S().Dt().ToString("dd/MM/yyyy"), rowRemision["FechaLlegada"].S().Dt().ToString("dd/MM/yyyy"), rowRemision["Ruta"].S());

                //    switch (oIView.iIdMonedaSV)
                //    {
                //        case 1:
                //            objCim.dPrice = rowRemision["SubtotalServiciosVuelo"].S().D() / objPref.dTipoCambio;
                //            break;
                //        case 2:
                //            objCim.dPrice = rowRemision["SubtotalServiciosVuelo"].S().D();
                //            break;
                //    }

                //    objCim.iIsWorkflow = 0;
                //    objCim.NoteExist = 0;
                //    objCim.sUsuario = objArinv.sUsuario;
                //    sResultadoCim = oIGestCat.SaveConcepto(objCim);
                //    iLineFacturacion += 1;
                //}
                //oIView.sFacturaVuelo = objArinv.sInvNum;
                #endregion

                InsertaHeader("FV", 1);
                InsertaDetalleSV("SERVICIOS PROPORCIONADOS DE VUELO");
                oIView.sFacturaVuelo = "En proceso";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void GeneraUnaSolaFactura_Presenter(object sender, EventArgs e)
        {
            try
            {
                #region COMENTADO
                //#region HEADER FACTURA (ArinV)
                //int days_due = 0;
                //FacturaPaso1 objArinv = new FacturaPaso1();
                //Prefactura objPref = oIView.objPrefactura;
                //objArinv.sFacturante = objPref.sClaveFacturanteSCC;
                //DataRow[] drRows = oIView.dtFacturanteSC.Select("cust_num = '" + objArinv.sFacturante + "'");
                //if (drRows.Length > 0)
                //{
                //    days_due = drRows[0].ItemArray[2].S().I();
                //}
                //objArinv.sInvNum = oIGestCat.GetNumFactura(objArinv.sFacturante);
                //objArinv.dtInvDate = DateTime.Now.Date;
                //objArinv.dtDueDate = objArinv.dtInvDate.AddDays(days_due);

                //switch (oIView.iIdMonedaSV)
                //{
                //    case 1:
                //        objArinv.sAcct = Utils.GetParametrosClave("12").S();
                //        objArinv.dAmount = (objPref.dSubMXNV + objPref.dDescMXNV) + objPref.dSubMXNC;
                //        objArinv.dSales_tax = objPref.dIVAMXNV + objPref.dIVAMXNC;
                //        objArinv.dExch_rate = 1;
                //        break;
                //    case 2:
                //        objArinv.sAcct = Utils.GetParametrosClave("13").S();
                //        objArinv.dAmount = (objPref.dSubDllV + objPref.dDescDllV) + objPref.dSubDllC;
                //        objArinv.dSales_tax = objPref.dIVADllV + objPref.dIVADllC;
                //        objArinv.dExch_rate = oIView.TipoCambioPrefactura;
                //        break;
                //}

                //objArinv.sRef = string.Format("(FV) {0}", objArinv.sInvNum);
                //objArinv.sDescription = objArinv.sRef;

                //DataSet ds = oIView.dsInformacionContrato;
                //switch (ds.Tables[5].Rows[0]["FactorIVA"].S().I())
                //{
                //    case 16:
                //        objArinv.sTax_code1 = Utils.GetParametrosClave("15");
                //        break;
                //    case 4:
                //        objArinv.sTax_code1 = Utils.GetParametrosClave("16");
                //        break;
                //}

                //objArinv.sAcct_unit3 = ds.Tables[0].Rows[0]["AeropuertoIATA"].S();
                //objArinv.sAcct_unit4 = ds.Tables[0].Rows[0]["TipoClienteDescripcion"].S();
                //objArinv.TipoFactura = "FV";
                //objArinv.Ufremision = oIView.iIdPrefactura;
                //objArinv.Ufruta = ds.Tables[1].Rows[0]["Ruta"].S();
                //objArinv.Ufserie = ds.Tables[1].Rows[0]["Serie"].S();

                //if(oIView.bUnaFactura)
                //{
                //    DataSet dsPref = oIGestCat.GetInformacionFactura(oIView.iIdPrefactura);
                //    if (ds.Tables[1].Rows.Count > 0)
                //    {
                //        objArinv.DtFechaSalida = ds.Tables[1].Rows[0]["FechaSalida"].S().Dt();
                //        objArinv.DtFechaRegreso = ds.Tables[1].Rows[0]["FechaLlegada"].S().Dt();
                //    }
                //}

                //string sResultArin = oIGestCat.SaveArinV(objArinv);
                //#endregion

                //#region FACTURA DE VUELO (ArinVD)
                //////ArinVD Factura Vuelo
                //string sResultadoArinVD = string.Empty;
                //FacturaPaso2 objArinvD = new FacturaPaso2();
                //int iNumeracionCincos = 5;

                //foreach (DataRow row in ds.Tables[1].Rows)
                //{
                //    objArinvD = new FacturaPaso2();
                //    objArinvD.sFacturante = objArinv.sFacturante;
                //    objArinvD.sInvNum = objArinv.sInvNum;
                //    objArinvD.iDistSeq = iNumeracionCincos;

                //    switch (oIView.iIdMonedaSV)
                //    {
                //        case 1:
                //            objArinvD.dAmount = row["SubtotalServiciosVuelo"].S().D() * oIView.TipoCambioPrefactura;
                //            break;
                //        case 2:
                //            objArinvD.dAmount = row["SubtotalServiciosVuelo"].S().D();
                //            break;
                //    }
                //    objArinvD.dAmount = decimal.Round(objArinvD.dAmount, 2, MidpointRounding.AwayFromZero);
                //    objArinvD.iTaxSystem = 0;
                //    DataRow[] rowsserviciovuelo = ds.Tables[3].Select("IdRemision = " + row["IdRemision"].S());
                //    objArinvD.sAcct = rowsserviciovuelo[0]["ClaveCuenta"].S();
                //    objArinvD.sAcct_unit1 = "NA";
                //    objArinvD.sAcct_unit2 = row["IdMatriculaInfo"].S();
                //    objArinvD.sAcct_unit3 = objArinv.sAcct_unit3;
                //    objArinvD.sAcct_unit4 = objArinv.sAcct_unit4;
                //    objArinvD.sUsuario = objArinv.sUsuario;
                //    sResultadoArinVD = oIGestCat.SaveArinVD(objArinvD);
                //    iNumeracionCincos = iNumeracionCincos + 5;
                //}
                //#endregion

                //#region FACTURA DE VUELO DESCUENTO (ArinVD)
                //////ArinVD Factura Vuelo
                //string sResArinvD = string.Empty;
                //FacturaPaso2 objAVDesc = new FacturaPaso2();

                //foreach (DataRow row in ds.Tables[1].Rows)
                //{
                //    if (row.S("DescServiciosVuelo").D() > 0)
                //    {
                //        objAVDesc = new FacturaPaso2();
                //        objAVDesc.sFacturante = objArinv.sFacturante;
                //        objAVDesc.sInvNum = objArinv.sInvNum;
                //        objAVDesc.iDistSeq = 15;

                //        switch (oIView.iIdMonedaSV)
                //        {
                //            case 1:
                //                objAVDesc.dAmount = row["DescServiciosVuelo"].S().D() * oIView.TipoCambioPrefactura;
                //                break;
                //            case 2:
                //                objAVDesc.dAmount = row["DescServiciosVuelo"].S().D();
                //                break;
                //        }

                //        objAVDesc.dAmount = decimal.Round(objAVDesc.dAmount, 2, MidpointRounding.AwayFromZero) * (-1);
                //        objAVDesc.iTaxSystem = 0;
                //        switch (ds.Tables[5].Rows[0]["FactorIVA"].S().I())
                //        {
                //            case 16:
                //                objAVDesc.sAcct = "702310";
                //                break;
                //            case 4:
                //                objAVDesc.sAcct = "702320";
                //                break;
                //        }

                //        objAVDesc.sAcct_unit1 = null;
                //        objAVDesc.sAcct_unit2 = null;
                //        objAVDesc.sAcct_unit3 = objArinv.sAcct_unit3;
                //        objAVDesc.sAcct_unit4 = objArinv.sAcct_unit4;
                //        objAVDesc.sUsuario = objArinv.sUsuario;
                //        sResultadoArinVD = oIGestCat.SaveArinVD(objAVDesc);
                //    }
                //}
                //#endregion

                //#region FACTURAS DE SERVICIOS CON CARGO (ArinVD)
                //////ArinvD Factura SCC
                //objArinvD = new FacturaPaso2();
                //foreach (DataRow row in ds.Tables[2].Rows)
                //{
                //    if (row["SubTotal"].S().D() > 0)
                //    {
                //        objArinvD = new FacturaPaso2();
                //        objArinvD.sFacturante = objArinv.sFacturante;
                //        objArinvD.sInvNum = objArinv.sInvNum;
                //        objArinvD.iDistSeq = iNumeracionCincos;

                //        switch (row["FactorIVA"].S().I())
                //        {
                //            case 16:
                //                objArinvD.sTax_code1 = "NULL";
                //                break;
                //            case 4:
                //                objArinvD.sTax_code1 = "NULL";
                //                break;
                //        }

                //        switch (oIView.iIdMonedaSC)
                //        {
                //            case 1:
                //                objArinvD.dAmount = row["SubTotal"].S().D();
                //                break;
                //            case 2:
                //                objArinvD.dAmount = decimal.Round((row["SubTotal"].S().D() / oIView.TipoCambioPrefactura), 2, MidpointRounding.AwayFromZero);
                //                break;
                //        }
                //        objArinvD.iTaxSystem = 0;
                //        objArinvD.dAmount = decimal.Round(objArinvD.dAmount, 2, MidpointRounding.AwayFromZero);
                //        DataRow[] rows = ds.Tables[1].Select("IdRemision = " + row["IdRemision"].S());
                //        objArinvD.sAcct = row["cveCuenta"].S();
                //        objArinvD.sAcct_unit1 = row["CveCodigoUnidad1"].S();
                //        objArinvD.sAcct_unit2 = rows[0]["IdMatriculaInfo"].S();
                //        objArinvD.sAcct_unit3 = objArinv.sAcct_unit3;
                //        objArinvD.sAcct_unit4 = objArinv.sAcct_unit4;

                //        objArinvD.sUsuario = objArinv.sUsuario;

                //        sResultadoArinVD = oIGestCat.SaveArinVD(objArinvD);

                //        iNumeracionCincos = iNumeracionCincos + 5;
                //    }
                //}
                //#endregion

                //#region CONCEPTOS DE FACTURACION (Conceptos)
                //objArinvD = new FacturaPaso2();
                //objArinvD.sFacturante = objArinv.sFacturante;
                //objArinvD.sInvNum = objArinv.sInvNum;
                //objArinvD.iDistSeq = iNumeracionCincos;

                //switch (oIView.iIdMonedaSC)
                //{
                //    case 1:
                //        objArinvD.dAmount = objPref.dIVAMXNC + objPref.dIVAMXNV;
                //        break;
                //    case 2:
                //        objArinvD.dAmount = objPref.dIVADllC + objPref.dIVADllV;
                //        break;
                //}

                //switch (ds.Tables[5].Rows[0]["FactorIVA"].S().I())
                //{
                //    case 16:
                //        objArinvD.sTax_code1 = Utils.GetParametrosClave("15");
                //        objArinvD.sAcct = Utils.GetParametrosClave("17");
                //        break;
                //    case 4:
                //        objArinvD.sTax_code1 = Utils.GetParametrosClave("16");
                //        objArinvD.sAcct = Utils.GetParametrosClave("18");
                //        break;
                //}

                //objArinvD.iTaxSystem = 1;
                //objArinvD.sAcct_unit1 = "NA";
                //objArinvD.sAcct_unit2 = "NA";
                //objArinvD.sAcct_unit3 = objArinv.sAcct_unit3;
                //objArinvD.sAcct_unit4 = objArinv.sAcct_unit4;
                //objArinvD.sUsuario = objArinv.sUsuario;
                //sResultadoArinVD = oIGestCat.SaveArinVD(objArinvD);


                //int iLineFacturacion = 1;
                //string sResultadoCim = string.Empty;
                //FacturaPaso3 objCim = new FacturaPaso3();
                //foreach (DataRow row in oIView.dtSV.Rows)
                //{
                //    if (row["ImporteDlls"].S().D() > 0)
                //    {
                //        objCim = new FacturaPaso3();
                //        objCim.sInvNum = objArinv.sInvNum;
                //        objCim.iLine = iLineFacturacion;

                //        DataRow[] rowServicioVuelo = ds.Tables[6].Select("IdConcepto = " + row["IdConcepto"].S().I());

                //        switch (row["IdConcepto"].S().I())
                //        {


                //            case 1:
                //                objCim.dQty = Utils.ConvierteTiempoaDecimal(row["Cantidad"].S()).S().D();
                //                objCim.sUM = "HRS";
                //                objCim.sdescription = string.Format("{0}. {1} {2}", row["Descripcion"].S().ToUpper(), "TIEMPO DE VUELO:", row["Cantidad"].S());
                //                break;
                //            case 2:
                //                objCim.dQty = Utils.ConvierteTiempoaDecimal(row["Cantidad"].S()).S().D();
                //                objCim.sUM = "HRS";
                //                objCim.sdescription = string.Format("{0}. {1} {2}", row["Descripcion"].S().ToUpper(), "TIEMPO DE VUELO:", row["Cantidad"].S());
                //                break;
                //            case 3:
                //                objCim.dQty = row["Cantidad"].S().D();
                //                objCim.sUM = "SRV";
                //                objCim.sdescription = row["Descripcion"].S().ToUpper();
                //                break;
                //            case 4:
                //                objCim.dQty = row["Cantidad"].S().D();
                //                objCim.sUM = "SRV";
                //                objCim.sdescription = row["Descripcion"].S().ToUpper();
                //                break;
                //            case 5:
                //                objCim.dQty = row["Cantidad"].S().D();
                //                objCim.sUM = "SRV";
                //                objCim.sdescription = row["Descripcion"].S().ToUpper();
                //                break;
                //            case 6:
                //                objCim.dQty = row["Cantidad"].S().D();
                //                objCim.sUM = "SRV";
                //                objCim.sdescription = row["Descripcion"].S().ToUpper();
                //                break;
                //        }
                //        switch (oIView.iIdMonedaSV)
                //        {
                //            case 1:
                //                objCim.dPrice = decimal.Round((rowServicioVuelo[0]["TarifaDlls"].S().D() * objPref.dTipoCambio), 2, MidpointRounding.AwayFromZero);
                //                break;
                //            case 2:
                //                objCim.dPrice = rowServicioVuelo[0]["TarifaDlls"].S().D();
                //                break;
                //        }
                //        objCim.iIsWorkflow = 0;
                //        objCim.NoteExist = 0;
                //        objCim.sUsuario = objArinv.sUsuario;
                //        sResultadoCim = oIGestCat.SaveConcepto(objCim);
                //        iLineFacturacion += 1;
                //    }
                //}

                //// CONCEPTOS DE SERVICIOS CON CARGO
                //foreach (DataRow rowSCC in oIView.dtSC.Rows)
                //{
                //    if (rowSCC["SubtotalMXN"].S().D() > 0)
                //    {
                //        objCim = new FacturaPaso3();
                //        objCim.sInvNum = objArinv.sInvNum;
                //        objCim.iLine = iLineFacturacion;
                //        objCim.dQty = 1;
                //        objCim.sUM = "SRV";
                //        objCim.sdescription = rowSCC["ServicioConCargoDescripcion"].S();
                //        switch (objPref.iIdMonedaSCC)
                //        {
                //            case 1:
                //                objCim.dPrice = rowSCC["SubtotalMXN"].S().D();
                //                break;
                //            case 2:
                //                objCim.dPrice = rowSCC["SubtotalUSD"].S().D();
                //                break;
                //        }
                //        objCim.iIsWorkflow = 0;
                //        objCim.NoteExist = 0;
                //        objCim.sUsuario = objArinv.sUsuario;
                //        sResultadoCim = oIGestCat.SaveConcepto(objCim);
                //        iLineFacturacion += 1;
                //    }

                //}
                //#endregion

                //#region CONCEPTO DE DESCUENTO

                //foreach (DataRow row in ds.Tables[1].Rows)
                //{
                //    if (row.S("DescServiciosVuelo").D() > 0)
                //    {
                //        objCim = new FacturaPaso3();
                //        objCim.sInvNum = objArinv.sInvNum;
                //        objCim.iLine = iLineFacturacion;

                //        objCim.dQty = 1;
                //        objCim.sUM = "SRV";
                //        objCim.sdescription = "DESCUENTO";

                //        switch (oIView.iIdMonedaSV)
                //        {
                //            case 1:
                //                objCim.dPrice = decimal.Round(((row.S("DescServiciosVuelo").D() * objPref.dTipoCambio) * (-1)), 2, MidpointRounding.AwayFromZero);
                //                break;
                //            case 2:
                //                objCim.dPrice = row.S("DescServiciosVuelo").D() * (-1);
                //                break;
                //        }
                //        objCim.iIsWorkflow = 0;
                //        objCim.NoteExist = 0;
                //        objCim.sUsuario = objArinv.sUsuario;
                //        sResultadoCim = oIGestCat.SaveConcepto(objCim);
                //    }
                //}
                //#endregion

                //oIView.sFacturaVuelo = objArinv.sInvNum;
                //oIView.sFacturaSCC = objArinv.sInvNum;
                #endregion
                InsertaHeader("FV", 1);

                /*
                 * ds.Tables[0] --> Base Predeterminada y Tipo de Cliente
                 * ds.Tables[1] --> Remisiones incluidas en la prefactura
                 * ds.Tables[2] --> Servicios con cargo agrupados por remision
                 * ds.Tables[3] --> Remisión, Cuenta y Descripcion
                 * ds.Tables[4] --> Clave de la cuenta
                 * ds.Tables[5] --> Remisión con su factor de iva
                 * ds.Tables[6] --> IdConcepto y su tarifa en Dlls (Servicios de Vuelo)
                 */

                string sResultadoArinVD = string.Empty;
                FacturaPaso2 objArinvD = new FacturaPaso2();
                DataSet ds = oIView.dsInformacionContrato;

                int iEmpresa = 1;
                string sCodigoBarras = string.Empty;
                int iCantidad = 1;
                int iFolioFactura = (oIView.iIdPrefactura.S() + "1").S().I();
                string sProyecto = Utils.ObtieneParametroPorClave("110");
                string sItemSinCargo = Utils.ObtieneParametroPorClave("111");

                int iNumeracionLinea = 1;

                //FacturaSC oH = new FacturaSC();
                //oH.iIdPrefactura = iFolioFactura;
                //oH.iEmpresa = iEmpresa;
                //oH.sItem = sItemSinCargo;
                //oH.sConceptoUsuario = ds.Tables[3].Rows[0]["Descripcion"].S(); ;
                //oH.sCodigoBarras = sCodigoBarras;
                //oH.iCantidad = iCantidad;
                //oH.iNumeroLinea = iNumeracionLinea;
                //oH.iIdMoneda = 1;
                //oH.dSubtotal = 0;
                //oH.dTipoCambioPrefactura = 0;
                //oH.iFactorIVA = 0;
                //oH.sDimension1 = string.Empty;
                //oH.sDimension2 = string.Empty;
                //oH.sDimension3 = ds.Tables[0].Rows[0]["AeropuertoIATA"].S();
                //oH.sDimension4 = ds.Tables[0].Rows[0]["TipoClienteDescripcion"].S();
                //oH.sDimension5 = ds.Tables[0].Rows[0]["ProyectoSAP"].S();
                //oH.sProyecto = sProyecto;
                ////AQUI GUARDA LINEA DE CONCEPTO
                //InsertaDetalleFactura(oH);
                //iNumeracionLinea++;


                foreach (DataRow row in ds.Tables[1].Rows)
                {
                    objArinvD = new FacturaPaso2();
                    objArinvD.iEmpresa = iEmpresa;
                    objArinvD.iIdFactura = iFolioFactura;

                    if (1 == 1) //Vuelo Nacional e Internacional
                        objArinvD.sItem = Utils.ObtieneParametroPorClave("111");

                    objArinvD.sCodigoBarras = sCodigoBarras;
                    objArinvD.iCantidad = iCantidad;
                    objArinvD.iDistSeq = iNumeracionLinea;

                    objArinvD.sConceptoUsuario = string.Format("{0} - {1} - {2} : {3}", "SV", row["FechaSalida"].S().Dt().ToString("dd/MM/yyyy"), row["FechaLlegada"].S().Dt().ToString("dd/MM/yyyy"), row["Ruta"].S());

                    switch (row["FactorIVAServiciosVuelo"].S().I())
                    {
                        case 16:
                            objArinvD.sTax_code1 = "IVAT16";
                            break;
                        case 4:
                            objArinvD.sTax_code1 = "IVAT04";
                            break;
                    }

                    switch (oIView.iIdMonedaSV)
                    {
                        case 1:
                            objArinvD.dAmount = row["SubtotalServiciosVuelo"].S().D() * oIView.TipoCambioPrefactura;
                            break;
                        case 2:
                            objArinvD.dAmount = row["SubtotalServiciosVuelo"].S().D();
                            break;
                    }

                    objArinvD.dAmount = decimal.Round(objArinvD.dAmount, 2, MidpointRounding.AwayFromZero);
                    objArinvD.iTaxSystem = row["FactorIVAServiciosVuelo"].S().I();
                    objArinvD.sAcct_unit1 = string.Empty;                                           //AREA
                    objArinvD.sAcct_unit2 = row["IdMatriculaInfo"].S();                             //MATRICULA
                    objArinvD.sAcct_unit3 = ds.Tables[0].Rows[0]["AeropuertoIATA"].S();             //BASE
                    objArinvD.sAcct_unit4 = ds.Tables[0].Rows[0]["TipoClienteDescripcion"].S();     //CODIGO FINANCIERO
                    objArinvD.sAcct_unit5 = ds.Tables[0].Rows[0]["ProyectoSAP"].S();                //Proyecto
                    objArinvD.sUsuario = Utils.GetUser;
                    objArinvD.sProyecto = sProyecto;

                    sResultadoArinVD = oIGestCat.SaveArinVD(objArinvD);
                    iNumeracionLinea++;
                }

                //string sResultadoArinVD = string.Empty;
                //FacturaPaso2 objArinvD = new FacturaPaso2();
                //DataSet ds = oIView.dsInformacionContrato;
                //int iFolioDocumento = (oIView.iIdPrefactura.S() + "2").S().I();

                //int iEmpresa = 1;
                //int iCantidad = 1;
                //string sProyecto = Utils.ObtieneParametroPorClave("110");
                int iMoneda = oIView.iIdMonedaSC;
                //string sItemSinCargo = Utils.ObtieneParametroPorClave("111");
                decimal dTipoCambioPrefa = oIView.TipoCambioPrefactura;
                //int iNumeracionLinea = 1;


                
                ///CODIGO COMENTADO POR AJUSTES A LA FACTURACION 3.3
                //FacturaSC oHSC = new FacturaSC();
                //oHSC.iIdPrefactura = iFolioFactura;
                //oHSC.iEmpresa = iEmpresa;
                //oHSC.sItem = sItemSinCargo;
                //oHSC.sConceptoUsuario = "SERVICIOS CON CARGO";
                //oHSC.sCodigoBarras = string.Empty;
                //oHSC.iCantidad = iCantidad;
                //oHSC.iNumeroLinea = iNumeracionLinea;
                //oHSC.iIdMoneda = 1;
                //oHSC.dSubtotal = 0;
                //oHSC.dTipoCambioPrefactura = 0;
                //oHSC.iFactorIVA = 0;
                //oHSC.sDimension1 = string.Empty;
                //oHSC.sDimension2 = string.Empty;
                //oHSC.sDimension3 = ds.Tables[0].Rows[0]["AeropuertoIATA"].S();
                //oHSC.sDimension4 = ds.Tables[0].Rows[0]["TipoClienteDescripcion"].S();
                //oHSC.sDimension5 = ds.Tables[0].Rows[0]["ProyectoSAP"].S();
                //oHSC.sProyecto = sProyecto;
                ////AQUI GUARDA LINEA DE CONCEPTO
                //InsertaDetalleFactura(oHSC);
                //iNumeracionLinea++;



                foreach (DataRow rowRemision in ds.Tables[1].Rows)
                {
                    //FacturaSC oR = new FacturaSC();
                    //oR.iIdPrefactura = iFolioFactura;
                    //oR.iEmpresa = iEmpresa;
                    //oR.sItem = sItemSinCargo;
                    //oR.sConceptoUsuario = string.Format("{0} - {1} : {2}", rowRemision["FechaSalida"].S().Dt().ToString("dd/MM/yyyy"), rowRemision["FechaLlegada"].S().Dt().ToString("dd/MM/yyyy"), rowRemision["Ruta"].S());
                    //oR.sCodigoBarras = string.Empty;
                    //oR.iCantidad = iCantidad;
                    //oR.iNumeroLinea = iNumeracionLinea;
                    //oR.iIdMoneda = iMoneda;
                    //oR.dSubtotal = 0;
                    //oR.dTipoCambioPrefactura = dTipoCambioPrefa;
                    //oR.iFactorIVA = 0;
                    //oR.sDimension1 = string.Empty;
                    //oR.sDimension2 = string.Empty;
                    //oR.sDimension3 = ds.Tables[0].Rows[0]["AeropuertoIATA"].S();
                    //oR.sDimension4 = ds.Tables[0].Rows[0]["TipoClienteDescripcion"].S();
                    //oR.sDimension5 = ds.Tables[0].Rows[0]["ProyectoSAP"].S();
                    //oR.sProyecto = sProyecto;
                    ////AQUI GUARDA LINEA DE CONCEPTO
                    //InsertaDetalleFactura(oR);
                    //iNumeracionLinea++;

                    string sConceptoSCC = "SCC - " + string.Format("{0} - {1} : {2}", rowRemision["FechaSalida"].S().Dt().ToString("dd/MM/yyyy"), rowRemision["FechaLlegada"].S().Dt().ToString("dd/MM/yyyy"), rowRemision["Ruta"].S());


                    DataRow[] rowsServicios = ds.Tables[2].Select("IdRemision = " + rowRemision["IdRemision"].S());
                    foreach (DataRow row in rowsServicios)
                    {
                        if (row["SubTotal"].S().D() > 0)
                        {
                            FacturaSC oS = new FacturaSC();
                            oS.iIdPrefactura = iFolioFactura;
                            oS.iEmpresa = iEmpresa;
                            oS.sItem = row["ItemSAP"].S();
                            oS.sConceptoUsuario = sConceptoSCC + " " + row["ServicioConCargoDescripcion"].S();
                            oS.sCodigoBarras = string.Empty;
                            oS.iCantidad = iCantidad;
                            oS.iNumeroLinea = iNumeracionLinea;
                            oS.iIdMoneda = iMoneda;
                            oS.dSubtotal = row["SubTotal"].S().D();
                            oS.dTipoCambioPrefactura = dTipoCambioPrefa;
                            oS.iFactorIVA = row["FactorIVA"].S().I();
                            oS.sDimension1 = string.Empty;
                            oS.sDimension2 = rowRemision["IdMatriculaInfo"].S();
                            oS.sDimension3 = ds.Tables[0].Rows[0]["AeropuertoIATA"].S();
                            oS.sDimension4 = ds.Tables[0].Rows[0]["TipoClienteDescripcion"].S();
                            oS.sDimension5 = ds.Tables[0].Rows[0]["ProyectoSAP"].S();
                            oS.sProyecto = sProyecto;
                            //AQUI GUARDA LINEA DE CONCEPTO
                            InsertaDetalleFactura(oS);
                            iNumeracionLinea++;
                        }
                    }
                }

                oIView.sFacturaVuelo = "En proceso";
                oIView.sFacturaSCC = "En proceso";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Inserta el Header de la factura
        /// </summary>
        /// <param name="oF">Objeto que contiene todas las propiedades para header</param>
        /// <param name="sTipo">Servicios vuelo, Servicios cargo</param>
        private void InsertaHeader(string sTipo, int iNumerador)
        {
            try
            {
                FacturaPaso1 objArinv = new FacturaPaso1();
                Prefactura objPref = oIView.objPrefactura;

                objArinv.sTipo = sTipo;
                objArinv.iIdPrefactura = (oIView.iIdPrefactura.S() + iNumerador.S()).S().I();

                string sFact = sTipo == "SC" ? objPref.sClaveFacturanteSCC : objPref.sClaveFacturanteVuelo;
                objArinv.sFacturante = sFact;

                objArinv.dtInvDate = DateTime.Now.Date;
                //objArinv.dtDueDate = objArinv.dtInvDate.AddDays(days_due);
                objArinv.iEmpresa = 1;
                objArinv.sSucursal = "1";
                objArinv.sSerie = "";
                
                int iIdentificadorMoneda = sTipo == "SC" ? oIView.iIdMonedaSC : oIView.iIdMonedaSV;

                switch (iIdentificadorMoneda)
                {
                    case 1:
                        //objArinv.sAcct = Utils.GetParametrosClave("12").S();
                        objArinv.dAmount = objPref.dSubMXNC;
                        objArinv.dSales_tax = objPref.dIVAMXNC;
                        objArinv.dExch_rate = 1;
                        objArinv.sMoneda = "MXP";
                        break;
                    case 2:
                        //objArinv.sAcct = Utils.GetParametrosClave("13").S();
                        objArinv.dAmount = objPref.dSubDllC;
                        objArinv.dSales_tax = objPref.dIVADllC;
                        objArinv.dExch_rate = oIView.TipoCambioPrefactura.D();
                        objArinv.sMoneda = "USD";
                        break;
                }

                objArinv.sRef = "(" + sTipo + ") MXJ";
                objArinv.sDescription = objArinv.sRef;

                DataSet ds = oIView.dsInformacionContrato;
                if (ds.Tables[2].Rows.Count > 0)
                {
                    switch (ds.Tables[2].Rows[0]["FactorIVA"].S().I())
                    {
                        case 16:
                            objArinv.sTax_code1 = "IVAT16"; //Utils.GetParametrosClave("15");
                            break;
                        case 4:
                            objArinv.sTax_code1 = "IVAT04"; //Utils.GetParametrosClave("16");
                            break;
                    }
                }
                else
                {
                    if (sTipo == "FV")
                    {
                        switch (ds.Tables[1].Rows[0]["FactorIVAServiciosVuelo"].S().I())
                        {
                            case 16:
                                objArinv.sTax_code1 = "IVAT16"; //Utils.GetParametrosClave("15");
                                break;
                            case 4:
                                objArinv.sTax_code1 = "IVAT04"; //Utils.GetParametrosClave("16");
                                break;
                        }
                    }
                    else
                    {
                        switch (ds.Tables[1].Rows[0]["FactorIVASCC"].S().I())
                        {
                            case 16:
                                objArinv.sTax_code1 = "IVAT16"; //Utils.GetParametrosClave("15");
                                break;
                            case 4:
                                objArinv.sTax_code1 = "IVAT04"; //Utils.GetParametrosClave("16");
                                break;
                        }
                    }
                }

                objArinv.sAcct_unit3 = ds.Tables[0].Rows[0]["AeropuertoIATA"].S();
                objArinv.sAcct_unit4 = ds.Tables[0].Rows[0]["TipoClienteDescripcion"].S();
                objArinv.TipoFactura = sTipo;
                objArinv.Ufremision = (oIView.iIdPrefactura.S() + iNumerador.S()).S().I();

                objArinv.sMetodoPago = ds.Tables[0].Rows[0]["MetodoPago"].S();
                objArinv.sFormaPago = ds.Tables[0].Rows[0]["FormaPago"].S(); ;
                objArinv.sUsoCFDI = ds.Tables[0].Rows[0]["UsoCFDI"].S();

                string sResultArin = oIGestCat.SaveArinV(objArinv);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void InsertaDetalleSV(string sConcepto)
        {
            try
            {
                /*
                 * ds.Tables[0] --> Base Predeterminada y Tipo de Cliente
                 * ds.Tables[1] --> Remisiones incluidas en la prefactura
                 * ds.Tables[2] --> Servicios con cargo agrupados por remision
                 * ds.Tables[3] --> Remisión, Cuenta y Descripcion
                 * ds.Tables[4] --> Clave de la cuenta
                 * ds.Tables[5] --> Remisión con su factor de iva
                 * ds.Tables[6] --> IdConcepto y su tarifa en Dlls (Servicios de Vuelo)
                 */

                string sResultadoArinVD = string.Empty;
                FacturaPaso2 objArinvD = new FacturaPaso2();
                DataSet ds = oIView.dsInformacionContrato;
                
                int iEmpresa = 1;
                string sCodigoBarras = string.Empty;
                int iCantidad = 1;
                int iFolioFactura = (oIView.iIdPrefactura.S() + "1").S().I();
                string sProyecto = Utils.ObtieneParametroPorClave("110");
                string sItemSinCargo = Utils.ObtieneParametroPorClave("111");

                int iNumeracionLinea = 1;

                //FacturaSC oH = new FacturaSC();
                //oH.iIdPrefactura = iFolioFactura;
                //oH.iEmpresa = iEmpresa;
                //oH.sItem = sItemSinCargo;
                //oH.sConceptoUsuario = ds.Tables[3].Rows[0]["Descripcion"].S(); ;
                //oH.sCodigoBarras = sCodigoBarras;
                //oH.iCantidad = iCantidad;
                //oH.iNumeroLinea = iNumeracionLinea;
                //oH.iIdMoneda = 1;
                //oH.dSubtotal = 0;
                //oH.dTipoCambioPrefactura = 0;
                //oH.iFactorIVA = 0;
                //oH.sDimension1 = string.Empty;
                //oH.sDimension2 = string.Empty;
                //oH.sDimension3 = ds.Tables[0].Rows[0]["AeropuertoIATA"].S();
                //oH.sDimension4 = ds.Tables[0].Rows[0]["TipoClienteDescripcion"].S();
                //oH.sDimension5 = ds.Tables[0].Rows[0]["ProyectoSAP"].S();
                //oH.sProyecto = sProyecto;
                ////AQUI GUARDA LINEA DE CONCEPTO
                //InsertaDetalleFactura(oH);
                //iNumeracionLinea++;


                foreach (DataRow row in ds.Tables[1].Rows)
                {
                    objArinvD = new FacturaPaso2();
                    objArinvD.iEmpresa = iEmpresa;
                    objArinvD.iIdFactura = iFolioFactura;


                    switch (row["FactorIVAServiciosVuelo"].S().I()) //Vuelo Nacional e Internacional
                    {
                        case 16:
                            objArinvD.sItem = Utils.ObtieneParametroPorClave("111");
                            objArinvD.sTax_code1 = "IVAT16";
                            break;
                        case 4:
                            objArinvD.sItem = Utils.ObtieneParametroPorClave("114");
                            objArinvD.sTax_code1 = "IVAT4";
                            break;
                    }
                        

                    objArinvD.sCodigoBarras = sCodigoBarras;
                    objArinvD.iCantidad = iCantidad;
                    objArinvD.iDistSeq = iNumeracionLinea;
                    
                    objArinvD.sConceptoUsuario = string.Format("{0} - {1} - {2} : {3}", "SV", row["FechaSalida"].S().Dt().ToString("dd/MM/yyyy"), row["FechaLlegada"].S().Dt().ToString("dd/MM/yyyy"), row["Ruta"].S());

                    
                    switch (oIView.iIdMonedaSV)
                    {
                        case 1:
                            objArinvD.dAmount = row["SubtotalServiciosVuelo"].S().D() * oIView.TipoCambioPrefactura;
                            break;
                        case 2:
                            objArinvD.dAmount = row["SubtotalServiciosVuelo"].S().D();
                            break;
                    }

                    objArinvD.dAmount = decimal.Round(objArinvD.dAmount, 2, MidpointRounding.AwayFromZero);
                    objArinvD.iTaxSystem = row["FactorIVAServiciosVuelo"].S().I();
                    objArinvD.sAcct_unit1 = string.Empty;                                           //AREA
                    objArinvD.sAcct_unit2 = row["IdMatriculaInfo"].S();                             //MATRICULA
                    objArinvD.sAcct_unit3 = ds.Tables[0].Rows[0]["AeropuertoIATA"].S();             //BASE
                    objArinvD.sAcct_unit4 = ds.Tables[0].Rows[0]["TipoClienteDescripcion"].S();     //CODIGO FINANCIERO
                    objArinvD.sAcct_unit5 = ds.Tables[0].Rows[0]["ProyectoSAP"].S();                //Proyecto
                    objArinvD.sUsuario = Utils.GetUser;
                    objArinvD.sProyecto = sProyecto;

                    sResultadoArinVD = oIGestCat.SaveArinVD(objArinvD);
                    iNumeracionLinea++;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void InsertaDetalleSC(string sConcepto)
        {
            try
            {
                string sResultadoArinVD = string.Empty;
                FacturaPaso2 objArinvD = new FacturaPaso2();
                DataSet ds = oIView.dsInformacionContrato;
                int iFolioDocumento = (oIView.iIdPrefactura.S() + "2").S().I();

                int iEmpresa = 1;
                int iCantidad = 1;
                string sProyecto = Utils.ObtieneParametroPorClave("110");
                int iMoneda = oIView.iIdMonedaSC;
                string sItemSinCargo = Utils.ObtieneParametroPorClave("111");
                decimal dTipoCambioPrefa = oIView.TipoCambioPrefactura;
                int iNumeracionLinea = 1;


                //FacturaSC oH = new FacturaSC();
                //oH.iIdPrefactura = iFolioDocumento;
                //oH.iEmpresa = iEmpresa;
                //oH.sItem = sItemSinCargo;
                //oH.sConceptoUsuario = "SERVICIOS CON CARGO";
                //oH.sCodigoBarras = string.Empty;
                //oH.iCantidad = iCantidad;
                //oH.iNumeroLinea = iNumeracionLinea;
                //oH.iIdMoneda = 1;
                //oH.dSubtotal = 0;
                //oH.dTipoCambioPrefactura = 0;
                //oH.iFactorIVA = 0;
                //oH.sDimension1 = string.Empty;
                //oH.sDimension2 = string.Empty;
                //oH.sDimension3 = ds.Tables[0].Rows[0]["AeropuertoIATA"].S();
                //oH.sDimension4 = ds.Tables[0].Rows[0]["TipoClienteDescripcion"].S();
                //oH.sDimension5 = ds.Tables[0].Rows[0]["ProyectoSAP"].S();
                //oH.sProyecto = sProyecto;
                ////AQUI GUARDA LINEA DE CONCEPTO
                //InsertaDetalleFactura(oH);
                //iNumeracionLinea++;


                foreach (DataRow rowRemision in ds.Tables[1].Rows)
                {
                    //FacturaSC oR = new FacturaSC();
                    //oR.iIdPrefactura = iFolioDocumento;
                    //oR.iEmpresa = iEmpresa;
                    //oR.sItem = sItemSinCargo;
                    //oR.sConceptoUsuario = string.Format("{0} - {1} : {2}", rowRemision["FechaSalida"].S().Dt().ToString("dd/MM/yyyy"), rowRemision["FechaLlegada"].S().Dt().ToString("dd/MM/yyyy"), rowRemision["Ruta"].S());
                    //oR.sCodigoBarras = string.Empty;
                    //oR.iCantidad = iCantidad;
                    //oR.iNumeroLinea = iNumeracionLinea;
                    //oR.iIdMoneda = iMoneda;
                    //oR.dSubtotal = 0;
                    //oR.dTipoCambioPrefactura = 0;
                    //oR.iFactorIVA = 0;
                    //oR.sDimension1 = string.Empty;
                    //oR.sDimension2 = string.Empty;
                    //oR.sDimension3 = ds.Tables[0].Rows[0]["AeropuertoIATA"].S();
                    //oR.sDimension4 = ds.Tables[0].Rows[0]["TipoClienteDescripcion"].S();
                    //oR.sDimension5 = ds.Tables[0].Rows[0]["ProyectoSAP"].S();
                    //oR.sProyecto = sProyecto;
                    ////AQUI GUARDA LINEA DE CONCEPTO
                    //InsertaDetalleFactura(oR);
                    //iNumeracionLinea++;

                    string sConceptoSCC = "SCC - " + string.Format("{0} - {1} : {2}", rowRemision["FechaSalida"].S().Dt().ToString("dd/MM/yyyy"), rowRemision["FechaLlegada"].S().Dt().ToString("dd/MM/yyyy"), rowRemision["Ruta"].S());


                    DataRow[] rowsServicios = ds.Tables[2].Select("IdRemision = " + rowRemision["IdRemision"].S());
                    foreach (DataRow row in rowsServicios)
                    {
                        if (row["SubTotal"].S().D() > 0)
                        {
                            #region SERVICIOS CON CARGO
                            //objArinvD = new FacturaPaso2();
                            //objArinvD.iEmpresa = iEmpresa;
                            //objArinvD.iIdFactura = iFolioDocumento;
                            //objArinvD.sItem = row["ItemSAP"].S();
                            //objArinvD.sConceptoUsuario = row["ServicioConCargoDescripcion"].S();
                            //objArinvD.sCodigoBarras = "";
                            //objArinvD.iCantidad = iCantidad;
                            //objArinvD.iDistSeq = iNumeracionLinea;

                            //switch (row["FactorIVA"].S().I())
                            //{
                            //    case 16:
                            //        objArinvD.sTax_code1 = "IVAT16";
                            //        break;
                            //    case 4:
                            //        objArinvD.sTax_code1 = "IVAT04";
                            //        break;
                            //    case 0:
                            //        objArinvD.sTax_code1 = "IVATNA";
                            //        break;
                            //}

                            //switch (oIView.iIdMonedaSC)
                            //{
                            //    case 1:
                            //        objArinvD.dAmount = row["SubTotal"].S().D();
                            //        break;
                            //    case 2:
                            //        objArinvD.dAmount = decimal.Round((row["SubTotal"].S().D() / oIView.TipoCambioPrefactura), 2, MidpointRounding.AwayFromZero);
                            //        break;
                            //}

                            //objArinvD.iTaxSystem = row["FactorIVA"].S().I();
                            //DataRow[] rows = ds.Tables[1].Select("IdRemision = " + row["IdRemision"].S());

                            //objArinvD.sAlmacen = "";
                            //objArinvD.sAcct = row["cveCuenta"].S();
                            //objArinvD.sAcct_unit1 = string.Empty; //row["CveCodigoUnidad1"].S(); NO TENEMOS CODIGO DE UNIDAD PORQUE SOLO SE AGREGÓ EL ARTICULO
                            //objArinvD.sAcct_unit2 = rows[0]["IdMatriculaInfo"].S();
                            //objArinvD.sAcct_unit3 = ds.Tables[0].Rows[0]["AeropuertoIATA"].S();
                            //objArinvD.sAcct_unit4 = ds.Tables[0].Rows[0]["TipoClienteDescripcion"].S();
                            //objArinvD.sAcct_unit5 = string.Empty;
                            //objArinvD.sProyecto = sProyecto;
                            //objArinvD.sUsuario = Utils.GetUser;

                            //sResultadoArinVD = oIGestCat.SaveArinVD(objArinvD);
                            #endregion

                            FacturaSC oS = new FacturaSC();
                            oS.iIdPrefactura = iFolioDocumento;
                            oS.iEmpresa = iEmpresa;
                            oS.sItem = row["ItemSAP"].S();
                            oS.sConceptoUsuario = sConceptoSCC + " " + row["ServicioConCargoDescripcion"].S();
                            oS.sCodigoBarras = string.Empty;
                            oS.iCantidad = iCantidad;
                            oS.iNumeroLinea = iNumeracionLinea;
                            oS.iIdMoneda = iMoneda;
                            oS.dSubtotal = row["SubTotal"].S().D();
                            oS.dTipoCambioPrefactura = dTipoCambioPrefa;
                            oS.iFactorIVA = row["FactorIVA"].S().I();
                            oS.sDimension1 = row["CveCodigoUnidad1"].S(); // PROVISIONAL EN LO QUE SE OCUPAN LOS SERVICIOS CON CARGO COMO DEBERÍAN EN SAP
                            oS.sDimension2 = rowRemision["IdMatriculaInfo"].S();
                            oS.sDimension3 = ds.Tables[0].Rows[0]["AeropuertoIATA"].S();
                            oS.sDimension4 = ds.Tables[0].Rows[0]["TipoClienteDescripcion"].S();
                            oS.sDimension5 = ds.Tables[0].Rows[0]["ProyectoSAP"].S();
                            oS.sProyecto = sProyecto;
                            //AQUI GUARDA LINEA DE CONCEPTO
                            InsertaDetalleFactura(oS);
                            iNumeracionLinea++;
                        }
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void VerificaCantidadFacuturas(object sender, EventArgs e)
        {
            try
            {
                oIView.bUnaFactura = oIGestCat.MasDeUnaFactura(oIView.iIdContrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void eGetFacturantesSAP_Presenter(object sender, EventArgs e)
        {
            oIView.dtFacturantesSAP = new DBSAP().DBGetFacturantes(oIView.sClaveCliente); //oIGestCat.DBGetFacturantesSAP(oIView.iIdCliente);
        }

        private void InsertaDetalleFactura(FacturaSC oF)
        {
            /*  "@Empresa", Paso.iEmpresa,
                "@ID", Paso.iIdFactura,
                "@Linea", Paso.iDistSeq,
                "@Item", Paso.sItem,
                "@DescripcionUsuario", Paso.sConceptoUsuario,
                "@CodBarras", Paso.sCodigoBarras,
                "@Cantidad", Paso.iCantidad,
                "@Precio", decimal.Parse(Paso.dAmount),
                "@Descuento", Paso.dDescuento,
                "@Impuesto", Paso.iTaxSystem,
                "@CodigoImpuesto", Paso.sTax_code1,
                "@TotalLinea", ((Paso.iCantidad * decimal.Parse(Paso.dAmount)) - Paso.dDescuento),
                "@Almacen", Paso.sAlmacen,
                "@Proyecto", Paso.sProyecto,
                "@Dimension1", Paso.sAcct_unit1,
                "@Dimension2", Paso.sAcct_unit2,
                "@Dimension3", Paso.sAcct_unit3,
                "@Dimension4", Paso.sAcct_unit4,
                "@Dimension5", Paso.sAcct_unit5*/

            try
            {
                FacturaPaso2 objArinvD = new FacturaPaso2();
                objArinvD.iEmpresa = oF.iEmpresa;
                objArinvD.iIdFactura = oF.iIdPrefactura;
                objArinvD.sItem = oF.sItem;
                objArinvD.sConceptoUsuario = oF.sConceptoUsuario;
                objArinvD.sCodigoBarras = oF.sCodigoBarras;
                objArinvD.iCantidad = oF.iCantidad;
                objArinvD.iDistSeq = oF.iNumeroLinea;

                switch (oF.iFactorIVA)
                {
                    case 16:
                        objArinvD.sTax_code1 = "IVAT16";
                        break;
                    case 4:
                        objArinvD.sTax_code1 = "IVAT04";
                        break;
                    case 0:
                        objArinvD.sTax_code1 = "IVATNA";
                        break;
                }

                switch (oF.iIdMoneda)
                {
                    case 1:
                        objArinvD.dAmount = oF.dSubtotal;
                        break;
                    case 2:
                        objArinvD.dAmount = decimal.Round((oF.dSubtotal / oF.dTipoCambioPrefactura), 2, MidpointRounding.AwayFromZero);
                        break;
                }

                objArinvD.iTaxSystem = oF.iFactorIVA;

                objArinvD.sAlmacen = "";
                objArinvD.sAcct_unit1 = oF.sDimension1;
                objArinvD.sAcct_unit2 = oF.sDimension2;
                objArinvD.sAcct_unit3 = oF.sDimension3;
                objArinvD.sAcct_unit4 = oF.sDimension4;
                objArinvD.sAcct_unit5 = oF.sDimension5;
                objArinvD.sProyecto = oF.sProyecto;
                objArinvD.sUsuario = Utils.GetUser;

                string sResultadoArinVD = oIGestCat.SaveArinVD(objArinvD);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}