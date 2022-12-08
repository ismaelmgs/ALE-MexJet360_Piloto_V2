using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{

    [Serializable]
    public class FlightsFlexx
    {
        private string _accountReference;
        private string _aircraftCategory;
        private string _airportFrom;
        private string _airportTo;
        private DateTime? _blocksoffestimated;
        private DateTime? _blocksonestimated;
        private string _bookingIdentifier;
        private string _bookingReference;
        private string _customerFirstname;
        private string _customerId;
        private string _customerLastname;
        private string _customerTrigram;
        private DateTime? _dateFrom;
        private DateTime? _dateTo;
        private int? _flightId;
        private string _flightNumber;
        private string _flightNumberCompany;
        private string _flightStatus;
        private string _flightType;
        private string _fplType;
        private decimal? _fuelArrival;
        private string _fuelMassUnit;
        private decimal? _fuelOffBlock;
        private decimal? _fuelRemainigActual;
        private int _IdaircraftAOC;
        private AircraftAOC _aircraftAOC;
        private int _Idoperator;
        private Operator _operator;
        private List<PaxReferences> _paxReferences;
        private string _paxNumber;
        private string _postFlightClosed;
        private string _realAirportFrom;
        private string _realAirportTo;
        private DateTime? _realDateIN;
        private DateTime? _realDateOFF;
        private DateTime? _realDateON;
        private DateTime? _realDateOUT;
        private string _registrationNumber;
        private string _requestedAircraftType;
        private DateTime? _rescheduledDateFrom;
        private DateTime? _rescheduledDateTo;
        private string _status;
        private int? _tripNumber;
        private decimal? _upliftMass;
        private string _userReference;
        private string _workflow;
        private string _workflowCustomName;
        private bool _statusBitacora;
        private bool _esGrupo;
        private int _consecutivoVuelo;
        private bool _InsertarBitacora;
        private DateTime? _blockOffEstUTC;
        private DateTime? _blockOnEstUTC;
        private DateTime? _blockOffEstLocal;
        private DateTime? _blockOnEstLocal;

        public string accountReference { get => _accountReference; set => _accountReference = value; }
        public string aircraftCategory { get => _aircraftCategory; set => _aircraftCategory = value; }
        public string airportFrom { get => _airportFrom; set => _airportFrom = value; }
        public string airportTo { get => _airportTo; set => _airportTo = value; }
        public DateTime? blocksoffestimated { get => _blocksoffestimated; set => _blocksoffestimated = value; }
        public DateTime? blocksonestimated { get => _blocksonestimated; set => _blocksonestimated = value; }
        public string bookingIdentifier { get => _bookingIdentifier; set => _bookingIdentifier = value; }
        public string bookingReference { get => _bookingReference; set => _bookingReference = value; }
        public string customerFirstname { get => _customerFirstname; set => _customerFirstname = value; }
        public string customerId { get => _customerId; set => _customerId = value; }
        public string customerLastname { get => _customerLastname; set => _customerLastname = value; }
        public string customerTrigram { get => _customerTrigram; set => _customerTrigram = value; }
        public DateTime? dateFrom { get => _dateFrom; set => _dateFrom = value; }
        public DateTime? dateTo { get => _dateTo; set => _dateTo = value; }
        public int? flightId { get => _flightId; set => _flightId = value; }
        public string flightNumber { get => _flightNumber; set => _flightNumber = value; }
        public string flightNumberCompany { get => _flightNumberCompany; set => _flightNumberCompany = value; }
        public string flightStatus { get => _flightStatus; set => _flightStatus = value; }
        public string flightType { get => _flightType; set => _flightType = value; }
        public string fplType { get => _fplType; set => _fplType = value; }
        public decimal? fuelArrival { get => _fuelArrival; set => _fuelArrival = value; }
        public string fuelMassUnit { get => _fuelMassUnit; set => _fuelMassUnit = value; }
        public decimal? fuelOffBlock { get => _fuelOffBlock; set => _fuelOffBlock = value; }
        public decimal? fuelRemainigActual { get => _fuelRemainigActual; set => _fuelRemainigActual = value; }
        public int IdaircraftAOC { get => _IdaircraftAOC; set => _IdaircraftAOC = value; }
        public AircraftAOC aircraftAOC { get => _aircraftAOC; set => _aircraftAOC = value; }
        public int Idoperator { get => _Idoperator; set => _Idoperator = value; }
        public Operator @operator { get => _operator; set => _operator = value; }
        public List<PaxReferences> paxReferences { get => _paxReferences; set => _paxReferences = value; }
        public string paxNumber { get => _paxNumber; set => _paxNumber = value; }
        public string postFlightClosed { get => _postFlightClosed; set => _postFlightClosed = value; }
        public string realAirportFrom { get => _realAirportFrom; set => _realAirportFrom = value; }
        public string realAirportTo { get => _realAirportTo; set => _realAirportTo = value; }
        public DateTime? realDateIN { get => _realDateIN; set => _realDateIN = value; }
        public DateTime? realDateOFF { get => _realDateOFF; set => _realDateOFF = value; }
        public DateTime? realDateON { get => _realDateON; set => _realDateON = value; }
        public DateTime? realDateOUT { get => _realDateOUT; set => _realDateOUT = value; }
        public string registrationNumber { get => _registrationNumber; set => _registrationNumber = value; }
        public string requestedAircraftType { get => _requestedAircraftType; set => _requestedAircraftType = value; }
        public DateTime? rescheduledDateFrom { get => _rescheduledDateFrom; set => _rescheduledDateFrom = value; }
        public DateTime? rescheduledDateTo { get => _rescheduledDateTo; set => _rescheduledDateTo = value; }
        public string status { get => _status; set => _status = value; }
        public int? tripNumber { get => _tripNumber; set => _tripNumber = value; }
        public decimal? upliftMass { get => _upliftMass; set => _upliftMass = value; }
        public string userReference { get => _userReference; set => _userReference = value; }
        public string workflow { get => _workflow; set => _workflow = value; }
        public string workflowCustomName { get => _workflowCustomName; set => _workflowCustomName = value; }
        public bool statusBitacora { get => _statusBitacora; set => _statusBitacora = value; }
        public bool esGrupo { get => _esGrupo; set => _esGrupo = value; }
        public int consecutivoVuelo { get => _consecutivoVuelo; set => _consecutivoVuelo = value; }
        public bool InsertarBitacora { get => _InsertarBitacora; set => _InsertarBitacora = value; }
        public DateTime? blockOffEstUTC { get => _blockOffEstUTC; set => _blockOffEstUTC = value; }
        public DateTime? blockOnEstUTC { get => _blockOnEstUTC; set => _blockOnEstUTC = value; }
        public DateTime? blockOffEstLocal { get => _blockOffEstLocal; set => _blockOffEstLocal = value; }
        public DateTime? blockOnEstLocal { get => _blockOnEstLocal; set => _blockOnEstLocal = value; }
    }

    [Serializable]
    public class Operator
    {
        private int? _Id;
        private string _name;

        public int Id { get => _Id.Value; set => _Id = value; }
        public string name { get => _name; set => _name = value; }

    }

    [Serializable]
    public class AircraftAOC
    {
        private int? _Id;
        private string _name;

        public int Id { get => _Id.Value; set => _Id = value; }
        public string name { get => _name; set => _name = value; }
    }

    [Serializable]
    public class PaxReferences
    {
        private int? _IdpaxReferences;
        private string _comment;
        private string _documentExternalReference;
        private string _externalReference;
        private bool? _isMain;
        private string _paxExternalReference;
        private string _paxType;
        private int? _internalId;
        private int? _flightId;
        private List<Links> _links;

        public int? IdpaxReferences { get => _IdpaxReferences; set => _IdpaxReferences = value; }
        public string comment { get => _comment; set => _comment = value; }
        public string documentExternalReference { get => _documentExternalReference; set => _documentExternalReference = value; }
        public string externalReference { get => _externalReference; set => _externalReference = value; }
        public bool isMain { get => _isMain.Value; set => _isMain = value; }
        public string paxExternalReference { get => _paxExternalReference; set => _paxExternalReference = value; }
        public string paxType { get => _paxType; set => _paxType = value; }
        public int? internalId { get => _internalId; set => _internalId = value; }
        public int? flightId { get => _flightId; set => _flightId = value; }
        public List<Links> links { get => _links; set => _links = value; }
    }

    [Serializable]
    public class Links
    {
        private int? _IdLink;
        private string _deprecation;
        private string _href;
        private string _hreflang;
        private string _media;
        private string _rel;
        private string _templated;
        private string _title;
        private string _type;
        private int? _IdpaxReferences;

        public int? IdLink { get => _IdLink; set => _IdLink = value; }
        public string deprecation { get => _deprecation; set => _deprecation = value; }
        public string href { get => _href; set => _href = value; }
        public string hreflang { get => _hreflang; set => _hreflang = value; }
        public string media { get => _media; set => _media = value; }
        public string rel { get => _rel; set => _rel = value; }
        public string templated { get => _templated; set => _templated = value; }
        public string title { get => _title; set => _title = value; }
        public string type { get => _type; set => _type = value; }
        public int? IdpaxReferences { get => _IdpaxReferences; set => _IdpaxReferences = value; }
    }

    [Serializable]
    public class groupFlights
    {
        private int? _tripNumber;
        private int _flightId;
        private bool _esGrupo; //si pertenece a un grupo de vuelos
        private int _noVuelo;

        public int tripNumber { get => _tripNumber.Value; set => _tripNumber = value; }
        public int flightId { get => _flightId; set => _flightId = value; }
        public bool esGrupo { get => _esGrupo; set => _esGrupo = value; }
        public int noVuelo { get => _noVuelo; set => _noVuelo = value; }
    }
}