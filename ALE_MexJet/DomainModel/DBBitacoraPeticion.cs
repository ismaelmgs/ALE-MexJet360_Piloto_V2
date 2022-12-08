using ALE_MexJet.Clases;
using ALE_MexJet.Objetos;
using Newtonsoft.Json;
using NucleoBase.Core;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;

namespace ALE_MexJet.DomainModel
{
    public class DBBitacoraPeticion : DBBaseFlexFli
    {
        #region Metodos

        public List<FlightsFlexx> getFlights(string sFechaInicio, string sFechaFin, string tripNumber)
        {
            try
            {
                List<PeriodosFlexx> periodos = Utils.getPeriodos(sFechaInicio, sFechaFin);
                List<FlightsFlexx> flights = new List<FlightsFlexx>();

                foreach (PeriodosFlexx item in periodos)
                {
                    List<FlightsFlexx> listFlights = new List<FlightsFlexx>();


                    listFlights = obtenerFlights(item);

                    foreach (FlightsFlexx flg in listFlights)
                    {
                        if (flg.tripNumber.Value == tripNumber.I())
                        {
                            flights.Add(flg);
                        }
                    }
                }

                return flights;
            }
            catch (Exception)
            {
                return null;
            }
        }

        internal bool validarBitacora()
        {
            try
            {
                new DBBitacoras().validateBitacoras();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        internal void createBitacora(List<FlightsFlexx> flights, List<PostFlightFlexx> lstPostFlights)
        {
            try
            {
                foreach(FlightsFlexx flight in flights)
                {
                    BitacoraFlex bitacora = new BitacoraFlex();
                    bitacora.AeronaveMatricula = flight.registrationNumber;
                    bitacora.VueloContratoId = flight.customerFirstname;
                    bitacora.Origen = flight.airportFrom;
                    bitacora.Destino = flight.airportTo;
                    bitacora.CantPax = Convert.ToInt32(flight.paxNumber);
                    bitacora.TripNum = flight.tripNumber.Value;
                    bitacora.Leg_Num = flight.tripNumber.Value;
                    bitacora.LegId = flight.flightId.Value;

                    PostFlightFlexx pf = new PostFlightFlexx();
                    pf = lstPostFlights.FirstOrDefault(x => x.FlightId == flight.flightId);

                    if(pf != null)
                    {
                        bitacora.FolioReal = pf.Time.Flight.TechlogNumber;
                        bitacora.PilotoId = pf.Time.Cmd.Nickname;
                        bitacora.CopilotoId = pf.Time.Fo.Nickname;
                        bitacora.Fecha = getfecha(pf.EstimatedTimes.TakeOff);
                        bitacora.OrigenCalzo = getfecha(pf.Time.Dep.TakeOff);
                        bitacora.DestinoCalzo = getfecha(pf.Time.Dep.BlocksOff);
                        bitacora.ConsumoOri = pf.Fuel.FuelRemainigActual.Value;
                        bitacora.ConsumoDes = pf.Fuel.FuelArrival.Value;
                        bitacora.Tipo = pf.TrafficReportPanel.FlightType;
                        bitacora.DestinoVuelo = getfecha(pf.Time.Arr.Landing);
                        bitacora.OrigenVuelo = getfecha(pf.Time.Dep.TakeOff);
                    }
                    
                    new DBBitacoras().saveBitacora(bitacora);
                }
            }
            catch (Exception ex)
            {

            }
        }

        internal void setPostFlight(List<PostFlightFlexx> lstPostFlights)
        {
            try
            {
                DataSet ds = new DataSet();
                foreach (PostFlightFlexx item in lstPostFlights)
                {
                    PostFlightFlexx p = new PostFlightFlexx();
                    p = item;

                    if (p.Time != null && p.Time.Status == "OK")
                    {
                        ds = savePostFlight(p);

                        DataTable dt = ds.Tables[0];
                        var Idpostflight = Convert.ToInt32(dt.Rows[0]["Identity"].ToString());

                        if (p.ArrFuel != null)
                        {
                            ArrFuel af = new ArrFuel();
                            af = p.ArrFuel;
                            af.Idpostflight = Idpostflight;

                            saveArrFuel(af);
                        }

                        if (p.Time != null)
                        {
                            Time t = new Time();
                            t = p.Time;
                            t.Idpostflight = Idpostflight;

                            ds = saveTime(t);

                            DataTable dtt = ds.Tables[0];

                            var Idtime = Convert.ToInt32(dtt.Rows[0]["Identity"].ToString());

                            if (t.Cmd != null)
                            {
                                Cmd c = new Cmd();
                                c = t.Cmd;
                                c.Idtime = Idtime;
                                c.Idoperator = c.Operator != null ? c.Operator.Id : 0;

                                saveCmd(c);

                                if (c.Operator != null)
                                {
                                    Operator o = new Operator();
                                    o = c.Operator;

                                    saveOperator(o);
                                }

                                if (c.UserCharacteristics != null)
                                {
                                    UserCharacteristics us = new UserCharacteristics();
                                    us = c.UserCharacteristics;
                                    us.Idcmd = c.Id;

                                    saveUserCharacteristics(us);
                                }

                            }

                            if (t.Fo != null)
                            {
                                Fo f = new Fo();
                                f = t.Fo;
                                f.Idtime = Idtime;
                                f.Idoperator = f.Operator != null ? f.Operator.Id : 0;

                                saveFo(f);

                                if (f.Operator != null)
                                {
                                    Operator o = new Operator();
                                    o = f.Operator;

                                    saveOperator(o);
                                }

                                if (f.UserCharacteristics != null)
                                {
                                    UserCharacteristics us = new UserCharacteristics();
                                    us = f.UserCharacteristics;
                                    us.Idfo = f.Idfo;

                                    saveUserCharacteristics(us);
                                }

                            }

                            if (t.Dep != null)
                            {
                                Dep d = new Dep();
                                d = t.Dep;
                                d.Idtime = Idtime;

                                saveDep(d);
                            }

                            if (t.Flight != null)
                            {
                                FlightTime ft = new FlightTime();
                                ft = t.Flight;
                                ft.Idtime = Idtime;

                                saveFlight(ft);
                            }

                            if (t.Arr != null)
                            {
                                Arr a = new Arr();
                                a = t.Arr;
                                a.Idtime = Idtime;

                                saveArr(a);
                            }
                        }

                        if (p.EstimatedTimes != null)
                        {
                            EstimatedTimes es = new EstimatedTimes();
                            es = p.EstimatedTimes;
                            es.Idpostflight = Idpostflight;

                            saveEstimatedTimes(es);
                        }

                        if (p.TrafficReportPanel != null)
                        {
                            TrafficReportPanel tr = new TrafficReportPanel();
                            tr = p.TrafficReportPanel;
                            tr.Idpostflight = Idpostflight;

                            saveTrafficReportPanel(tr);
                        }

                        if (p.PreFlight != null)
                        {
                            PreFlight pf = new PreFlight();
                            pf = p.PreFlight;
                            pf.Idpostflight = Idpostflight;

                            savePreFlight(pf);
                        }

                        if (p.Fuel != null)
                        {
                            Fuel fu = new Fuel();
                            fu = p.Fuel;
                            fu.Idpostflight = Idpostflight;

                            saveFuel(fu);
                            var IdFuel = fu.Id;

                            if (fu.FuelRelease != null && IdFuel != null)
                            {
                                FuelRelease fr = new FuelRelease();
                                fr = fu.FuelRelease;
                                fr.Idfuel = IdFuel;
                                fr.IdarrFuel = 0;

                                saveFuelRealse(fr);
                            }

                            if (fu.Release != null && IdFuel != null)
                            {
                                Release re = new Release();
                                re = fu.Release;
                                re.Idfuel = IdFuel;
                                re.IdarrFuel = 0;

                                saveRealse(re);
                            }
                        }

                        updateFlightStatus(p.FlightId);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        internal List<PostFlightFlexx> postFlights(List<FlightsFlexx> flights)
        {
            try
            {
                string token = Globales.GetConfigApp<string>("X-Auth-Token");
                List<PostFlightFlexx> pf = new List<PostFlightFlexx>();

                foreach (FlightsFlexx item in flights)
                {
                    try
                    {
                        PostFlightFlexx p = new PostFlightFlexx();
                        p = obtenerPostFlight(item);

                        pf.Add(p);
                    }
                    catch (Exception ex)
                    {
                    }
                }

                return pf;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        internal void setFlights(List<FlightsFlexx> flights)
        {
            try
            {
                List<groupFlights> gfs = new List<groupFlights>();
                foreach (FlightsFlexx item in flights)
                {
                    try
                    {
                        FlightsFlexx flg = new FlightsFlexx();
                        flg = item;

                        groupFlights gf = new groupFlights();
                        gf.tripNumber = flg.tripNumber.Value;
                        gf.flightId = flg.flightId.Value;
                        gf.esGrupo = flights.Any(x => x.tripNumber == gf.tripNumber && x.flightId != gf.flightId);
                        gf.noVuelo = gfs.Where(x => x.tripNumber == gf.tripNumber).Count() == 0 ? 1 : (gfs.Where(x => x.tripNumber == gf.tripNumber).Count() + 1);

                        gfs.Add(gf);

                        if (item.@operator != null)
                        {
                            Operator o = new Operator();
                            o = item.@operator;
                            flg.Idoperator = o.Id;

                            saveOperator(o);
                        }

                        if (item.aircraftAOC != null)
                        {
                            AircraftAOC airc = new AircraftAOC();
                            airc = item.aircraftAOC;
                            flg.IdaircraftAOC = airc.Id;

                            saveAircraft(airc);
                        }

                        if (item.paxReferences != null)
                        {
                            foreach (PaxReferences i in item.paxReferences)
                            {
                                PaxReferences px = new PaxReferences();
                                px = i;
                                px.flightId = flg.flightId.Value;

                                DataSet ds = savePaxReferences(px);
                                DataTable dt = ds.Tables[0];

                                var Id = Convert.ToInt32(dt.Rows[0]["Identity"].ToString());

                                if (px.links != null)
                                {
                                    foreach (Links l in px.links)
                                    {
                                        Links link = new Links();
                                        link = l;
                                        link.IdpaxReferences = Id;

                                        saveLinks(link);
                                    }
                                }
                            }
                        }

                        saveFlights(flg);

                    }
                    catch (Exception ex)
                    {
                    }

                    setGroupFlights(gfs);
                }
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region Peticiones

        private List<FlightsFlexx> obtenerFlights(PeriodosFlexx item)
        {
            try
            {
                string token = Globales.GetConfigApp<string>("X-Auth-Token");

                List<FlightsFlexx> flg = new List<FlightsFlexx>();

                var client = new RestClient(Helper.UrlBase + Helper.Urlflights);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var dmenos = item.Inicio.ToString("yyyy-MM-dd");
                var dmas = item.Fin.ToString("yyyy-MM-dd");

                IRestResponse response = null;

                var request = new RestRequest("", Method.GET);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("X-Auth-Token", token);

                request.AddParameter("from", dmenos);
                request.AddParameter("to", dmas);

                response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var contents = response.Content;
                    flg = JsonConvert.DeserializeObject<List<FlightsFlexx>>(contents);

                    return flg;
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void setGroupFlights(List<groupFlights> gfs)
        {
            try
            {
                foreach (groupFlights gf in gfs)
                {
                    updateFlights(gf);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private DateTime? getfecha(long? value)
        {
            if (value != null)
            {
                double ticks = Convert.ToDouble(value);
                TimeSpan time = TimeSpan.FromMilliseconds(ticks);
                DateTime startdate = new DateTime(1970, 1, 1) + time;

                return startdate;
            }

            return null;
        }

        private PostFlightFlexx obtenerPostFlight(FlightsFlexx item)
        {
            try
            {
                string token = Globales.GetConfigApp<string>("X-Auth-Token");

                PostFlightFlexx pflg = new PostFlightFlexx();

                var client = new RestClient(Helper.UrlBase + Helper.Urlpostflight.Replace("{1}", item.flightId.ToString()));
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                IRestResponse response = null;

                var request = new RestRequest("", Method.GET);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("X-Auth-Token", token);

                response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var contents = response.Content;
                    pflg = JsonConvert.DeserializeObject<PostFlightFlexx>(contents);
                    pflg.FlightId = item.flightId.Value;

                    return pflg;
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region EJECUCION DE SPS

        private void saveFlights(FlightsFlexx flg)
        {
            oDB_SP.EjecutarDS("[FLX].[spI_Flights]",
                              "@accountReference", flg.accountReference,
                              "@aircraftCategory", flg.aircraftCategory,
                              "@airportFrom", flg.airportFrom,
                              "@airportTo", flg.airportTo,
                              "@blocksoffestimated", flg.blocksoffestimated,
                              "@blocksonestimated", flg.blocksonestimated,
                              "@bookingIdentifier", flg.bookingIdentifier,
                              "@bookingReference", flg.bookingReference,
                              "@customerFirstname", flg.customerFirstname,
                              "@customerId", flg.customerId,
                              "@customerLastname", flg.customerLastname,
                              "@customerTrigram", flg.customerTrigram,
                              "@dateFrom", flg.dateFrom,
                              "@dateTo", flg.dateTo,
                              "@flightId", flg.flightId,
                              "@flightNumber", flg.flightNumber,
                              "@flightNumberCompany", flg.flightNumberCompany,
                              "@flightStatus", flg.flightStatus,
                              "@flightType", flg.@flightType,
                              "@fplType", flg.fplType,
                              "@fuelArrival", flg.fuelArrival,
                              "@fuelMassUnit", flg.fuelMassUnit,
                              "@fuelOffBlock", flg.fuelOffBlock,
                              "@fuelRemainigActual", flg.fuelRemainigActual,
                              "@paxNumber", flg.paxNumber,
                              "@postFlightClosed", flg.postFlightClosed,
                              "@realAirportFrom", flg.realAirportFrom,
                              "@realAirportTo", flg.realAirportTo,
                              "@realDateIN", flg.realDateIN,
                              "@realDateOFF", flg.realDateOFF,
                              "@realDateON", flg.realDateON,
                              "@realDateOUT", flg.realDateOUT,
                              "@registrationNumber", flg.registrationNumber,
                              "@requestedAircraftType", flg.requestedAircraftType,
                              "@rescheduledDateFrom", flg.rescheduledDateFrom,
                              "@rescheduledDateTo", flg.rescheduledDateTo,
                              "@status", flg.status,
                              "@tripNumber", flg.tripNumber,
                              "@upliftMass", flg.upliftMass,
                              "@userReference", flg.userReference,
                              "@workflow", flg.workflow,
                              "@workflowCustomName", flg.workflowCustomName,
                              "@IdaircraftAOC", flg.IdaircraftAOC,
                              "@Idoperator", flg.Idoperator,
                              "@blockOffEstUTC", flg.blockOffEstUTC,
                              "@blockOnEstUTC", flg.blockOnEstUTC,
                              "@blockOffEstLocal", flg.blockOffEstLocal,
                              "@blockOnEstLocal", flg.blockOnEstLocal
                              );
        }

        private void saveOperator(Operator o)
        {
            oDB_SP.EjecutarSP("[FLX].[spI_operator]",
                              "@IdOperator", o.Id,
                              "@Name", o.name
                              );
        }

        internal void saveAircraft(AircraftAOC airc)
        {
            oDB_SP.EjecutarSP("[FLX].[spI_aircraftAOC]",
                              "@IdaircraftAOC", airc.Id,
                              "@Name", airc.name
                              );
        }

        internal void saveLinks(Links link)
        {
            oDB_SP.EjecutarSP("[FLX].[spI_Flights]",
                              "@IdpaxReferences", link.IdpaxReferences,
                              "@deprecation", link.deprecation,
                              "@href", link.href,
                              "@hreflang", link.hreflang,
                              "@media", link.media,
                              "@rel", link.rel,
                              "@templated", link.templated,
                              "@title", link.title,
                              "@type", link.type
                              );
        }

        internal DataSet savePaxReferences(PaxReferences px)
        {
            var msj = oDB_SP.EjecutarDS("[FLX].[spI_paxReferences]",
                              "@comment", px.comment,
                              "@documentExternalReference", px.documentExternalReference,
                              "@externalReference", px.externalReference,
                              "@internalId", px.internalId,
                              "@isMain", px.isMain,
                              "@paxExternalReference", px.paxExternalReference,
                              "@paxType", px.paxType,
                              "@flightId", px.flightId
                              );

            return msj;
        }

        internal void updateFlights(groupFlights gf)
        {
            oDB_SP.EjecutarSP("[FLX].[spU_Flights]",
                              "@consecutivoVuelo", gf.noVuelo,
                              "@esGrupo", gf.esGrupo,
                              "@flightId", gf.flightId,
                              "@tripNumber", gf.tripNumber
                              );
        }

        internal DataSet savePostFlight(PostFlightFlexx p)
        {
            var msj = oDB_SP.EjecutarDS("[FLX].[spI_PostFlights]",
                                         "@aircraftId", p.AircraftId,
                                         "@booking", p.Booking,
                                         "@bookingId", p.BookingId,
                                         "@endHobbs", p.EndHobbs,
                                         "@flight", p.Flight,
                                         "@ftlComments", p.FtlComments,
                                         "@hold", p.Hold,
                                         "@precision", p.Precision,
                                         "@reducedRestEnabled", p.ReducedRestEnabled,
                                         "@startHobbs", p.StartHobbs,
                                         "@tailNumber", p.TailNumber,
                                         "@visual", p.Visual,
                                         "@flightId", p.FlightId
                                         );

            return msj;
        }

        internal void saveArrFuel(ArrFuel af)
        {
            try
            {
                var x = oDB_SP.EjecutarDS("[FLX].[spI_arrFuel]",
                               "@IdarrFuel", af.Id,
                               "@Idpostflight", af.Idpostflight,
                               "@agentId", af.AgentId,
                               "@airportId", af.AirportId,
                               "@by", af.By,
                               "@cancellationRequired", af.CancellationRequired,
                               "@creditCardExpiration", af.CreditCardExpiration,
                               "@creditCardNumber", af.CreditCardNumber,
                               "@currency", af.Currency,
                               "@deliveryDate", getfecha(af.DeliveryDate.Value),
                               "@density", af.Density,
                               "@externalNote", af.ExternalNote,
                               "@externalSource", af.ExternalSource,
                               "@externalStatus", af.ExternalStatus,
                               "@fplType", af.FplType,
                               "@fuelArrival", af.FuelArrival,
                               "@fuelBurned", af.FuelBurned,
                               "@fuelCost", af.FuelCost,
                               "@fuelCurrency", af.FuelCurrency,
                               "@fuelPriceIndex", af.FuelPriceIndex,
                               "@fuelPriceIndexCommercial", af.FuelPriceIndexCommercial,
                               "@fuelProvider", af.FuelProvider,
                               "@fuelRemainig", af.FuelRemainig,
                               "@fuelRemainigActual", af.FuelRemainigActual,
                               "@fuelReqReleaseSent", af.FuelReqReleaseSent,
                               "@fuelReqSent", af.FuelReqSent,
                               "@fuelTankUnit", af.FuelTankUnit,
                               "@fuelType", af.FuelType,
                               "@notes", af.Notes,
                               "@payment", af.Payment,
                               "@paymentType", af.PaymentType,
                               "@permitProviderId", af.PermitProviderId,
                               "@poCreatedDate", af.PoCreatedDate,
                               "@poNumber", af.PoNumber,
                               "@price", af.Price,
                               "@priceComercial", af.PriceComercial,
                               "@pricePerUnit", af.PricePerUnit,
                               "@pricePerUnitComercial", af.PricePerUnitComercial,
                               "@providerId", af.ProviderId,
                               "@quantity", af.Quantity,
                               "@quoteOnly", af.QuoteOnly,
                               "@releaseAgentId", af.ReleaseAgentId,
                               "@releaseDocument", af.ReleaseDocument,
                               "@status", af.Status,
                               "@unitDensity", af.UnitDensity,
                               "@updatedDate", af.UpdatedDate,
                               "@uplift", af.Uplift,
                               "@upliftMass", af.UpliftMass,
                               "@upliftUnit", af.UpliftUnit);
            }
            catch (Exception ex)
            {
            }
        }

        internal DataSet getFlightsValidation()
        {
            try
            {
                return oDB_SP.EjecutarDS("[FLX].[spS_FlightsValidation]");

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        internal void saveFo(Fo f)
        {
            oDB_SP.EjecutarSP("[FLX].[spI_fo]",
                              "@Idfo", f.Idfo,
                              "@Idtime", f.Idtime,
                              "@accountName", f.AccountName,
                              "@firstName", f.FirstName,
                              "@height", f.Height,
                              "@jobTitle", f.JobTitle,
                              "@lastName", f.LastName,
                              "@middleName", f.MiddleName,
                              "@nickname", f.Nickname,
                              "@Idoperator", f.Idoperator,
                              "@personnelNumber", f.PersonnelNumber,
                              "@pilot", f.Pilot,
                              "@status", f.Status,
                              "@weight", f.Weight
                              );
        }

        internal void saveFlight(FlightTime ft)
        {
            oDB_SP.EjecutarDS("[FLX].[spI_flight]",
                              "@Idflight", ft.Idflight,
                              "@Idtime", ft.Idtime,
                              "@acMeter", ft.AcMeter,
                              "@aircraftExtraEnginesAcmCycles", ft.AircraftExtraEnginesAcmCycles,
                              "@aircraftExtraEnginesAcmHours", ft.AircraftExtraEnginesAcmHours,
                              "@aircraftExtraEnginesApuCycles", ft.AircraftExtraEnginesApuCycles,
                              "@aircraftExtraEnginesApuHours", ft.AircraftExtraEnginesApuHours,
                              "@aircraftExtraSysCcyCyclesLh", ft.AircraftExtraSysCcyCyclesLh,
                              "@aircraftExtraSysCcyCyclesRh", ft.AircraftExtraSysCcyCyclesRh,
                              "@aircraftExtraSysIcyCyclesLh", ft.AircraftExtraSysIcyCyclesLh,
                              "@aircraftExtraSysIcyCyclesRh", ft.AircraftExtraSysIcyCyclesRh,
                              "@aircraftExtraSysPcyCyclesLh", ft.AircraftExtraSysPcyCyclesLh,
                              "@aircraftExtraSysPcyCyclesRh", ft.AircraftExtraSysPcyCyclesRh,
                              "@aircraftTotalExtraEnginesAcmCycles", ft.AircraftTotalExtraEnginesAcmCycles,
                              "@aircraftTotalExtraEnginesAcmHours", ft.AircraftTotalExtraEnginesAcmHours,
                              "@aircraftTotalExtraEnginesApuCycles", ft.AircraftTotalExtraEnginesApuCycles,
                              "@aircraftTotalExtraEnginesApuHours", ft.AircraftTotalExtraEnginesApuHours,
                              "@aircraftTotalExtraSysCcyCyclesLh", ft.AircraftTotalExtraSysCcyCyclesLh,
                              "@aircraftTotalExtraSysCcyCyclesRh", ft.AircraftTotalExtraSysCcyCyclesRh,
                              "@aircraftTotalExtraSysIcyCyclesLh", ft.AircraftTotalExtraSysIcyCyclesLh,
                              "@aircraftTotalExtraSysIcyCyclesRh", ft.AircraftTotalExtraSysIcyCyclesRh,
                              "@aircraftTotalExtraSysPcyCyclesLh", ft.AircraftTotalExtraSysPcyCyclesLh,
                              "@aircraftTotalExtraSysPcyCyclesRh", ft.AircraftTotalExtraSysPcyCyclesRh,
                              "@apuOilUplift", ft.ApuOilUplift,
                              "@cOilUplift", ft.COilUplift,
                              "@endHobbs", ft.EndHobbs,
                              "@engine1Cycles", ft.Engine1Cycles,
                              "@engine2Cycles", ft.Engine2Cycles,
                              "@engine3Cycles", ft.Engine3Cycles,
                              "@engine4Cycles", ft.Engine4Cycles,
                              "@engineCycles", ft.EngineCycles,
                              "@engineNr", ft.EngineNr,
                              "@extraEnginesAcm", ft.ExtraEnginesAcm,
                              "@extraEnginesAcmCycles", ft.ExtraEnginesAcmCycles,
                              "@extraEnginesAcmHours", ft.ExtraEnginesAcmHours,
                              "@extraEnginesApu", ft.ExtraEnginesApu,
                              "@extraEnginesApuCycles", ft.ExtraEnginesApuCycles,
                              "@extraEnginesApuHours", ft.ExtraEnginesApuHours,
                              "@extraSysCcyCyclesLh", ft.ExtraSysCcyCyclesLh,
                              "@extraSysCcyCyclesRh", ft.ExtraSysCcyCyclesRh,
                              "@extraSysIcyCyclesLh", ft.ExtraSysIcyCyclesLh,
                              "@extraSysIcyCyclesRh", ft.ExtraSysIcyCyclesRh,
                              "@extraSysPcyCyclesLh", ft.ExtraSysPcyCyclesLh,
                              "@extraSysPcyCyclesRh", ft.ExtraSysPcyCyclesRh,
                              "@extraSystemCcy", ft.ExtraSystemCcy,
                              "@extraSystemHobbs", ft.ExtraSystemHobbs,
                              "@extraSystemIcy", ft.ExtraSystemIcy,
                              "@extraSystemPcy", ft.ExtraSystemPcy,
                              "@lOilUplift", ft.LOilUplift,
                              "@nightFlightTime", ft.NightFlightTime,
                              "@previousFlightEndHobbsPresent", ft.PreviousFlightEndHobbsPresent,
                              "@r2OilUplift", ft.R2OilUplift,
                              "@rOilUplift", ft.ROilUplift,
                              "@startHobbs", ft.StartHobbs,
                              "@techlogNumber", ft.TechlogNumber,
                              "@totalEngineCycles1", ft.TotalEngineCycles1,
                              "@totalEngineCycles2", ft.TotalEngineCycles2,
                              "@totalEngineCycles3", ft.TotalEngineCycles3,
                              "@totalEngineCycles4", ft.TotalEngineCycles4,
                              "@totalFlightHours", ft.TotalFlightHours,
                              "@totalLandings", ft.TotalLandings);
        }

        internal void updateFlightStatus(int flightId)
        {
            oDB_SP.EjecutarSP("[FLX].[spU_FlightStatusBitacora]",
                             "@flightId", flightId
                             );
        }

        internal void saveDep(Dep d)
        {
            oDB_SP.EjecutarSP("[FLX].[spI_dep]",
                              "@Idtime", d.Idtime,
                              "@blocksOff", d.BlocksOff.HasValue ? getfecha(d.BlocksOff.Value) : null,
                              "@cmdCheckin", d.CmdCheckin,
                              "@cmdLoggedTakeOffs", d.CmdLoggedTakeOffs,
                              "@cmdStartTime", d.CmdStartTime.HasValue ? getfecha(d.CmdStartTime.Value) : null,
                              "@cmdTakeOffs", d.CmdTakeOffs,
                              "@cmdTakeOffsNight", d.CmdTakeOffsNight,
                              "@delayOffBlockReason", d.DelayOffBlockReason,
                              "@delayTakeOffReason", d.DelayTakeOffReason,
                              "@foCheckin", d.FoCheckin,
                              "@foLoggedTakeOffs", d.FoLoggedTakeOffs,
                              "@foStartTime", d.FoStartTime.HasValue ? getfecha(d.FoStartTime.Value) : null,
                              "@foTakeOffs", d.FoTakeOffs,
                              "@foTakeOffsNight", d.FoTakeOffsNight,
                              "@pax", d.Pax,
                              "@takeOff", d.TakeOff.HasValue ? getfecha(d.TakeOff.Value) : null
                              );
        }

        internal void saveArr(Arr a)
        {
            oDB_SP.EjecutarSP("[FLX].[spI_arr]",
                              //"@Idarr", a.Idarr,
                              "@Idtime", a.Idtime,
                              //"@approachType", a.approachType,
                              "@blocksOn", a.BlocksOn.HasValue ? getfecha(a.BlocksOn.Value) : null,
                              //"@cmdCheckout", a.cmdCheckout,
                              //"@cmdClose", a.cmdClose.HasValue ? getfecha(a.cmdClose.Value) : null,
                              "@cmdDayLandings", a.CmdDayLandings,
                              //"@cmdDutyTime", a.cmdDutyTime,
                              //"@cmdEfvs", a.cmdEfvs,
                              //"@cmdFdpExtension", a.cmdFdpExtension,
                              //"@cmdHold", a.cmdHold,
                              //"@cmdInstTime", a.cmdInstTime,
                              //"@cmdLoggedLandings", a.cmdLoggedLandings,
                              "@cmdNightLandings", a.CmdNightLandings,
                              //"@cmdNightTime", a.cmdNightTime,
                              //"@cmdNonPrecision", a.cmdNonPrecision,
                              "@cmdPicTime", a.CmdPicTime,
                              //"@cmdPostFlightRestIncrease", a.cmdPostFlightRestIncrease,
                              //"@cmdPrecision", a.cmdPrecision,
                              //"@cmdSicTime", a.cmdSicTime,
                              //"@cmdSplitDutyClose", a.cmdSplitDutyClose,
                              //"@cmdSplitDutyCloseTime", a.cmdSplitDutyCloseTime.HasValue ? getfecha(a.cmdSplitDutyCloseTime.Value) : null,
                              //"@cmdSplitDutyStart", a.cmdSplitDutyStart,
                              //"@cmdSplitDutyStartTime", a.cmdSplitDutyStartTime.HasValue ? getfecha(a.cmdSplitDutyStartTime.Value) : null,
                              //"@cmdUseReducedRest", a.cmdUseReducedRest,
                              //"@cmdVisual", a.cmdVisual,
                              //"@delayLandingReason", a.delayLandingReason,
                              //"@delayOnBlockReason", a.delayOnBlockReason,
                              //"@foCheckout", a.foCheckout,
                              //"@foClose", a.foClose.HasValue ? getfecha(a.foClose.Value) : null,
                              "@foDayLandings", a.FoDayLandings,
                              //"@foDutyTime", a.foDutyTime,
                              //"@foEfvs", a.foEfvs,
                              //"@foFdpExtension", a.foFdpExtension,
                              //"@foHold", a.foHold,
                              //"@foInstTime", a.foInstTime,
                              //"@foLoggedLandings", a.foLoggedLandings,
                              "@foNightLandings", a.FoNightLandings,
                              //"@foNightTime", a.foNightTime,
                              //"@foNonPrecision", a.foNonPrecision,
                              //"@foPicTime", a.foPicTime,
                              //"@foPostFlightRestIncrease", a.foPostFlightRestIncrease,
                              //"@foPrecision", a.foPrecision,
                              "@foSicTime", a.FoSicTime,
                              //"@foSplitDutyClose", a.foSplitDutyClose,
                              //"@foSplitDutyCloseTime", a.foSplitDutyCloseTime.HasValue ? getfecha(a.foSplitDutyCloseTime.Value) : null,
                              //"@foSplitDutyStart", a.foSplitDutyStart,
                              //"@foSplitDutyStartTime", a.foSplitDutyStartTime.HasValue ? getfecha(a.foSplitDutyStartTime.Value) : null,
                              //"@foUseReducedRest", a.foUseReducedRest,
                              //"@foVisual", a.foVisual,
                              "@landing", a.Landing.HasValue ? getfecha(a.Landing.Value) : null
                              );
        }

        internal void saveFuel(Fuel fu)
        {
            oDB_SP.EjecutarDS("[FLX].[spI_fuel]",
                              "@Idfuel", fu.Id,
                              "@Idpostflight", fu.Idpostflight,
                              "@agentId", fu.AgentId,
                              "@airportId", fu.AirportId,
                              "@by", fu.By,
                              "@cancellationRequired", fu.CancellationRequired,
                              "@creditCardExpiration", fu.CreditCardExpiration,
                              "@creditCardNumber", fu.CreditCardNumber,
                              "@currency", fu.Currency,
                              "@deliveryDate", getfecha(fu.DeliveryDate),
                              "@density", fu.Density,
                              "@externalNote", fu.ExternalNote,
                              "@externalSource", fu.ExternalSource,
                              "@externalStatus", fu.ExternalStatus,
                              "@fplType", fu.FplType,
                              "@fuelArrival", fu.FuelArrival,
                              "@fuelBurned", fu.FuelBurned,
                              "@fuelCost", fu.FuelCost,
                              "@fuelCurrency", fu.FuelCurrency,
                              "@fuelPriceIndex", fu.FuelPriceIndex,
                              "@fuelPriceIndexCommercial", fu.FuelPriceIndexCommercial,
                              "@fuelProvider", fu.FuelProvider,
                              "@fuelRemainig", fu.FuelRemainig,
                              "@fuelRemainigActual", fu.FuelRemainigActual,
                              "@fuelReqReleaseSent", fu.FuelReqReleaseSent,
                              "@fuelReqSent", fu.FuelReqSent,
                              "@fuelTankUnit", fu.FuelTankUnit,
                              "@fuelType", fu.FuelType,
                              "@notes", fu.Notes,
                              "@payment", fu.Payment,
                              "@paymentType", fu.PaymentType,
                              "@permitProviderId", fu.PermitProviderId,
                              "@poCreatedDate", fu.PoCreatedDate,
                              "@poNumber", fu.PoNumber,
                              "@price", fu.Price,
                              "@priceComercial", fu.PriceComercial,
                              "@pricePerUnit", fu.PricePerUnit,
                              "@pricePerUnitComercial", fu.PricePerUnitComercial,
                              "@providerId", fu.ProviderId,
                              "@quantity", fu.Quantity,
                              "@quoteOnly", fu.QuoteOnly,
                              "@releaseAgentId", fu.ReleaseAgentId,
                              "@releaseDocument", fu.ReleaseDocument,
                              "@status", fu.Status,
                              "@unitDensity", fu.UnitDensity,
                              "@updatedDate", fu.UpdatedDate,
                              "@uplift", fu.Uplift,
                              "@upliftMass", fu.UpliftMass,
                              "@upliftUnit", fu.UpliftUnit
                                         );

        }

        internal void saveFuelRealse(FuelRelease fr)
        {
            oDB_SP.EjecutarDS("[FLX].[spI_fuelRelease]",
                              "@IdfuelRelease", fr.Id,
                              "@IdarrFuel", fr.IdarrFuel,
                              "@Idfuel", fr.Idfuel,
                              "@customName", fr.CustomName,
                              "@entityName", fr.EntityName,
                              "@fileSize", fr.FileSize,
                              "@imageSizeX", fr.ImageSizeX,
                              "@imageSizeY", fr.ImageSizeY,
                              "@isThumbnail", fr.IsThumbnail,
                              "@name", fr.Name,
                              "@originalName", fr.OriginalName,
                              "@path", fr.Path,
                              "@refEntityId", fr.RefEntityId,
                              "@status", fr.Status,
                              "@thumbnail", fr.Thumbnail,
                              "@uuid", fr.Uuid);
        }

        internal void savePreFlight(PreFlight pf)
        {
            oDB_SP.EjecutarSP("[FLX].[spI_preFlight]",
                              "@Idpostflight", pf.Idpostflight,
                              "@IdpreFlight", pf.Id,
                              "@by", pf.By,
                              "@fplType", pf.FplType,
                              "@fuelTankUnit", pf.FuelTankUnit,
                              "@status", pf.Status
                              );
        }

        internal void saveRealse(Release re)
        {
            oDB_SP.EjecutarDS("[FLX].[spI_release]",
                              "@Idrelease", re.Id,
                              "@Idfuel", re.Idfuel,
                              "@IdarrFuel", re.IdarrFuel,
                              "@by", re.By,
                              "@status", re.Status);
        }

        internal void saveTrafficReportPanel(TrafficReportPanel tr)
        {
            oDB_SP.EjecutarSP("[FLX].[spI_trafficReportPanel]",
                              "@Idpostflight", tr.Idpostflight,
                              "@dossierNumber", tr.DossierNumber,
                              "@flightType", tr.FlightType,
                              "@referenceNumber", tr.ReferenceNumber
                              );
        }

        internal void saveEstimatedTimes(EstimatedTimes es)
        {
            oDB_SP.EjecutarSP("[FLX].[spI_estimatedTimes]",
                              "@Idpostflight", es.Idpostflight,
                              "@blocksOff", getfecha(es.BlocksOff.Value),
                              "@blocksOn", getfecha(es.BlocksOn.Value),
                              "@estimatedPostflightPhase", es.EstimatedPostflightPhase,
                              "@estimatedPreflightPhase", es.EstimatedPreflightPhase,
                              "@landing", getfecha(es.Landing.Value),
                              "@takeOff", getfecha(es.TakeOff.Value)
                              );
        }

        internal void saveUserCharacteristics(UserCharacteristics us)
        {
            oDB_SP.EjecutarSP("[FLX].[spI_userCharacteristics]",
                              "@Idcrew", us.Idcrew,
                              "@IddeIcingSignedBy", us.IddeIcingSignedBy,
                              "@Idfo", us.Idfo,
                              "@Idcmd", us.Idcmd,
                              "@defaultLuggageWeight", us.DefaultLuggageWeight,
                              "@eyeColor", us.EyeColor,
                              "@hairColor", us.HairColor
                              );
        }

        internal void saveCmd(Cmd c)
        {
            oDB_SP.EjecutarSP("[FLX].[spI_cmd]",
                              "@Idcmd", c.Id,
                              "@Idtime", c.Idtime,
                              "@accountName", c.AccountName,
                              "@firstName", c.FirstName,
                              "@height", c.Height,
                              "@jobTitle", c.JobTitle,
                              "@lastName", c.LastName,
                              "@middleName", c.MiddleName,
                              "@nickname", c.Nickname,
                              "@Idoperator", c.Idoperator,
                              "@personnelNumber", c.PersonnelNumber,
                              "@pilot", c.Pilot,
                              "@status", c.Status,
                              "@weight", c.Weight
                              );
        }

        internal DataSet saveTime(Time t)
        {
            var msj = oDB_SP.EjecutarDS("[FLX].[spI_time]",
                                         "@Idpostflight", t.Idpostflight,
                                         "@foIsCmd", t.FoIsCmd,
                                         "@foOpenFdp", t.FoOpenFdp,
                                         "@pilotFlying", t.PilotFlying,
                                         "@splitDutyType", t.SplitDutyType,
                                         "@status", t.Status,
                                         "@by", t.By,
                                         "@cmdOpenFdp", t.CmdOpenFdp
                                         );

            return msj;
        }

        #endregion
    }
}