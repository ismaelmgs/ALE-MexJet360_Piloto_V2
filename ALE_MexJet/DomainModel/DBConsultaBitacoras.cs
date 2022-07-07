using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using NucleoBase.BaseDeDatos;
using ALE_MexJet.Objetos;

namespace ALE_MexJet.DomainModel
{
    public class DBConsultaBitacoras : DBBaseAleSuite
    {
        public DataTable DBGetConsultaBitacoras()
        {
            try
            {
                return oDB_SP.EjecutarDT("[ASwitch].[spS_ASwitch_ConsultaBitacoras]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DBSetActualizaBitacora(BitacoraVuelo oB)
        {
            try
            {
                bool ban = true;

                oDB_SP.EjecutarValor("[ASwitch].[spU_ASwitch_ActualizaBitacoras]", "@idBitacora", oB.IIdBitacora,
                                                                                   "@legid", oB.LLegId,
                                                                                   "@folio", oB.SFolio,
                                                                                   "@flightoff", oB.SFlightOff,
                                                                                   "@flighton", oB.SFlightOn,
                                                                                   "@flightdiff", oB.SFlightDiff,
                                                                                   "@calzoin", oB.SCalzoIn,
                                                                                   "@calzoout", oB.SCalzoOut,
                                                                                   "@calzodiff", oB.SCalzoDiff,
                                                                                   "@fuelin", oB.SFuelIn,
                                                                                   "@fuelout", oB.SFuelOut,
                                                                                   "@fueldiff", oB.SFuelDiff);	
                return ban;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DBSetAutorizaBitacora(BitacoraVuelo oB)
        {
            try
            {
                bool ban = true;

                oDB_SP.EjecutarValor("[ASwitch].[spU_ASwitch_ActualizaEstatus]", "@idBitacora", oB.IIdBitacora,
                                                                                 "@legid", oB.LLegId,
                                                                                 "@folio", oB.SFolio,
                                                                                 "@user", oB.SUser);
                return ban;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public DataTable DBGetFotoXBitacora(BitacoraVuelo oB)
        {
            try
            {
                return oDB_SP.EjecutarDT("[ASwitch].[spS_ASwitch_ConsultaFotoXBitacora]", "@IdBitacora", oB.IIdBitacora,
                                                                                          "@LegId", oB.LLegId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}