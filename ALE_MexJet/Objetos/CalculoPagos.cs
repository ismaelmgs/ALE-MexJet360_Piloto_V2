using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    [Serializable]
    public class PilotosCalculo
    {
        private string _sCrewCode = string.Empty;
        private decimal _dImporte = 0;
        List<VuelosPiloto> _oLst = new List<VuelosPiloto>();

        public string sCrewCode { get { return _sCrewCode; } set { _sCrewCode = value; } }
        public decimal dImporte { get { return _dImporte; } set { _dImporte = value; } }
        public List<VuelosPiloto> oLst { get { return _oLst; } set { _oLst = value; } }
    }

    [Serializable]
    public class VuelosPiloto
    {
        private int _iNoVuelo = 0;
        private int _iNoPierna = 0;
        private int _ilegid = 0;
        private DateTime _dtFechaDia = DateTime.Now;
        private string _sClavePiloto = string.Empty;
        private string _sPod = string.Empty;
        private string _sPoa = string.Empty;
        private DateTime _dtFechaSalida = DateTime.Now;
        private string _sFechaLlegada = string.Empty;
        private DateTime _dtLocdep = DateTime.Now;
        private DateTime _dtLocarr = DateTime.Now;
        private string _sCrewcode = string.Empty;
        private string _sDutytype = string.Empty;
        private bool _bVloNal = true;
        private bool _bSeCobra = true;
        private string _sHomeBase = string.Empty;

        public int iNoVuelo { get { return _iNoVuelo; } set { _iNoVuelo = value; } }
        public int iNoPierna { get { return _iNoPierna; } set { _iNoPierna = value; } }
        public int ilegid { get { return _ilegid; } set { _ilegid = value; } }
        public DateTime dtFechaDia { get { return _dtFechaDia; } set { _dtFechaDia = value; } }
        public string sClavePiloto { get { return _sClavePiloto; } set { _sClavePiloto = value; } }
        public string sPod { get { return _sPod; } set { _sPod = value; } }
        public string sPoa { get { return _sPoa; } set { _sPoa = value; } }
        public DateTime dtFechaSalida { get { return _dtFechaSalida; } set { _dtFechaSalida = value; } }
        public string sFechaLlegada { get { return _sFechaLlegada; } set { _sFechaLlegada = value; } }
        public DateTime dtLocdep { get { return _dtLocdep; } set { _dtLocdep = value; } }
        public DateTime dtLocarr { get { return _dtLocarr; } set { _dtLocarr = value; } }
        public string sCrewcode { get { return _sCrewcode; } set { _sCrewcode = value; } }
        public string sDutytype { get { return _sDutytype; } set { _sDutytype = value; } }
        public bool bVloNal { get { return _bVloNal; } set { _bVloNal = value; } }
        public bool bSeCobra { get { return _bSeCobra; } set { _bSeCobra = value; } }
        public string sHomeBase { get { return _sHomeBase; } set { _sHomeBase = value; } }
    }

    [Serializable]
    public class CantidadComidas
    {
        private string _sCrewCode = string.Empty;
        private DateTime _dtFechaInicio = DateTime.Now;
        private DateTime _dtFechaFin = DateTime.Now;
        private DataTable _dtVuelos = new DataTable();
        //private DataTable _dtNC = new DataTable();
        private int _iVuelo = 0;
        private int _iCantDesayunos = 0;
        private int _iCantComidas = 0;
        private int _iCantCenas = 0;
        private int _iCantDesayunosInt = 0;
        private int _iCantComidasInt = 0;
        private int _iCantCenasInt = 0;
        private DataTable _dtDias = new DataTable();
        private List<ComidasPorDia> _oLstPorDia = new List<ComidasPorDia>();
        private List<PernoctasDayUse> _oLstPer = new List<PernoctasDayUse>();

        public string sCrewCode { get { return _sCrewCode; } set { _sCrewCode = value; } }
        public DateTime dtFechaInicio { get { return _dtFechaInicio; } set { _dtFechaInicio = value; } }
        public DateTime dtFechaFin { get { return _dtFechaFin; } set { _dtFechaFin = value; } }
        public DataTable dtVuelos { get { return _dtVuelos; } set { _dtVuelos = value; } }
        //public DataTable dtNC { get { return _dtNC; } set { _dtNC = value; } }

        public int iVuelo { get { return _iVuelo; } set { _iVuelo = value; } }

        public int iCantDesayunos { get { return _iCantDesayunos; } set { _iCantDesayunos = value; } }
        public int iCantComidas { get { return _iCantComidas; } set { _iCantComidas = value; } }
        public int iCantCenas { get { return _iCantCenas; } set { _iCantCenas = value; } }

        public int iCantDesayunosInt { get { return _iCantDesayunosInt; } set { _iCantDesayunosInt = value; } }
        public int iCantComidasInt { get { return _iCantComidasInt; } set { _iCantComidasInt = value; } }
        public int iCantCenasInt { get { return _iCantCenasInt; } set { _iCantCenasInt = value; } }

        public DataTable dtDias { get { return _dtDias; } set { _dtDias = value; } }
        public List<ComidasPorDia> oLstPorDia { get { return _oLstPorDia; } set { _oLstPorDia = value; } }

        public List<PernoctasDayUse> oLstPer { get { return _oLstPer; } set { _oLstPer = value; } }
    }

    [Serializable]
    public class ComidasPorDia
    {
        private string _sClavePiloto = string.Empty;
        private DateTime _dtFechaDia = new DateTime();
        private int _iDesayunosNal = 0;
        private int _iComidaNal = 0;
        private int _iCenaNal = 0;
        private int _iDesayunosInt = 0;
        private int _iComidaInt = 0;
        private int _iCenaInt = 0;
        private string _sduty_type = string.Empty;
        private string _sOrigen = string.Empty;
        private string _sDestino = string.Empty;
        private DateTime _dtFechaFin = new DateTime();


        public string sClavePiloto { set { _sClavePiloto = value; } get { return _sClavePiloto; } }
        public DateTime dtFechaDia { set { _dtFechaDia = value; } get { return _dtFechaDia; } }
        public int iDesayunosNal { set { _iDesayunosNal = value; } get { return _iDesayunosNal; } }
        public int iComidaNal { set { _iComidaNal = value; } get { return _iComidaNal; } }
        public int iCenaNal { set { _iCenaNal = value; } get { return _iCenaNal; } }
        public int iDesayunosInt { set { _iDesayunosInt = value; } get { return _iDesayunosInt; } }
        public int iComidaInt { set { _iComidaInt = value; } get { return _iComidaInt; } }
        public int iCenaInt { set { _iCenaInt = value; } get { return _iCenaInt; } }
        public string sduty_type { set { _sduty_type = value; } get { return _sduty_type; } }
        public string sOrigen { set { _sOrigen = value; } get { return _sOrigen; } }
        public string sDestino { set { _sDestino = value; } get { return _sDestino; } }
        public DateTime dtFechaFin { set { _dtFechaFin = value; } get { return _dtFechaFin; } }

    }

    [Serializable]
    public class HorarioAlimentos
    {
        public float fInicioDesayuno { set; get; }
        public float fFinDesayuno { set; get; }
        public float fInicioComida { set; get; }
        public float fFinComida { set; get; }
        public float fInicioCena { set; get; }
        public float fFinCena { set; get; }
    }

    [Serializable]
    public class PernoctasDayUse
    {
        private DateTime _dtFecha = new DateTime();
        private int _iNoPernoctas = 0;
        private int _iDayUse = 0;

        public DateTime dtFecha { get { return _dtFecha; } set { _dtFecha = value; } }
        public int iNoPernoctas { get { return _iNoPernoctas; } set { _iNoPernoctas = value; } }
        public int iDayUse { get { return _iDayUse; } set { _iDayUse = value; } }
    }

    [Serializable]
    public class CantidadHoteles
    {
        public string sCrewCode { get; set; }
        public DateTime dtFechaInicio { get; set; }
        public DateTime dtFechaFin { get; set; }
        public int iDescanso { get; set; }
        public int iCantHotelesNac { get; set; }
        public int iCantHotelesInt { get; set; }
        public DataTable dtVuelos { get; set; }
        public List<HotelesPorDia> oLstHotPorDia { get; set; }
    }
    [Serializable]
    public class HotelesPorDia
    {
        public string sCvePiloto { get; set; }
        public DateTime dtFechaDia { get; set; }
        public int iDesNal { get; set; }
        public int iDesInt { get; set; }
        public string sOrigen { get; set; }
        public string sDestino { get; set; }
    }
    [Serializable]
    public class HorarioHoteles
    {
        public float fInicioDescanso { set; get; }
        public float fFinDescanso { set; get; }
    }
}