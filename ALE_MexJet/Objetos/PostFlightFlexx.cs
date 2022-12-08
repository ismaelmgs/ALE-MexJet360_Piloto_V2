using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    [Serializable]
    public class PostFlightFlexx
    {
        private int? _Idpostflight;
        private int? _aircraftId;
        private int flightId;
        private string booking;
        private int? _bookingId;
        private int? _endHobbs;
        private string flight;
        private string ftlComments;
        private bool? _hold;
        private int? _precision;
        private bool? _reducedRestEnabled;
        private decimal? _startHobbs;
        private string tailNumber;
        private bool? _visual;
        private ArrFuel _arrFuel;
        private Cargo _cargo;
        private Deice _deice;
        private Documents _documents;
        private EstimatedTimes _estimatedTimes;
        private Fuel fuel;
        private List<FuelProviders> _fuelProviders;
        private PreFlight _preFlight;
        private Time _time;
        private TrafficReportPanel _trafficReportPanel;

        public int? Idpostflight { get => _Idpostflight; set => _Idpostflight = value; }
        public int? AircraftId { get => _aircraftId; set => _aircraftId = value; }
        public int FlightId { get => flightId; set => flightId = value; }
        public string Booking { get => booking; set => booking = value; }
        public int? BookingId { get => _bookingId; set => _bookingId = value; }
        public int? EndHobbs { get => _endHobbs; set => _endHobbs = value; }
        public string Flight { get => flight; set => flight = value; }
        public string FtlComments { get => ftlComments; set => ftlComments = value; }
        public bool? Hold { get => _hold; set => _hold = value; }
        public int? Precision { get => _precision; set => _precision = value; }
        public bool? ReducedRestEnabled { get => _reducedRestEnabled; set => _reducedRestEnabled = value; }
        public decimal? StartHobbs { get => _startHobbs; set => _startHobbs = value; }
        public string TailNumber { get => tailNumber; set => tailNumber = value; }
        public bool? Visual { get => _visual; set => _visual = value; }
        public ArrFuel ArrFuel { get => _arrFuel; set => _arrFuel = value; }
        public Cargo Cargo { get => _cargo; set => _cargo = value; }
        public Deice Deice { get => _deice; set => _deice = value; }
        public Documents Documents { get => _documents; set => _documents = value; }
        public EstimatedTimes EstimatedTimes { get => _estimatedTimes; set => _estimatedTimes = value; }
        public Fuel Fuel { get => fuel; set => fuel = value; }
        public List<FuelProviders> FuelProviders { get => _fuelProviders; set => _fuelProviders = value; }
        public PreFlight PreFlight { get => _preFlight; set => _preFlight = value; }
        public Time Time { get => _time; set => _time = value; }
        public TrafficReportPanel TrafficReportPanel { get => _trafficReportPanel; set => _trafficReportPanel = value; }
    }

    [Serializable]
    public class TrafficReportPanel
    {
        private int? _IdtrafficReportPanel;
        private int? _Idpostflight;
        private string dossierNumber;
        private string flightType;
        private string referenceNumber;

        public int? IdtrafficReportPanel { get => _IdtrafficReportPanel; set => _IdtrafficReportPanel = value; }
        public int? Idpostflight { get => _Idpostflight; set => _Idpostflight = value; }
        public string DossierNumber { get => dossierNumber; set => dossierNumber = value; }
        public string FlightType { get => flightType; set => flightType = value; }
        public string ReferenceNumber { get => referenceNumber; set => referenceNumber = value; }
    }

    [Serializable]
    public class Time
    {
        private int? _id;
        private int? _Idpostflight;
        private bool? _foIsCmd;
        private int? _foOpenFdp;
        private int? _pilotFlying;
        private string splitDutyType;
        private string status;
        private string by;
        private int? _cmdOpenFdp;
        private List<Add_crew> _add_crew;
        private Arr _arr;
        private Cmd _cmd;
        private Dep _dep;
        private List<Dtls2> _dtls2;
        private List<Fa> _fa;
        private FlightTime _flight;
        private Fo _fo;
        private List<OnboardEngineers> _onboardEngineers;

        public int? Id { get => _id; set => _id = value; }
        public int? Idpostflight { get => _Idpostflight; set => _Idpostflight = value; }
        public bool? FoIsCmd { get => _foIsCmd; set => _foIsCmd = value; }
        public int? FoOpenFdp { get => _foOpenFdp; set => _foOpenFdp = value; }
        public int? PilotFlying { get => _pilotFlying; set => _pilotFlying = value; }
        public string SplitDutyType { get => splitDutyType; set => splitDutyType = value; }
        public string Status { get => status; set => status = value; }
        public string By { get => by; set => by = value; }
        public int? CmdOpenFdp { get => _cmdOpenFdp; set => _cmdOpenFdp = value; }
        public List<Add_crew> Add_crew { get => _add_crew; set => _add_crew = value; }
        public Arr Arr { get => _arr; set => _arr = value; }
        public Cmd Cmd { get => _cmd; set => _cmd = value; }
        public Dep Dep { get => _dep; set => _dep = value; }
        public List<Dtls2> Dtls2 { get => _dtls2; set => _dtls2 = value; }
        public List<Fa> Fa { get => _fa; set => _fa = value; }
        public FlightTime Flight { get => _flight; set => _flight = value; }
        public Fo Fo { get => _fo; set => _fo = value; }
        public List<OnboardEngineers> OnboardEngineers { get => _onboardEngineers; set => _onboardEngineers = value; }
    }

    [Serializable]
    public class OnboardEngineers
    {
        private int? _IdonboardEngineers;
        private int? _Idtime;
        private DateTime? _closeTime;
        private bool? _closed;
        private int? _efvs;
        private bool? _fdpExtension;
        private string firstName;
        private DateTime? _from;
        private bool? _hold;
        private int? _instTime;
        private int? _landings;
        private string lastName;
        private int? _loggedLandings;
        private int? _loggedTakeOffs;
        private string nickname;
        private int? _nightLandings;
        private int? _nightTakeOffs;
        private int? _nightTime;
        private int? _nonPrecision;
        private int? _picTime;
        private string pilotRole;
        private bool? _postFlightRestIncrease;
        private int? _precision;
        private DateTime? _reportTime;
        private bool? _reported;
        private int? _sicTime;
        private bool? _splitDutyClose;
        private DateTime? _splitDutyCloseTime;
        private bool? _splitDutyStart;
        private DateTime? _splitDutyStartTime;
        private int? _takeOffs;
        private int? _to;
        private bool? _useReducedRest;
        private bool? _visual;

        public int? IdonboardEngineers { get => _IdonboardEngineers; set => _IdonboardEngineers = value; }
        public int? Idtime { get => _Idtime; set => _Idtime = value; }
        public DateTime? CloseTime { get => _closeTime; set => _closeTime = value; }
        public bool? Closed { get => _closed; set => _closed = value; }
        public int? Efvs { get => _efvs; set => _efvs = value; }
        public bool? FdpExtension { get => _fdpExtension; set => _fdpExtension = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public DateTime? From { get => _from; set => _from = value; }
        public bool? Hold { get => _hold; set => _hold = value; }
        public int? InstTime { get => _instTime; set => _instTime = value; }
        public int? Landings { get => _landings; set => _landings = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public int? LoggedLandings { get => _loggedLandings; set => _loggedLandings = value; }
        public int? LoggedTakeOffs { get => _loggedTakeOffs; set => _loggedTakeOffs = value; }
        public string Nickname { get => nickname; set => nickname = value; }
        public int? NightLandings { get => _nightLandings; set => _nightLandings = value; }
        public int? NightTakeOffs { get => _nightTakeOffs; set => _nightTakeOffs = value; }
        public int? NightTime { get => _nightTime; set => _nightTime = value; }
        public int? NonPrecision { get => _nonPrecision; set => _nonPrecision = value; }
        public int? PicTime { get => _picTime; set => _picTime = value; }
        public string PilotRole { get => pilotRole; set => pilotRole = value; }
        public bool? PostFlightRestIncrease { get => _postFlightRestIncrease; set => _postFlightRestIncrease = value; }
        public int? Precision { get => _precision; set => _precision = value; }
        public DateTime? ReportTime { get => _reportTime; set => _reportTime = value; }
        public bool? Reported { get => _reported; set => _reported = value; }
        public int? SicTime { get => _sicTime; set => _sicTime = value; }
        public bool? SplitDutyClose { get => _splitDutyClose; set => _splitDutyClose = value; }
        public DateTime? SplitDutyCloseTime { get => _splitDutyCloseTime; set => _splitDutyCloseTime = value; }
        public bool? SplitDutyStart { get => _splitDutyStart; set => _splitDutyStart = value; }
        public DateTime? SplitDutyStartTime { get => _splitDutyStartTime; set => _splitDutyStartTime = value; }
        public int? TakeOffs { get => _takeOffs; set => _takeOffs = value; }
        public int? To { get => _to; set => _to = value; }
        public bool? UseReducedRest { get => _useReducedRest; set => _useReducedRest = value; }
        public bool? Visual { get => _visual; set => _visual = value; }
    }

    [Serializable]
    public class Fo
    {
        private int? _Idfo;
        private int? _Idtime;
        private string accountName;
        private string firstName;
        private decimal? _height;
        private string jobTitle;
        private string lastName;
        private string middleName;
        private string nickname;
        private int? _Idoperator;
        private string personnelNumber;
        private bool? _pilot;
        private string status;
        private decimal? _weight;
        private Operator _operator;
        private UserCharacteristics _userCharacteristics;

        public int? Idfo { get => _Idfo; set => _Idfo = value; }
        public int? Idtime { get => _Idtime; set => _Idtime = value; }
        public string AccountName { get => accountName; set => accountName = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public decimal? Height { get => _height; set => _height = value; }
        public string JobTitle { get => jobTitle; set => jobTitle = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string MiddleName { get => middleName; set => middleName = value; }
        public string Nickname { get => nickname; set => nickname = value; }
        public int? Idoperator { get => _Idoperator; set => _Idoperator = value; }
        public string PersonnelNumber { get => personnelNumber; set => personnelNumber = value; }
        public bool? Pilot { get => _pilot; set => _pilot = value; }
        public string Status { get => status; set => status = value; }
        public decimal? Weight { get => _weight; set => _weight = value; }
        public Operator Operator { get => _operator; set => _operator = value; }
        public UserCharacteristics UserCharacteristics { get => _userCharacteristics; set => _userCharacteristics = value; }
    }

    [Serializable]
    public class FlightTime
    {
        private int? _Idflight;
        private int? _Idtime;
        private decimal? _acMeter;
        private int? _aircraftExtraEnginesAcmCycles;
        private decimal? _aircraftExtraEnginesAcmHours;
        private int? _aircraftExtraEnginesApuCycles;
        private decimal? _aircraftExtraEnginesApuHours;
        private decimal? _aircraftExtraSysCcyCyclesLh;
        private decimal? _aircraftExtraSysCcyCyclesRh;
        private decimal? _aircraftExtraSysIcyCyclesLh;
        private decimal? _aircraftExtraSysIcyCyclesRh;
        private decimal? _aircraftExtraSysPcyCyclesLh;
        private decimal? _aircraftExtraSysPcyCyclesRh;
        private decimal? _aircraftTotalExtraEnginesAcmCycles;
        private decimal? _aircraftTotalExtraEnginesAcmHours;
        private decimal? _aircraftTotalExtraEnginesApuCycles;
        private decimal? _aircraftTotalExtraEnginesApuHours;
        private decimal? _aircraftTotalExtraSysCcyCyclesLh;
        private decimal? _aircraftTotalExtraSysCcyCyclesRh;
        private decimal? _aircraftTotalExtraSysIcyCyclesLh;
        private decimal? _aircraftTotalExtraSysIcyCyclesRh;
        private decimal? _aircraftTotalExtraSysPcyCyclesLh;
        private decimal? _aircraftTotalExtraSysPcyCyclesRh;
        private decimal? _apuOilUplift;
        private decimal? _cOilUplift;
        private int? _endHobbs;
        private int? _engine1Cycles;
        private int? _engine2Cycles;
        private int? _engine3Cycles;
        private int? _engine4Cycles;
        private int? _engineCycles;
        private int? _engineNr;
        private bool? _extraEnginesAcm;
        private int? _extraEnginesAcmCycles;
        private decimal? _extraEnginesAcmHours;
        private bool? _extraEnginesApu;
        private int? _extraEnginesApuCycles;
        private decimal? _extraEnginesApuHours;
        private decimal? _extraSysCcyCyclesLh;
        private decimal? _extraSysCcyCyclesRh;
        private decimal? _extraSysIcyCyclesLh;
        private decimal? _extraSysIcyCyclesRh;
        private decimal? _extraSysPcyCyclesLh;
        private decimal? _extraSysPcyCyclesRh;
        private bool? _extraSystemCcy;
        private bool? _extraSystemHobbs;
        private bool? _extraSystemIcy;
        private bool? _extraSystemPcy;
        private decimal? _lOilUplift;
        private int? _nightFlightTime;
        private bool? _previousFlightEndHobbsPresent;
        private decimal? _r2OilUplift;
        private decimal? _rOilUplift;
        private decimal? _startHobbs;
        private string techlogNumber;
        private int? _totalEngineCycles1;
        private int? _totalEngineCycles2;
        private int? _totalEngineCycles3;
        private int? _totalEngineCycles4;
        private long? _totalFlightHours;
        private long? _totalLandings;

        public int? Idflight { get => _Idflight; set => _Idflight = value; }
        public int? Idtime { get => _Idtime; set => _Idtime = value; }
        public decimal? AcMeter { get => _acMeter; set => _acMeter = value; }
        public int? AircraftExtraEnginesAcmCycles { get => _aircraftExtraEnginesAcmCycles; set => _aircraftExtraEnginesAcmCycles = value; }
        public decimal? AircraftExtraEnginesAcmHours { get => _aircraftExtraEnginesAcmHours; set => _aircraftExtraEnginesAcmHours = value; }
        public int? AircraftExtraEnginesApuCycles { get => _aircraftExtraEnginesApuCycles; set => _aircraftExtraEnginesApuCycles = value; }
        public decimal? AircraftExtraEnginesApuHours { get => _aircraftExtraEnginesApuHours; set => _aircraftExtraEnginesApuHours = value; }
        public decimal? AircraftExtraSysCcyCyclesLh { get => _aircraftExtraSysCcyCyclesLh; set => _aircraftExtraSysCcyCyclesLh = value; }
        public decimal? AircraftExtraSysCcyCyclesRh { get => _aircraftExtraSysCcyCyclesRh; set => _aircraftExtraSysCcyCyclesRh = value; }
        public decimal? AircraftExtraSysIcyCyclesLh { get => _aircraftExtraSysIcyCyclesLh; set => _aircraftExtraSysIcyCyclesLh = value; }
        public decimal? AircraftExtraSysIcyCyclesRh { get => _aircraftExtraSysIcyCyclesRh; set => _aircraftExtraSysIcyCyclesRh = value; }
        public decimal? AircraftExtraSysPcyCyclesLh { get => _aircraftExtraSysPcyCyclesLh; set => _aircraftExtraSysPcyCyclesLh = value; }
        public decimal? AircraftExtraSysPcyCyclesRh { get => _aircraftExtraSysPcyCyclesRh; set => _aircraftExtraSysPcyCyclesRh = value; }
        public decimal? AircraftTotalExtraEnginesAcmCycles { get => _aircraftTotalExtraEnginesAcmCycles; set => _aircraftTotalExtraEnginesAcmCycles = value; }
        public decimal? AircraftTotalExtraEnginesAcmHours { get => _aircraftTotalExtraEnginesAcmHours; set => _aircraftTotalExtraEnginesAcmHours = value; }
        public decimal? AircraftTotalExtraEnginesApuCycles { get => _aircraftTotalExtraEnginesApuCycles; set => _aircraftTotalExtraEnginesApuCycles = value; }
        public decimal? AircraftTotalExtraEnginesApuHours { get => _aircraftTotalExtraEnginesApuHours; set => _aircraftTotalExtraEnginesApuHours = value; }
        public decimal? AircraftTotalExtraSysCcyCyclesLh { get => _aircraftTotalExtraSysCcyCyclesLh; set => _aircraftTotalExtraSysCcyCyclesLh = value; }
        public decimal? AircraftTotalExtraSysCcyCyclesRh { get => _aircraftTotalExtraSysCcyCyclesRh; set => _aircraftTotalExtraSysCcyCyclesRh = value; }
        public decimal? AircraftTotalExtraSysIcyCyclesLh { get => _aircraftTotalExtraSysIcyCyclesLh; set => _aircraftTotalExtraSysIcyCyclesLh = value; }
        public decimal? AircraftTotalExtraSysIcyCyclesRh { get => _aircraftTotalExtraSysIcyCyclesRh; set => _aircraftTotalExtraSysIcyCyclesRh = value; }
        public decimal? AircraftTotalExtraSysPcyCyclesLh { get => _aircraftTotalExtraSysPcyCyclesLh; set => _aircraftTotalExtraSysPcyCyclesLh = value; }
        public decimal? AircraftTotalExtraSysPcyCyclesRh { get => _aircraftTotalExtraSysPcyCyclesRh; set => _aircraftTotalExtraSysPcyCyclesRh = value; }
        public decimal? ApuOilUplift { get => _apuOilUplift; set => _apuOilUplift = value; }
        public decimal? COilUplift { get => _cOilUplift; set => _cOilUplift = value; }
        public int? EndHobbs { get => _endHobbs; set => _endHobbs = value; }
        public int? Engine1Cycles { get => _engine1Cycles; set => _engine1Cycles = value; }
        public int? Engine2Cycles { get => _engine2Cycles; set => _engine2Cycles = value; }
        public int? Engine3Cycles { get => _engine3Cycles; set => _engine3Cycles = value; }
        public int? Engine4Cycles { get => _engine4Cycles; set => _engine4Cycles = value; }
        public int? EngineCycles { get => _engineCycles; set => _engineCycles = value; }
        public int? EngineNr { get => _engineNr; set => _engineNr = value; }
        public bool? ExtraEnginesAcm { get => _extraEnginesAcm; set => _extraEnginesAcm = value; }
        public int? ExtraEnginesAcmCycles { get => _extraEnginesAcmCycles; set => _extraEnginesAcmCycles = value; }
        public decimal? ExtraEnginesAcmHours { get => _extraEnginesAcmHours; set => _extraEnginesAcmHours = value; }
        public bool? ExtraEnginesApu { get => _extraEnginesApu; set => _extraEnginesApu = value; }
        public int? ExtraEnginesApuCycles { get => _extraEnginesApuCycles; set => _extraEnginesApuCycles = value; }
        public decimal? ExtraEnginesApuHours { get => _extraEnginesApuHours; set => _extraEnginesApuHours = value; }
        public decimal? ExtraSysCcyCyclesLh { get => _extraSysCcyCyclesLh; set => _extraSysCcyCyclesLh = value; }
        public decimal? ExtraSysCcyCyclesRh { get => _extraSysCcyCyclesRh; set => _extraSysCcyCyclesRh = value; }
        public decimal? ExtraSysIcyCyclesLh { get => _extraSysIcyCyclesLh; set => _extraSysIcyCyclesLh = value; }
        public decimal? ExtraSysIcyCyclesRh { get => _extraSysIcyCyclesRh; set => _extraSysIcyCyclesRh = value; }
        public decimal? ExtraSysPcyCyclesLh { get => _extraSysPcyCyclesLh; set => _extraSysPcyCyclesLh = value; }
        public decimal? ExtraSysPcyCyclesRh { get => _extraSysPcyCyclesRh; set => _extraSysPcyCyclesRh = value; }
        public bool? ExtraSystemCcy { get => _extraSystemCcy; set => _extraSystemCcy = value; }
        public bool? ExtraSystemHobbs { get => _extraSystemHobbs; set => _extraSystemHobbs = value; }
        public bool? ExtraSystemIcy { get => _extraSystemIcy; set => _extraSystemIcy = value; }
        public bool? ExtraSystemPcy { get => _extraSystemPcy; set => _extraSystemPcy = value; }
        public decimal? LOilUplift { get => _lOilUplift; set => _lOilUplift = value; }
        public int? NightFlightTime { get => _nightFlightTime; set => _nightFlightTime = value; }
        public bool? PreviousFlightEndHobbsPresent { get => _previousFlightEndHobbsPresent; set => _previousFlightEndHobbsPresent = value; }
        public decimal? R2OilUplift { get => _r2OilUplift; set => _r2OilUplift = value; }
        public decimal? ROilUplift { get => _rOilUplift; set => _rOilUplift = value; }
        public decimal? StartHobbs { get => _startHobbs; set => _startHobbs = value; }
        public string TechlogNumber { get => techlogNumber; set => techlogNumber = value; }
        public int? TotalEngineCycles1 { get => _totalEngineCycles1; set => _totalEngineCycles1 = value; }
        public int? TotalEngineCycles2 { get => _totalEngineCycles2; set => _totalEngineCycles2 = value; }
        public int? TotalEngineCycles3 { get => _totalEngineCycles3; set => _totalEngineCycles3 = value; }
        public int? TotalEngineCycles4 { get => _totalEngineCycles4; set => _totalEngineCycles4 = value; }
        public long? TotalFlightHours { get => _totalFlightHours; set => _totalFlightHours = value; }
        public long? TotalLandings { get => _totalLandings; set => _totalLandings = value; }
    }

    [Serializable]
    public class Fa
    {
        private int? _Idfa;
        private int? _Idtime;
        private long? _closeTime;
        private bool? _closed;
        private int? _efvs;
        private bool? _fdpExtension;
        private string firstName;
        private long? _from;
        private bool? _hold;
        private int? _instTime;
        private int? _landings;
        private string lastName;
        private int? _loggedLandings;
        private int? _loggedTakeOffs;
        private string nickname;
        private int? _nightLandings;
        private int? _nightTakeOffs;
        private int? _nightTime;
        private int? _nonPrecision;
        private int? _picTime;
        private string pilotRole;
        private bool? _postFlightRestIncrease;
        private int? _precision;
        private long? _reportTime;
        private bool? _reported;
        private bool? _splitDutyClose;
        private long? _splitDutyCloseTime;
        private bool? _splitDutyStart;
        private long? _splitDutyStartTime;
        private int? _takeOffs;
        private long? _to;
        private bool? _useReducedRest;
        private bool? _visual;

        public int? Idfa { get => _Idfa; set => _Idfa = value; }
        public int? Idtime { get => _Idtime; set => _Idtime = value; }
        public long? CloseTime { get => _closeTime; set => _closeTime = value; }
        public bool? Closed { get => _closed; set => _closed = value; }
        public int? Efvs { get => _efvs; set => _efvs = value; }
        public bool? FdpExtension { get => _fdpExtension; set => _fdpExtension = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public long? From { get => _from; set => _from = value; }
        public bool? Hold { get => _hold; set => _hold = value; }
        public int? InstTime { get => _instTime; set => _instTime = value; }
        public int? Landings { get => _landings; set => _landings = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public int? LoggedLandings { get => _loggedLandings; set => _loggedLandings = value; }
        public int? LoggedTakeOffs { get => _loggedTakeOffs; set => _loggedTakeOffs = value; }
        public string Nickname { get => nickname; set => nickname = value; }
        public int? NightLandings { get => _nightLandings; set => _nightLandings = value; }
        public int? NightTakeOffs { get => _nightTakeOffs; set => _nightTakeOffs = value; }
        public int? NightTime { get => _nightTime; set => _nightTime = value; }
        public int? NonPrecision { get => _nonPrecision; set => _nonPrecision = value; }
        public int? PicTime { get => _picTime; set => _picTime = value; }
        public string PilotRole { get => pilotRole; set => pilotRole = value; }
        public bool? PostFlightRestIncrease { get => _postFlightRestIncrease; set => _postFlightRestIncrease = value; }
        public int? Precision { get => _precision; set => _precision = value; }
        public long? ReportTime { get => _reportTime; set => _reportTime = value; }
        public bool? Reported { get => _reported; set => _reported = value; }
        public bool? SplitDutyClose { get => _splitDutyClose; set => _splitDutyClose = value; }
        public long? SplitDutyCloseTime { get => _splitDutyCloseTime; set => _splitDutyCloseTime = value; }
        public bool? SplitDutyStart { get => _splitDutyStart; set => _splitDutyStart = value; }
        public long? SplitDutyStartTime { get => _splitDutyStartTime; set => _splitDutyStartTime = value; }
        public int? TakeOffs { get => _takeOffs; set => _takeOffs = value; }
        public long? To { get => _to; set => _to = value; }
        public bool? UseReducedRest { get => _useReducedRest; set => _useReducedRest = value; }
        public bool? Visual { get => _visual; set => _visual = value; }
    }

    [Serializable]
    public class Dtls2
    {
        private int? _Iddtls2;
        private int? _Idtime;
        private int? _aoc;
        private bool? _canUseFdp1hExtension;
        private long? _checkin;
        private long? _checkinActual;
        private long? _checkinDefault;
        private long? _checkout;
        private long? _checkoutActual;
        private long? _checkoutDefault;
        private bool? _closed;
        private bool? _dgacFdtl;
        private bool? _faaFtl;
        private bool? _fdpEqualsDp;
        private bool? _fdpExtension;
        private bool? _fdpExtensionIsNotPossible;
        private bool? _lastFlightActualsEntered;
        private int? _lastFlightId;
        private long? _nextWeeklyRestDueFor;
        private string pilotRole;
        private bool? _postFlightRestIncrease;
        private long? _reducedCheckin;
        private long? _reducedCheckout;
        private bool? _reducedRestEnabled;
        private int? _refDutyId;
        private bool? _reported;
        private bool? _restReductionIsNotPossible;
        private bool? _splitDutyClose;
        private bool? _MyProperty;
        private long? _splitDutyStart;
        private bool? _startOfWeeklyFirstDuty;
        private bool? _suitableAccommodation;
        private bool? _suitableAccommodationEnabled;
        private bool? _useReducedRest;
        private int? _userId;
        private int? _weeklyHoursLeft;

        public int? Iddtls2 { get => _Iddtls2; set => _Iddtls2 = value; }
        public int? Idtime { get => _Idtime; set => _Idtime = value; }
        public int? Aoc { get => _aoc; set => _aoc = value; }
        public bool? CanUseFdp1hExtension { get => _canUseFdp1hExtension; set => _canUseFdp1hExtension = value; }
        public long? Checkin { get => _checkin; set => _checkin = value; }
        public long? CheckinActual { get => _checkinActual; set => _checkinActual = value; }
        public long? CheckinDefault { get => _checkinDefault; set => _checkinDefault = value; }
        public long? Checkout { get => _checkout; set => _checkout = value; }
        public long? CheckoutActual { get => _checkoutActual; set => _checkoutActual = value; }
        public long? CheckoutDefault { get => _checkoutDefault; set => _checkoutDefault = value; }
        public bool? Closed { get => _closed; set => _closed = value; }
        public bool? DgacFdtl { get => _dgacFdtl; set => _dgacFdtl = value; }
        public bool? FaaFtl { get => _faaFtl; set => _faaFtl = value; }
        public bool? FdpEqualsDp { get => _fdpEqualsDp; set => _fdpEqualsDp = value; }
        public bool? FdpExtension { get => _fdpExtension; set => _fdpExtension = value; }
        public bool? FdpExtensionIsNotPossible { get => _fdpExtensionIsNotPossible; set => _fdpExtensionIsNotPossible = value; }
        public bool? LastFlightActualsEntered { get => _lastFlightActualsEntered; set => _lastFlightActualsEntered = value; }
        public int? LastFlightId { get => _lastFlightId; set => _lastFlightId = value; }
        public long? NextWeeklyRestDueFor { get => _nextWeeklyRestDueFor; set => _nextWeeklyRestDueFor = value; }
        public string PilotRole { get => pilotRole; set => pilotRole = value; }
        public bool? PostFlightRestIncrease { get => _postFlightRestIncrease; set => _postFlightRestIncrease = value; }
        public long? ReducedCheckin { get => _reducedCheckin; set => _reducedCheckin = value; }
        public long? ReducedCheckout { get => _reducedCheckout; set => _reducedCheckout = value; }
        public bool? ReducedRestEnabled { get => _reducedRestEnabled; set => _reducedRestEnabled = value; }
        public int? RefDutyId { get => _refDutyId; set => _refDutyId = value; }
        public bool? Reported { get => _reported; set => _reported = value; }
        public bool? RestReductionIsNotPossible { get => _restReductionIsNotPossible; set => _restReductionIsNotPossible = value; }
        public bool? SplitDutyClose { get => _splitDutyClose; set => _splitDutyClose = value; }
        public bool? MyProperty { get => _MyProperty; set => _MyProperty = value; }
        public long? SplitDutyStart { get => _splitDutyStart; set => _splitDutyStart = value; }
        public bool? StartOfWeeklyFirstDuty { get => _startOfWeeklyFirstDuty; set => _startOfWeeklyFirstDuty = value; }
        public bool? SuitableAccommodation { get => _suitableAccommodation; set => _suitableAccommodation = value; }
        public bool? SuitableAccommodationEnabled { get => _suitableAccommodationEnabled; set => _suitableAccommodationEnabled = value; }
        public bool? UseReducedRest { get => _useReducedRest; set => _useReducedRest = value; }
        public int? UserId { get => _userId; set => _userId = value; }
        public int? WeeklyHoursLeft { get => _weeklyHoursLeft; set => _weeklyHoursLeft = value; }
    }

    [Serializable]
    public class Dep
    {
        private int? _Iddep;
        private int? _Idtime;
        private long? _blocksOff;
        private bool? _cmdCheckin;
        private int? _cmdLoggedTakeOffs;
        private long? _cmdStartTime;
        private int? _cmdTakeOffs;
        private int? _cmdTakeOffsNight;
        private string delayOffBlockReason;
        private string delayTakeOffReason;
        private bool? _foCheckin;
        private int? _foLoggedTakeOffs;
        private long? _foStartTime;
        private int? _foTakeOffs;
        private int? _foTakeOffsNight;
        private int? _pax;
        private long? _takeOff;

        public int? Iddep { get => _Iddep; set => _Iddep = value; }
        public int? Idtime { get => _Idtime; set => _Idtime = value; }
        public long? BlocksOff { get => _blocksOff; set => _blocksOff = value; }
        public bool? CmdCheckin { get => _cmdCheckin; set => _cmdCheckin = value; }
        public int? CmdLoggedTakeOffs { get => _cmdLoggedTakeOffs; set => _cmdLoggedTakeOffs = value; }
        public long? CmdStartTime { get => _cmdStartTime; set => _cmdStartTime = value; }
        public int? CmdTakeOffs { get => _cmdTakeOffs; set => _cmdTakeOffs = value; }
        public int? CmdTakeOffsNight { get => _cmdTakeOffsNight; set => _cmdTakeOffsNight = value; }
        public string DelayOffBlockReason { get => delayOffBlockReason; set => delayOffBlockReason = value; }
        public string DelayTakeOffReason { get => delayTakeOffReason; set => delayTakeOffReason = value; }
        public bool? FoCheckin { get => _foCheckin; set => _foCheckin = value; }
        public int? FoLoggedTakeOffs { get => _foLoggedTakeOffs; set => _foLoggedTakeOffs = value; }
        public long? FoStartTime { get => _foStartTime; set => _foStartTime = value; }
        public int? FoTakeOffs { get => _foTakeOffs; set => _foTakeOffs = value; }
        public int? FoTakeOffsNight { get => _foTakeOffsNight; set => _foTakeOffsNight = value; }
        public int? Pax { get => _pax; set => _pax = value; }
        public long? TakeOff { get => _takeOff; set => _takeOff = value; }
    }

    [Serializable]
    public class Cmd
    {
        private int? _id;
        private int? _Idtime;
        private string accountName;
        private string firstName;
        private decimal? _height;
        private string jobTitle;
        private string lastName;
        private string middleName;
        private string nickname;
        private int? _Idoperator;
        private string personnelNumber;
        private bool? _pilot;
        private string status;
        private decimal? _weight;
        private Operator _operator;
        private UserCharacteristics _userCharacteristics;

        public int? Id { get => _id; set => _id = value; }
        public int? Idtime { get => _Idtime; set => _Idtime = value; }
        public string AccountName { get => accountName; set => accountName = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public decimal? Height { get => _height; set => _height = value; }
        public string JobTitle { get => jobTitle; set => jobTitle = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string MiddleName { get => middleName; set => middleName = value; }
        public string Nickname { get => nickname; set => nickname = value; }
        public int? Idoperator { get => _Idoperator; set => _Idoperator = value; }
        public string PersonnelNumber { get => personnelNumber; set => personnelNumber = value; }
        public bool? Pilot { get => _pilot; set => _pilot = value; }
        public string Status { get => status; set => status = value; }
        public decimal? Weight { get => _weight; set => _weight = value; }
        public Operator Operator { get => _operator; set => _operator = value; }
        public UserCharacteristics UserCharacteristics { get => _userCharacteristics; set => _userCharacteristics = value; }
    }

    [Serializable]
    public class Arr
    {
        private int? _Idarr;
        private int? _Idtime;
        private string approachType;
        private bool? _cmdCheckout;
        private long? _blocksOn;
        private long? _cmdClose;
        private int? _cmdDayLandings;
        private int? _cmdDutyTime;
        private int? _cmdEfvs;
        private bool? _cmdFdpExtension;
        private bool? _cmdHold;
        private int? _cmdInstTime;
        private int? _cmdLoggedLandings;
        private int? _cmdNightLandings;
        private int? _cmdNightTime;
        private int? _cmdNonPrecision;
        private int? _cmdPicTime;
        private bool? _cmdPostFlightRestIncrease;
        private int? _cmdPrecision;
        private int? _cmdSicTime;
        private bool? _cmdSplitDutyClose;
        private long? _cmdSplitDutyCloseTime;
        private bool? _cmdSplitDutyStart;
        private long? _cmdSplitDutyStartTime;
        private bool? _cmdUseReducedRest;
        private bool? _cmdVisual;
        private string delayLandingReason;
        private string delayOnBlockReason;
        private bool? _foCheckout;
        private long? _foClose;
        private int? _foDayLandings;
        private int? _foDutyTime;
        private int? _foEfvs;
        private bool? _foFdpExtension;
        private bool? _foHold;
        private int? _foInstTime;
        private int? _foLoggedLandings;
        private int? _foNightLandings;
        private int? _foNightTime;
        private int? _foNonPrecision;
        private int? _foPicTime;
        private bool? _foPostFlightRestIncrease;
        private int? _foPrecision;
        private int? _foSicTime;
        private bool? _foSplitDutyClose;
        private long? _foSplitDutyCloseTime;
        private bool? _foSplitDutyStart;
        private long? _foSplitDutyStartTime;
        private bool? _foUseReducedRest;
        private bool? _foVisual;
        private long? _landing;

        public int? Idarr { get => _Idarr; set => _Idarr = value; }
        public int? Idtime { get => _Idtime; set => _Idtime = value; }
        public string ApproachType { get => approachType; set => approachType = value; }
        public bool? CmdCheckout { get => _cmdCheckout; set => _cmdCheckout = value; }
        public long? BlocksOn { get => _blocksOn; set => _blocksOn = value; }
        public long? CmdClose { get => _cmdClose; set => _cmdClose = value; }
        public int? CmdDayLandings { get => _cmdDayLandings; set => _cmdDayLandings = value; }
        public int? CmdDutyTime { get => _cmdDutyTime; set => _cmdDutyTime = value; }
        public int? CmdEfvs { get => _cmdEfvs; set => _cmdEfvs = value; }
        public bool? CmdFdpExtension { get => _cmdFdpExtension; set => _cmdFdpExtension = value; }
        public bool? CmdHold { get => _cmdHold; set => _cmdHold = value; }
        public int? CmdInstTime { get => _cmdInstTime; set => _cmdInstTime = value; }
        public int? CmdLoggedLandings { get => _cmdLoggedLandings; set => _cmdLoggedLandings = value; }
        public int? CmdNightLandings { get => _cmdNightLandings; set => _cmdNightLandings = value; }
        public int? CmdNightTime { get => _cmdNightTime; set => _cmdNightTime = value; }
        public int? CmdNonPrecision { get => _cmdNonPrecision; set => _cmdNonPrecision = value; }
        public int? CmdPicTime { get => _cmdPicTime; set => _cmdPicTime = value; }
        public bool? CmdPostFlightRestIncrease { get => _cmdPostFlightRestIncrease; set => _cmdPostFlightRestIncrease = value; }
        public int? CmdPrecision { get => _cmdPrecision; set => _cmdPrecision = value; }
        public int? CmdSicTime { get => _cmdSicTime; set => _cmdSicTime = value; }
        public bool? CmdSplitDutyClose { get => _cmdSplitDutyClose; set => _cmdSplitDutyClose = value; }
        public long? CmdSplitDutyCloseTime { get => _cmdSplitDutyCloseTime; set => _cmdSplitDutyCloseTime = value; }
        public bool? CmdSplitDutyStart { get => _cmdSplitDutyStart; set => _cmdSplitDutyStart = value; }
        public long? CmdSplitDutyStartTime { get => _cmdSplitDutyStartTime; set => _cmdSplitDutyStartTime = value; }
        public bool? CmdUseReducedRest { get => _cmdUseReducedRest; set => _cmdUseReducedRest = value; }
        public bool? CmdVisual { get => _cmdVisual; set => _cmdVisual = value; }
        public string DelayLandingReason { get => delayLandingReason; set => delayLandingReason = value; }
        public string DelayOnBlockReason { get => delayOnBlockReason; set => delayOnBlockReason = value; }
        public bool? FoCheckout { get => _foCheckout; set => _foCheckout = value; }
        public long? FoClose { get => _foClose; set => _foClose = value; }
        public int? FoDayLandings { get => _foDayLandings; set => _foDayLandings = value; }
        public int? FoDutyTime { get => _foDutyTime; set => _foDutyTime = value; }
        public int? FoEfvs { get => _foEfvs; set => _foEfvs = value; }
        public bool? FoFdpExtension { get => _foFdpExtension; set => _foFdpExtension = value; }
        public bool? FoHold { get => _foHold; set => _foHold = value; }
        public int? FoInstTime { get => _foInstTime; set => _foInstTime = value; }
        public int? FoLoggedLandings { get => _foLoggedLandings; set => _foLoggedLandings = value; }
        public int? FoNightLandings { get => _foNightLandings; set => _foNightLandings = value; }
        public int? FoNightTime { get => _foNightTime; set => _foNightTime = value; }
        public int? FoNonPrecision { get => _foNonPrecision; set => _foNonPrecision = value; }
        public int? FoPicTime { get => _foPicTime; set => _foPicTime = value; }
        public bool? FoPostFlightRestIncrease { get => _foPostFlightRestIncrease; set => _foPostFlightRestIncrease = value; }
        public int? FoPrecision { get => _foPrecision; set => _foPrecision = value; }
        public int? FoSicTime { get => _foSicTime; set => _foSicTime = value; }
        public bool? FoSplitDutyClose { get => _foSplitDutyClose; set => _foSplitDutyClose = value; }
        public long? FoSplitDutyCloseTime { get => _foSplitDutyCloseTime; set => _foSplitDutyCloseTime = value; }
        public bool? FoSplitDutyStart { get => _foSplitDutyStart; set => _foSplitDutyStart = value; }
        public long? FoSplitDutyStartTime { get => _foSplitDutyStartTime; set => _foSplitDutyStartTime = value; }
        public bool? FoUseReducedRest { get => _foUseReducedRest; set => _foUseReducedRest = value; }
        public bool? FoVisual { get => _foVisual; set => _foVisual = value; }
        public long? Landing { get => _landing; set => _landing = value; }
    }

    [Serializable]
    public class Add_crew
    {
        private int? _Idadd_crew;
        private int? _Idtime;
        private long? _closeTime;
        private bool? _closed;
        private int? _efvs;
        private bool? _fdpExtension;
        private string firstName;
        private long? _from;
        private bool? _hold;
        private int? _instTime;
        private int? _landings;
        private string lastName;
        private int? _loggedLandings;
        private int? _loggedTakeOffs;
        private string nickname;
        private int? _nightLandings;
        private int? _nightTakeOffs;
        private int? _nightTime;
        private int? _nonPrecision;
        private int? _picTime;
        private string pilotRole;
        private bool? _postFlightRestIncrease;
        private string precision;
        private long? _reportTime;
        private bool? _reported;
        private int? _sicTime;
        private bool? _splitDutyClose;
        private long? _splitDutyCloseTime;
        private bool? _splitDutyStart;
        private long? _splitDutyStartTime;
        private int? _takeOffs;
        private long? _to;
        private bool? _useReducedRest;
        private bool? _visual;

        public int? Idadd_crew { get => _Idadd_crew; set => _Idadd_crew = value; }
        public int? Idtime { get => _Idtime; set => _Idtime = value; }
        public long? CloseTime { get => _closeTime; set => _closeTime = value; }
        public bool? Closed { get => _closed; set => _closed = value; }
        public int? Efvs { get => _efvs; set => _efvs = value; }
        public bool? FdpExtension { get => _fdpExtension; set => _fdpExtension = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public long? From { get => _from; set => _from = value; }
        public bool? Hold { get => _hold; set => _hold = value; }
        public int? InstTime { get => _instTime; set => _instTime = value; }
        public int? Landings { get => _landings; set => _landings = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public int? LoggedLandings { get => _loggedLandings; set => _loggedLandings = value; }
        public int? LoggedTakeOffs { get => _loggedTakeOffs; set => _loggedTakeOffs = value; }
        public string Nickname { get => nickname; set => nickname = value; }
        public int? NightLandings { get => _nightLandings; set => _nightLandings = value; }
        public int? NightTakeOffs { get => _nightTakeOffs; set => _nightTakeOffs = value; }
        public int? NightTime { get => _nightTime; set => _nightTime = value; }
        public int? NonPrecision { get => _nonPrecision; set => _nonPrecision = value; }
        public int? PicTime { get => _picTime; set => _picTime = value; }
        public string PilotRole { get => pilotRole; set => pilotRole = value; }
        public bool? PostFlightRestIncrease { get => _postFlightRestIncrease; set => _postFlightRestIncrease = value; }
        public string Precision { get => precision; set => precision = value; }
        public long? ReportTime { get => _reportTime; set => _reportTime = value; }
        public bool? Reported { get => _reported; set => _reported = value; }
        public int? SicTime { get => _sicTime; set => _sicTime = value; }
        public bool? SplitDutyClose { get => _splitDutyClose; set => _splitDutyClose = value; }
        public long? SplitDutyCloseTime { get => _splitDutyCloseTime; set => _splitDutyCloseTime = value; }
        public bool? SplitDutyStart { get => _splitDutyStart; set => _splitDutyStart = value; }
        public long? SplitDutyStartTime { get => _splitDutyStartTime; set => _splitDutyStartTime = value; }
        public int? TakeOffs { get => _takeOffs; set => _takeOffs = value; }
        public long? To { get => _to; set => _to = value; }
        public bool? UseReducedRest { get => _useReducedRest; set => _useReducedRest = value; }
        public bool? Visual { get => _visual; set => _visual = value; }
    }

    [Serializable]
    public class PreFlight
    {
        private int? _id;
        private int? _Idpostflight;
        private string by;
        private string fplType;
        private string fuelTankUnit;
        private string status;
        private Flp _flp;
        private Oflp _oflp;
        private Receipt _receipt;
        private TafMetar _tafMetar;
        private WeightAndBalance _weightAndBalance;

        public int? Id { get => _id; set => _id = value; }
        public int? Idpostflight { get => _Idpostflight; set => _Idpostflight = value; }
        public string By { get => by; set => by = value; }
        public string FplType { get => fplType; set => fplType = value; }
        public string FuelTankUnit { get => fuelTankUnit; set => fuelTankUnit = value; }
        public string Status { get => status; set => status = value; }
        public Flp Flp { get => _flp; set => _flp = value; }
        public Oflp Oflp { get => _oflp; set => _oflp = value; }
        public Receipt Receipt { get => _receipt; set => _receipt = value; }
        public TafMetar TafMetar { get => _tafMetar; set => _tafMetar = value; }
        public WeightAndBalance WeightAndBalance { get => _weightAndBalance; set => _weightAndBalance = value; }
    }

    [Serializable]
    public class WeightAndBalance
    {
        private int? _IdweightAndalance;
        private int? _IdpreFlight;
        private string customName;
        private string entityName;
        private int? _fileSize;
        private int? _imageSizeX;
        private int? _imageSizeY;
        private bool? _isThumbnail;
        private string name;
        private string originalName;
        private string path;
        private int? _refEntityId;
        private string status;
        private string thumbnail;
        private string uuid;

        public int? IdweightAndalance { get => _IdweightAndalance; set => _IdweightAndalance = value; }
        public int? IdpreFlight { get => _IdpreFlight; set => _IdpreFlight = value; }
        public string CustomName { get => customName; set => customName = value; }
        public string EntityName { get => entityName; set => entityName = value; }
        public int? FileSize { get => _fileSize; set => _fileSize = value; }
        public int? ImageSizeX { get => _imageSizeX; set => _imageSizeX = value; }
        public int? ImageSizeY { get => _imageSizeY; set => _imageSizeY = value; }
        public bool? IsThumbnail { get => _isThumbnail; set => _isThumbnail = value; }
        public string Name { get => name; set => name = value; }
        public string OriginalName { get => originalName; set => originalName = value; }
        public string Path { get => path; set => path = value; }
        public int? RefEntityId { get => _refEntityId; set => _refEntityId = value; }
        public string Status { get => status; set => status = value; }
        public string Thumbnail { get => thumbnail; set => thumbnail = value; }
        public string Uuid { get => uuid; set => uuid = value; }
    }

    [Serializable]
    public class TafMetar
    {
        private int? _IdtafMetar;
        private int? _IdpreFlight;
        private string customName;
        private string entityName;
        private int? _fileSize;
        private int? _imageSizeX;
        private int? _imageSizeY;
        private bool? _isThumbnail;
        private string name;
        private string originalName;
        private string path;
        private int? _refEntityId;
        private string status;
        private string thumbnail;
        private string uuid;

        public int? IdtafMetar { get => _IdtafMetar; set => _IdtafMetar = value; }
        public int? IdpreFlight { get => _IdpreFlight; set => _IdpreFlight = value; }
        public string CustomName { get => customName; set => customName = value; }
        public string EntityName { get => entityName; set => entityName = value; }
        public int? FileSize { get => _fileSize; set => _fileSize = value; }
        public int? ImageSizeX { get => _imageSizeX; set => _imageSizeX = value; }
        public int? ImageSizeY { get => _imageSizeY; set => _imageSizeY = value; }
        public bool? IsThumbnail { get => _isThumbnail; set => _isThumbnail = value; }
        public string Name { get => name; set => name = value; }
        public string OriginalName { get => originalName; set => originalName = value; }
        public string Path { get => path; set => path = value; }
        public int? RefEntityId { get => _refEntityId; set => _refEntityId = value; }
        public string Status { get => status; set => status = value; }
        public string Thumbnail { get => thumbnail; set => thumbnail = value; }
        public string Uuid { get => uuid; set => uuid = value; }
    }

    [Serializable]
    public class Receipt
    {
        private int? _Idreceipt;
        private int? _IdpreFlight;
        private string customName;
        private string entityName;
        private int? _fileSize;
        private int? _imageSizeX;
        private int? _imageSizeY;
        private bool? _isThumbnail;
        private string name;
        private string originalName;
        private string path;
        private int? _refEntityId;
        private string status;
        private string thumbnail;
        private string uuid;

        public int? Idreceipt { get => _Idreceipt; set => _Idreceipt = value; }
        public int? IdpreFlight { get => _IdpreFlight; set => _IdpreFlight = value; }
        public string CustomName { get => customName; set => customName = value; }
        public string EntityName { get => entityName; set => entityName = value; }
        public int? FileSize { get => _fileSize; set => _fileSize = value; }
        public int? ImageSizeX { get => _imageSizeX; set => _imageSizeX = value; }
        public int? ImageSizeY { get => _imageSizeY; set => _imageSizeY = value; }
        public bool? IsThumbnail { get => _isThumbnail; set => _isThumbnail = value; }
        public string Name { get => name; set => name = value; }
        public string OriginalName { get => originalName; set => originalName = value; }
        public string Path { get => path; set => path = value; }
        public int? RefEntityId { get => _refEntityId; set => _refEntityId = value; }
        public string Status { get => status; set => status = value; }
        public string Thumbnail { get => thumbnail; set => thumbnail = value; }
        public string Uuid { get => uuid; set => uuid = value; }
    }

    [Serializable]
    public class Oflp
    {
        private int? _Idoflp;
        private int? _IdpreFlight;
        private string customName;
        private string entityName;
        private int? _fileSize;
        private int? _imageSizeX;
        private int? _imageSizeY;
        private bool? _isThumbnail;
        private string name;
        private string originalName;
        private string path;
        private int? _refEntityId;
        private string status;
        private string thumbnail;
        private string uuid;

        public int? Idoflp { get => _Idoflp; set => _Idoflp = value; }
        public int? IdpreFlight { get => _IdpreFlight; set => _IdpreFlight = value; }
        public string CustomName { get => customName; set => customName = value; }
        public string EntityName { get => entityName; set => entityName = value; }
        public int? FileSize { get => _fileSize; set => _fileSize = value; }
        public int? ImageSizeX { get => _imageSizeX; set => _imageSizeX = value; }
        public int? ImageSizeY { get => _imageSizeY; set => _imageSizeY = value; }
        public bool? IsThumbnail { get => _isThumbnail; set => _isThumbnail = value; }
        public string Name { get => name; set => name = value; }
        public string OriginalName { get => originalName; set => originalName = value; }
        public string Path { get => path; set => path = value; }
        public int? RefEntityId { get => _refEntityId; set => _refEntityId = value; }
        public string Status { get => status; set => status = value; }
        public string Thumbnail { get => thumbnail; set => thumbnail = value; }
        public string Uuid { get => uuid; set => uuid = value; }
    }

    [Serializable]
    public class Flp
    {
        private int? _Idflp;
        private int? _IdpreFlight;
        private string customName;
        private string entityName;
        private int? _fileSize;
        private int? _imageSizeX;
        private int? _imageSizeY;
        private bool? _isThumbnail;
        private string name;
        private string originalName;
        private string path;
        private int? _refEntityId;
        private string status;
        private string thumbnail;
        private string uuid;

        public int? Idflp { get => _Idflp; set => _Idflp = value; }
        public int? IdpreFlight { get => _IdpreFlight; set => _IdpreFlight = value; }
        public string CustomName { get => customName; set => customName = value; }
        public string EntityName { get => entityName; set => entityName = value; }
        public int? FileSize { get => _fileSize; set => _fileSize = value; }
        public int? ImageSizeX { get => _imageSizeX; set => _imageSizeX = value; }
        public int? ImageSizeY { get => _imageSizeY; set => _imageSizeY = value; }
        public bool? IsThumbnail { get => _isThumbnail; set => _isThumbnail = value; }
        public string Name { get => name; set => name = value; }
        public string OriginalName { get => originalName; set => originalName = value; }
        public string Path { get => path; set => path = value; }
        public int? RefEntityId { get => _refEntityId; set => _refEntityId = value; }
        public string Status { get => status; set => status = value; }
        public string Thumbnail { get => thumbnail; set => thumbnail = value; }
        public string Uuid { get => uuid; set => uuid = value; }
    }
    [Serializable]
    public class FuelProviders
    {
        private int? _IdfuelProviders;
        private int? _Idpostflight;
        private int? _MyProperty;
        private int? _eurPerLiter;
        private string eurPerLiterCommercial;
        private string name;
        private int? _priceIndex;
        private int? _priceIndexCommercial;
        private bool? _hold;
        private string journeyLog;
        private int? _nonPrecision;

        public int? IdfuelProviders { get => _IdfuelProviders; set => _IdfuelProviders = value; }
        public int? Idpostflight { get => _Idpostflight; set => _Idpostflight = value; }
        public int? MyProperty { get => _MyProperty; set => _MyProperty = value; }
        public int? EurPerLiter { get => _eurPerLiter; set => _eurPerLiter = value; }
        public string EurPerLiterCommercial { get => eurPerLiterCommercial; set => eurPerLiterCommercial = value; }
        public string Name { get => name; set => name = value; }
        public int? PriceIndex { get => _priceIndex; set => _priceIndex = value; }
        public int? PriceIndexCommercial { get => _priceIndexCommercial; set => _priceIndexCommercial = value; }
        public bool? Hold { get => _hold; set => _hold = value; }
        public string JourneyLog { get => journeyLog; set => journeyLog = value; }
        public int? NonPrecision { get => _nonPrecision; set => _nonPrecision = value; }
    }

    [Serializable]
    public class Fuel
    {
        private int? _id;
        private int? _Idpostflight;
        private int? _agentId;
        private int? _airportId;
        private string by;
        private bool? _cancellationRequired;
        private DateTime? _creditCardExpiration;
        private string creditCardNumber;
        private string currency;
        private long? _deliveryDate;
        private decimal? _density;
        private string externalNote;
        private string externalSource;
        private string externalStatus;
        private string fplType;
        private decimal? _fuelArrival;
        private decimal? _fuelBurned;
        private decimal? _fuelCost;
        private string fuelCurrency;
        private decimal? _fuelPriceIndex;
        private decimal? _fuelPriceIndexCommercial;
        private int? _fuelProvider;
        private decimal? _fuelRemainig;
        private decimal? _fuelRemainigActual;
        private DateTime? _fuelReqReleaseSent;
        private DateTime? _fuelReqSent;
        private string fuelTankUnit;
        private string fuelType;
        private string notes;
        private string payment;
        private string paymentType;
        private int? _permitProviderId;
        private DateTime? _poCreatedDate;
        private string poNumber;
        private decimal? _price;
        private decimal? _priceComercial;
        private decimal? _pricePerUnit;
        private decimal? _pricePerUnitComercial;
        private int? _providerId;
        private int? _quantity;
        private bool? _quoteOnly;
        private int? _releaseAgentId;
        private int? _releaseDocument;
        private string status;
        private string unitDensity;
        private DateTime? _updatedDate;
        private decimal? _uplift;
        private decimal? _upliftMass;
        private string upliftUnit;
        private FuelRelease fuelRelease;
        private Release release;

        public int? Id { get => _id; set => _id = value; }
        public int? Idpostflight { get => _Idpostflight; set => _Idpostflight = value; }
        public int? AgentId { get => _agentId; set => _agentId = value; }
        public int? AirportId { get => _airportId; set => _airportId = value; }
        public string By { get => by; set => by = value; }
        public bool? CancellationRequired { get => _cancellationRequired; set => _cancellationRequired = value; }
        public DateTime? CreditCardExpiration { get => _creditCardExpiration; set => _creditCardExpiration = value; }
        public string CreditCardNumber { get => creditCardNumber; set => creditCardNumber = value; }
        public string Currency { get => currency; set => currency = value; }
        public long? DeliveryDate { get => _deliveryDate; set => _deliveryDate = value; }
        public decimal? Density { get => _density; set => _density = value; }
        public string ExternalNote { get => externalNote; set => externalNote = value; }
        public string ExternalSource { get => externalSource; set => externalSource = value; }
        public string ExternalStatus { get => externalStatus; set => externalStatus = value; }
        public string FplType { get => fplType; set => fplType = value; }
        public decimal? FuelArrival { get => _fuelArrival; set => _fuelArrival = value; }
        public decimal? FuelBurned { get => _fuelBurned; set => _fuelBurned = value; }
        public decimal? FuelCost { get => _fuelCost; set => _fuelCost = value; }
        public string FuelCurrency { get => fuelCurrency; set => fuelCurrency = value; }
        public decimal? FuelPriceIndex { get => _fuelPriceIndex; set => _fuelPriceIndex = value; }
        public decimal? FuelPriceIndexCommercial { get => _fuelPriceIndexCommercial; set => _fuelPriceIndexCommercial = value; }
        public int? FuelProvider { get => _fuelProvider; set => _fuelProvider = value; }
        public decimal? FuelRemainig { get => _fuelRemainig; set => _fuelRemainig = value; }
        public decimal? FuelRemainigActual { get => _fuelRemainigActual; set => _fuelRemainigActual = value; }
        public DateTime? FuelReqReleaseSent { get => _fuelReqReleaseSent; set => _fuelReqReleaseSent = value; }
        public DateTime? FuelReqSent { get => _fuelReqSent; set => _fuelReqSent = value; }
        public string FuelTankUnit { get => fuelTankUnit; set => fuelTankUnit = value; }
        public string FuelType { get => fuelType; set => fuelType = value; }
        public string Notes { get => notes; set => notes = value; }
        public string Payment { get => payment; set => payment = value; }
        public string PaymentType { get => paymentType; set => paymentType = value; }
        public int? PermitProviderId { get => _permitProviderId; set => _permitProviderId = value; }
        public DateTime? PoCreatedDate { get => _poCreatedDate; set => _poCreatedDate = value; }
        public string PoNumber { get => poNumber; set => poNumber = value; }
        public decimal? Price { get => _price; set => _price = value; }
        public decimal? PriceComercial { get => _priceComercial; set => _priceComercial = value; }
        public decimal? PricePerUnit { get => _pricePerUnit; set => _pricePerUnit = value; }
        public decimal? PricePerUnitComercial { get => _pricePerUnitComercial; set => _pricePerUnitComercial = value; }
        public int? ProviderId { get => _providerId; set => _providerId = value; }
        public int? Quantity { get => _quantity; set => _quantity = value; }
        public bool? QuoteOnly { get => _quoteOnly; set => _quoteOnly = value; }
        public int? ReleaseAgentId { get => _releaseAgentId; set => _releaseAgentId = value; }
        public int? ReleaseDocument { get => _releaseDocument; set => _releaseDocument = value; }
        public string Status { get => status; set => status = value; }
        public string UnitDensity { get => unitDensity; set => unitDensity = value; }
        public DateTime? UpdatedDate { get => _updatedDate; set => _updatedDate = value; }
        public decimal? Uplift { get => _uplift; set => _uplift = value; }
        public decimal? UpliftMass { get => _upliftMass; set => _upliftMass = value; }
        public string UpliftUnit { get => upliftUnit; set => upliftUnit = value; }
        public FuelRelease FuelRelease { get => fuelRelease; set => fuelRelease = value; }
        public Release Release { get => release; set => release = value; }
    }

    [Serializable]
    public class EstimatedTimes
    {
        private int? _IdestimatedTimes;
        private int? _Idpostflight;
        private long? _blocksOff;
        private long? _blocksOn;
        private int? _estimatedPostflightPhase;
        private int? _estimatedPreflightPhase;
        private long? _landing;
        private long? _takeOff;

        public int? IdestimatedTimes { get => _IdestimatedTimes; set => _IdestimatedTimes = value; }
        public int? Idpostflight { get => _Idpostflight; set => _Idpostflight = value; }
        public long? BlocksOff { get => _blocksOff; set => _blocksOff = value; }
        public long? BlocksOn { get => _blocksOn; set => _blocksOn = value; }
        public int? EstimatedPostflightPhase { get => _estimatedPostflightPhase; set => _estimatedPostflightPhase = value; }
        public int? EstimatedPreflightPhase { get => _estimatedPreflightPhase; set => _estimatedPreflightPhase = value; }
        public long? Landing { get => _landing; set => _landing = value; }
        public long? TakeOff { get => _takeOff; set => _takeOff = value; }
    }

    [Serializable]
    public class Documents
    {
        private int? _Iddocuments;
        private int? _Idpostflight;
        private string by;
        private bool? _fmsUpdate;
        private bool? _occurence;
        private bool? _safaRampCheck;
        private string status;
        private List<Docs> _documents;
        private FlightLog _flightLog;
        private FuelDocs _fuel;
        private TechLog _techLog;

        public int? Iddocuments { get => _Iddocuments; set => _Iddocuments = value; }
        public int? Idpostflight { get => _Idpostflight; set => _Idpostflight = value; }
        public string By { get => by; set => by = value; }
        public bool? FmsUpdate { get => _fmsUpdate; set => _fmsUpdate = value; }
        public bool? Occurence { get => _occurence; set => _occurence = value; }
        public bool? SafaRampCheck { get => _safaRampCheck; set => _safaRampCheck = value; }
        public string Status { get => status; set => status = value; }
        public List<Docs> documents { get => _documents; set => _documents = value; }
        public FlightLog FlightLog { get => _flightLog; set => _flightLog = value; }
        public FuelDocs Fuel { get => _fuel; set => _fuel = value; }
        public TechLog TechLog { get => _techLog; set => _techLog = value; }
    }

    [Serializable]
    public class TechLog
    {
        private int? _IdtechLog;
        private int? _Iddocuments;
        private string customName;
        private string entityName;
        private int? _fileSize;
        private int? _imageSizeX;
        private int? _imageSizeY;
        private bool? _isThumbnail;
        private string name;
        private string originalName;
        private string path;
        private int? _refEntityId;
        private string status;
        private string uuid;

        public int? IdtechLog { get => _IdtechLog; set => _IdtechLog = value; }
        public int? Iddocuments { get => _Iddocuments; set => _Iddocuments = value; }
        public string CustomName { get => customName; set => customName = value; }
        public string EntityName { get => entityName; set => entityName = value; }
        public int? FileSize { get => _fileSize; set => _fileSize = value; }
        public int? ImageSizeX { get => _imageSizeX; set => _imageSizeX = value; }
        public int? ImageSizeY { get => _imageSizeY; set => _imageSizeY = value; }
        public bool? IsThumbnail { get => _isThumbnail; set => _isThumbnail = value; }
        public string Name { get => name; set => name = value; }
        public string OriginalName { get => originalName; set => originalName = value; }
        public string Path { get => path; set => path = value; }
        public int? RefEntityId { get => _refEntityId; set => _refEntityId = value; }
        public string Status { get => status; set => status = value; }
        public string Uuid { get => uuid; set => uuid = value; }
    }

    [Serializable]
    public class FuelDocs
    {
        private int? _Idfuel;
        private int? _Iddocuments;
        private string customName;
        private string entityName;
        private int? _fileSize;
        private int? _imageSizeX;
        private int? _imageSizeY;
        private bool? _isThumbnail;
        private string name;
        private string originalName;
        private string path;
        private int? _refEntityId;
        private string status;
        private string thumbnail;
        private string uuid;

        public int? Idfuel { get => _Idfuel; set => _Idfuel = value; }
        public int? Iddocuments { get => _Iddocuments; set => _Iddocuments = value; }
        public string CustomName { get => customName; set => customName = value; }
        public string EntityName { get => entityName; set => entityName = value; }
        public int? FileSize { get => _fileSize; set => _fileSize = value; }
        public int? ImageSizeX { get => _imageSizeX; set => _imageSizeX = value; }
        public int? ImageSizeY { get => _imageSizeY; set => _imageSizeY = value; }
        public bool? IsThumbnail { get => _isThumbnail; set => _isThumbnail = value; }
        public string Name { get => name; set => name = value; }
        public string OriginalName { get => originalName; set => originalName = value; }
        public string Path { get => path; set => path = value; }
        public int? RefEntityId { get => _refEntityId; set => _refEntityId = value; }
        public string Status { get => status; set => status = value; }
        public string Thumbnail { get => thumbnail; set => thumbnail = value; }
        public string Uuid { get => uuid; set => uuid = value; }
    }

    [Serializable]
    public class FlightLog
    {
        private int? _IdflightLog;
        private int? _Iddocuments;
        private string customName;
        private string entityName;
        private int? _fileSize;
        private int? _imageSizeX;
        private int? _imageSizeY;
        private bool? _isThumbnail;
        private string name;
        private string originalName;
        private string path;
        private int? _refEntityId;
        private string status;
        private string uuid;

        public int? IdflightLog { get => _IdflightLog; set => _IdflightLog = value; }
        public int? Iddocuments { get => _Iddocuments; set => _Iddocuments = value; }
        public string CustomName { get => customName; set => customName = value; }
        public string EntityName { get => entityName; set => entityName = value; }
        public int? FileSize { get => _fileSize; set => _fileSize = value; }
        public int? ImageSizeX { get => _imageSizeX; set => _imageSizeX = value; }
        public int? ImageSizeY { get => _imageSizeY; set => _imageSizeY = value; }
        public bool? IsThumbnail { get => _isThumbnail; set => _isThumbnail = value; }
        public string Name { get => name; set => name = value; }
        public string OriginalName { get => originalName; set => originalName = value; }
        public string Path { get => path; set => path = value; }
        public int? RefEntityId { get => _refEntityId; set => _refEntityId = value; }
        public string Status { get => status; set => status = value; }
        public string Uuid { get => uuid; set => uuid = value; }
    }

    [Serializable]
    public class Docs
    {
        private int? _Iddoc;
        private int? _Iddocuments;
        private string customName;
        private string entityName;
        private int? _fileSize;
        private int? _imageSizeX;
        private int? _imageSizeY;
        private bool? _isThumbnail;
        private string name;
        private string originalName;
        private string path;
        private int? _refEntityId;
        private string status;
        private string thumbnail;
        private string uuid;

        public int? Iddoc { get => _Iddoc; set => _Iddoc = value; }
        public int? Iddocuments { get => _Iddocuments; set => _Iddocuments = value; }
        public string CustomName { get => customName; set => customName = value; }
        public string EntityName { get => entityName; set => entityName = value; }
        public int? FileSize { get => _fileSize; set => _fileSize = value; }
        public int? ImageSizeX { get => _imageSizeX; set => _imageSizeX = value; }
        public int? ImageSizeY { get => _imageSizeY; set => _imageSizeY = value; }
        public bool? IsThumbnail { get => _isThumbnail; set => _isThumbnail = value; }
        public string Name { get => name; set => name = value; }
        public string OriginalName { get => originalName; set => originalName = value; }
        public string Path { get => path; set => path = value; }
        public int? RefEntityId { get => _refEntityId; set => _refEntityId = value; }
        public string Status { get => status; set => status = value; }
        public string Thumbnail { get => thumbnail; set => thumbnail = value; }
        public string Uuid { get => uuid; set => uuid = value; }
    }

    [Serializable]
    public class Deice
    {
        private int? _Iddeice;
        private int? _Idpostflight;
        private string by;
        private DateTime? _deIcingHoldOverTimeEnd;
        private DateTime? _deIcingHoldOverTimeStart;
        private int? _deIcingLiters;
        private int? _deIcingMix;
        private int? _deIcingOutsideAirTemperature;
        private string deIcingSignedOn;
        private DateTime? _deIcingStart;
        private string deIcingType;
        private string status;
        private List<Crew> _crew;
        private DeIcingSignedBy _deIcingSignedBy;

        public int? Iddeice { get => _Iddeice; set => _Iddeice = value; }
        public int? Idpostflight { get => _Idpostflight; set => _Idpostflight = value; }
        public string By { get => by; set => by = value; }
        public DateTime? DeIcingHoldOverTimeEnd { get => _deIcingHoldOverTimeEnd; set => _deIcingHoldOverTimeEnd = value; }
        public DateTime? DeIcingHoldOverTimeStart { get => _deIcingHoldOverTimeStart; set => _deIcingHoldOverTimeStart = value; }
        public int? DeIcingLiters { get => _deIcingLiters; set => _deIcingLiters = value; }
        public int? DeIcingMix { get => _deIcingMix; set => _deIcingMix = value; }
        public int? DeIcingOutsideAirTemperature { get => _deIcingOutsideAirTemperature; set => _deIcingOutsideAirTemperature = value; }
        public string DeIcingSignedOn { get => deIcingSignedOn; set => deIcingSignedOn = value; }
        public DateTime? DeIcingStart { get => _deIcingStart; set => _deIcingStart = value; }
        public string DeIcingType { get => deIcingType; set => deIcingType = value; }
        public string Status { get => status; set => status = value; }
        public List<Crew> Crew { get => _crew; set => _crew = value; }
        public DeIcingSignedBy DeIcingSignedBy { get => _deIcingSignedBy; set => _deIcingSignedBy = value; }
    }

    [Serializable]
    public class DeIcingSignedBy
    {
        private int? _IddeIcingSignedBy;
        private int? _Iddeice;
        private int? _accountName;
        private int? _firstName;
        private int? _height;
        private int? _jobTitle;
        private int? _lastName;
        private int? _middleName;
        private int? _nickname;
        private int? _Idoperator;
        private int? _personnelNumber;
        private int? _pilot;
        private int? _status;
        private int? _weight;
        private Operator @operator;
        private UserCharacteristics userCharacteristics;

        public int? IddeIcingSignedBy { get => _IddeIcingSignedBy; set => _IddeIcingSignedBy = value; }
        public int? Iddeice { get => _Iddeice; set => _Iddeice = value; }
        public int? AccountName { get => _accountName; set => _accountName = value; }
        public int? FirstName { get => _firstName; set => _firstName = value; }
        public int? Height { get => _height; set => _height = value; }
        public int? JobTitle { get => _jobTitle; set => _jobTitle = value; }
        public int? LastName { get => _lastName; set => _lastName = value; }
        public int? MiddleName { get => _middleName; set => _middleName = value; }
        public int? Nickname { get => _nickname; set => _nickname = value; }
        public int? Idoperator { get => _Idoperator; set => _Idoperator = value; }
        public int? PersonnelNumber { get => _personnelNumber; set => _personnelNumber = value; }
        public int? Pilot { get => _pilot; set => _pilot = value; }
        public int? Status { get => _status; set => _status = value; }
        public int? Weight { get => _weight; set => _weight = value; }
        public Operator Operator { get => @operator; set => @operator = value; }
        public UserCharacteristics UserCharacteristics { get => userCharacteristics; set => userCharacteristics = value; }
    }

    [Serializable]
    public class Crew
    {
        private int? _Idcrew;
        private int? _Iddeice;
        private string accountName;
        private string firstName;
        private decimal? _height;
        private string jobTitle;
        private string lastName;
        private string middleName;
        private string nickname;
        private int? _Idoperator;
        private string personnelNumber;
        private bool? _pilot;
        private string status;
        private decimal? _weight;
        private Operator @operator;
        private UserCharacteristics userCharacteristics;

        public int? Idcrew { get => _Idcrew; set => _Idcrew = value; }
        public int? Iddeice { get => _Iddeice; set => _Iddeice = value; }
        public string AccountName { get => accountName; set => accountName = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public decimal? Height { get => _height; set => _height = value; }
        public string JobTitle { get => jobTitle; set => jobTitle = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string MiddleName { get => middleName; set => middleName = value; }
        public string Nickname { get => nickname; set => nickname = value; }
        public int? Idoperator { get => _Idoperator; set => _Idoperator = value; }
        public string PersonnelNumber { get => personnelNumber; set => personnelNumber = value; }
        public bool? Pilot { get => _pilot; set => _pilot = value; }
        public string Status { get => status; set => status = value; }
        public decimal? Weight { get => _weight; set => _weight = value; }
        public Operator Operator { get => @operator; set => @operator = value; }
        public UserCharacteristics UserCharacteristics { get => userCharacteristics; set => userCharacteristics = value; }
    }

    [Serializable]
    public class UserCharacteristics
    {
        private int? _IduserCharacteristics;
        private int? _Idcrew;
        private int? _IddeIcingSignedBy;
        private int? _Idcmd;
        private int? _Idfo;
        private string defaultLuggageWeight;
        private string eyeColor;
        private string hairColor;

        public int? IduserCharacteristics { get => _IduserCharacteristics; set => _IduserCharacteristics = value; }
        public int? Idcrew { get => _Idcrew; set => _Idcrew = value; }
        public int? IddeIcingSignedBy { get => _IddeIcingSignedBy; set => _IddeIcingSignedBy = value; }
        public int? Idcmd { get => _Idcmd; set => _Idcmd = value; }
        public int? Idfo { get => _Idfo; set => _Idfo = value; }
        public string DefaultLuggageWeight { get => defaultLuggageWeight; set => defaultLuggageWeight = value; }
        public string EyeColor { get => eyeColor; set => eyeColor = value; }
        public string HairColor { get => hairColor; set => hairColor = value; }
    }

    [Serializable]
    public class Cargo
    {
        private int? _Idcargo;
        private int? _Idpostflight;
        private string cargoUnit;
        private double? cargoWeight;

        public int? Idcargo { get => _Idcargo; set => _Idcargo = value; }
        public int? Idpostflight { get => _Idpostflight; set => _Idpostflight = value; }
        public string CargoUnit { get => cargoUnit; set => cargoUnit = value; }
        public double? CargoWeight { get => cargoWeight; set => cargoWeight = value; }
    }

    [Serializable]
    public class ArrFuel
    {
        private int? _id;
        private int? _Idpostflight;
        private int? _agentId;
        private int? _airportId;
        private string by;
        private bool? _cancellationRequired;
        private DateTime? _creditCardExpiration;
        private string creditCardNumber;
        private string currency;
        private long? _deliveryDate;
        private decimal? _density;
        private string externalNote;
        private string externalSource;
        private string externalStatus;
        private string fplType;
        private int? _fuelArrival;
        private int? _fuelBurned;
        private int? _fuelCost;
        private string fuelCurrency;
        private decimal? _fuelPriceIndex;
        private decimal? _fuelPriceIndexCommercial;
        private int? _fuelProvider;
        private decimal? _fuelRemainig;
        private decimal? _fuelRemainigActual;
        private DateTime? _fuelReqReleaseSent;
        private DateTime? _fuelReqSent;
        private string fuelTankUnit;
        private string fuelType;
        private string notes;
        private string payment;
        private string paymentType;
        private int? _permitProviderId;
        private DateTime? _poCreatedDate;
        private string poNumber;
        private decimal? _price;
        private decimal? _priceComercial;
        private decimal? _pricePerUnit;
        private decimal? _pricePerUnitComercial;
        private int? _providerId;
        private int? _quantity;
        private bool? _quoteOnly;
        private int? _releaseAgentId;
        private int? _releaseDocument;
        private string status;
        private string unitDensity;
        private string updatedDate;
        private string uplift;
        private string upliftMass;
        private string upliftUnit;

        private FuelRelease _fuelRelease;
        private Release _release;

        public int? Id { get => _id; set => _id = value; }
        public int? Idpostflight { get => _Idpostflight; set => _Idpostflight = value; }
        public int? AgentId { get => _agentId; set => _agentId = value; }
        public int? AirportId { get => _airportId; set => _airportId = value; }
        public string By { get => by; set => by = value; }
        public bool? CancellationRequired { get => _cancellationRequired; set => _cancellationRequired = value; }
        public DateTime? CreditCardExpiration { get => _creditCardExpiration; set => _creditCardExpiration = value; }
        public string CreditCardNumber { get => creditCardNumber; set => creditCardNumber = value; }
        public string Currency { get => currency; set => currency = value; }
        public long? DeliveryDate { get => _deliveryDate; set => _deliveryDate = value; }
        public decimal? Density { get => _density; set => _density = value; }
        public string ExternalNote { get => externalNote; set => externalNote = value; }
        public string ExternalSource { get => externalSource; set => externalSource = value; }
        public string ExternalStatus { get => externalStatus; set => externalStatus = value; }
        public string FplType { get => fplType; set => fplType = value; }
        public int? FuelArrival { get => _fuelArrival; set => _fuelArrival = value; }
        public int? FuelBurned { get => _fuelBurned; set => _fuelBurned = value; }
        public int? FuelCost { get => _fuelCost; set => _fuelCost = value; }
        public string FuelCurrency { get => fuelCurrency; set => fuelCurrency = value; }
        public decimal? FuelPriceIndex { get => _fuelPriceIndex; set => _fuelPriceIndex = value; }
        public decimal? FuelPriceIndexCommercial { get => _fuelPriceIndexCommercial; set => _fuelPriceIndexCommercial = value; }
        public int? FuelProvider { get => _fuelProvider; set => _fuelProvider = value; }
        public decimal? FuelRemainig { get => _fuelRemainig; set => _fuelRemainig = value; }
        public decimal? FuelRemainigActual { get => _fuelRemainigActual; set => _fuelRemainigActual = value; }
        public DateTime? FuelReqReleaseSent { get => _fuelReqReleaseSent; set => _fuelReqReleaseSent = value; }
        public DateTime? FuelReqSent { get => _fuelReqSent; set => _fuelReqSent = value; }
        public string FuelTankUnit { get => fuelTankUnit; set => fuelTankUnit = value; }
        public string FuelType { get => fuelType; set => fuelType = value; }
        public string Notes { get => notes; set => notes = value; }
        public string Payment { get => payment; set => payment = value; }
        public string PaymentType { get => paymentType; set => paymentType = value; }
        public int? PermitProviderId { get => _permitProviderId; set => _permitProviderId = value; }
        public DateTime? PoCreatedDate { get => _poCreatedDate; set => _poCreatedDate = value; }
        public string PoNumber { get => poNumber; set => poNumber = value; }
        public decimal? Price { get => _price; set => _price = value; }
        public decimal? PriceComercial { get => _priceComercial; set => _priceComercial = value; }
        public decimal? PricePerUnit { get => _pricePerUnit; set => _pricePerUnit = value; }
        public decimal? PricePerUnitComercial { get => _pricePerUnitComercial; set => _pricePerUnitComercial = value; }
        public int? ProviderId { get => _providerId; set => _providerId = value; }
        public int? Quantity { get => _quantity; set => _quantity = value; }
        public bool? QuoteOnly { get => _quoteOnly; set => _quoteOnly = value; }
        public int? ReleaseAgentId { get => _releaseAgentId; set => _releaseAgentId = value; }
        public int? ReleaseDocument { get => _releaseDocument; set => _releaseDocument = value; }
        public string Status { get => status; set => status = value; }
        public string UnitDensity { get => unitDensity; set => unitDensity = value; }
        public string UpdatedDate { get => updatedDate; set => updatedDate = value; }
        public string Uplift { get => uplift; set => uplift = value; }
        public string UpliftMass { get => upliftMass; set => upliftMass = value; }
        public string UpliftUnit { get => upliftUnit; set => upliftUnit = value; }
        public FuelRelease FuelRelease { get => _fuelRelease; set => _fuelRelease = value; }
        public Release Release { get => _release; set => _release = value; }
    }

    [Serializable]
    public class FuelRelease
    {
        private int? _Id;
        private int? _IdarrFuel;
        private int? _Idfuel;
        private string customName;
        private string entityName;
        private int? _fileSize;
        private int? _imageSizeX;
        private int? _imageSizeY;
        private int? _isThumbnail;
        private string name;
        private string originalName;
        private string path;
        private int? _refEntityId;
        private string status;
        private string thumbnail;
        private string uuid;

        public int? Id { get => _Id; set => _Id = value; }
        public int? IdarrFuel { get => _IdarrFuel; set => _IdarrFuel = value; }
        public int? Idfuel { get => _Idfuel; set => _Idfuel = value; }
        public string CustomName { get => customName; set => customName = value; }
        public string EntityName { get => entityName; set => entityName = value; }
        public int? FileSize { get => _fileSize; set => _fileSize = value; }
        public int? ImageSizeX { get => _imageSizeX; set => _imageSizeX = value; }
        public int? ImageSizeY { get => _imageSizeY; set => _imageSizeY = value; }
        public int? IsThumbnail { get => _isThumbnail; set => _isThumbnail = value; }
        public string Name { get => name; set => name = value; }
        public string OriginalName { get => originalName; set => originalName = value; }
        public string Path { get => path; set => path = value; }
        public int? RefEntityId { get => _refEntityId; set => _refEntityId = value; }
        public string Status { get => status; set => status = value; }
        public string Thumbnail { get => thumbnail; set => thumbnail = value; }
        public string Uuid { get => uuid; set => uuid = value; }
    }

    [Serializable]
    public class Release
    {
        private int? _id;
        private int? _IdarrFuel;
        private int? _Idfuel;
        private string by;
        private string status;

        public int? Id { get => _id; set => _id = value; }
        public int? IdarrFuel { get => _IdarrFuel; set => _IdarrFuel = value; }
        public int? Idfuel { get => _Idfuel; set => _Idfuel = value; }
        public string By { get => by; set => by = value; }
        public string Status { get => status; set => status = value; }
    }

    [Serializable]
    public class BitacoraFlex
    {
        private string _AeronaveMatricula;
        private string _FolioReal;
        private string _VueloContratoId;
        private string _PilotoId;
        private string _CopilotoId;
        private DateTime? _Fecha;
        private string _Origen;
        private string _Destino;
        private DateTime? _OrigenVuelo;
        private DateTime? _OrigenCalzo;
        private decimal _ConsumoOri;
        private int _CantPax;
        private string _Tipo;
        private DateTime? _DestinoVuelo;
        private DateTime? _DestinoCalzo;
        private decimal _ConsumoDes;
        private string _LogNum;
        private int _TripNum;
        private int _Leg_Num;
        private int _LegId;

        public string AeronaveMatricula { get => _AeronaveMatricula; set => _AeronaveMatricula = value; }
        public string FolioReal { get => _FolioReal; set => _FolioReal = value; }
        public string VueloContratoId { get => _VueloContratoId; set => _VueloContratoId = value; }
        public string PilotoId { get => _PilotoId; set => _PilotoId = value; }
        public string CopilotoId { get => _CopilotoId; set => _CopilotoId = value; }
        public DateTime? Fecha { get => _Fecha; set => _Fecha = value; }
        public string Origen { get => _Origen; set => _Origen = value; }
        public string Destino { get => _Destino; set => _Destino = value; }
        public DateTime? OrigenVuelo { get => _OrigenVuelo; set => _OrigenVuelo = value; }
        public DateTime? OrigenCalzo { get => _OrigenCalzo; set => _OrigenCalzo = value; }
        public decimal ConsumoOri { get => _ConsumoOri; set => _ConsumoOri = value; }
        public int CantPax { get => _CantPax; set => _CantPax = value; }
        public string Tipo { get => _Tipo; set => _Tipo = value; }
        public DateTime? DestinoVuelo { get => _DestinoVuelo; set => _DestinoVuelo = value; }
        public DateTime? DestinoCalzo { get => _DestinoCalzo; set => _DestinoCalzo = value; }
        public decimal ConsumoDes { get => _ConsumoDes; set => _ConsumoDes = value; }
        public string LogNum { get => _LogNum; set => _LogNum = value; }
        public int TripNum { get => _TripNum; set => _TripNum = value; }
        public int Leg_Num { get => _Leg_Num; set => _Leg_Num = value; }
        public int LegId { get => _LegId; set => _LegId = value; }
    }
}