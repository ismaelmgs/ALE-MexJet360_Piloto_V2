using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class Pasajero
    {
        private int _iIdPasajero = -1;
        private string _sCodigoCliente = string.Empty;
        private int _iStatus = 1;
        private string _sUsuarioCreacion = string.Empty;
        private string _sUsuarioMod = string.Empty;
        private string _sIP = string.Empty;
        private int _iIdCliente = -1;
        private string _spaxname = string.Empty;
        private string _slast_name = string.Empty;
        private string _sfirst_name = string.Empty;
        private string _sPasatiempos = string.Empty;
        private string _sPerfilLinkedin = string.Empty;
        private string _sPerfilFacebook = string.Empty;
        private string _sPerfilInstagram = string.Empty;
        private string _sPerfilTwitter = string.Empty;
        private int _iEstadoCivil = 0;
        private int _iProtocoloCliente = 0;
        private int _iAlergias = 0;
        private string _sCualesAlergias = string.Empty;
        private int _iCondicionesMedicasEspeciales = 0;
        private string _sCualesCondicionesMedicasEspeciales = string.Empty;
        private int _iFobias = 0;
        private string _sCualesFobias = string.Empty;
        private int _iMultiplesNacionalidades = 0;
        private string _sCualesMultiplesNacionalidades = string.Empty;
        private string _sRestriccionesAlimenticias = string.Empty;
        private string _sDeportes = string.Empty;
        

        public int iIdPasajero
        {
            get { return _iIdPasajero; }
            set { _iIdPasajero = value; }
        }
        public string sCodigoCliente
        {
            get { return _sCodigoCliente; }
            set { _sCodigoCliente = value; }
        }
        public int iStatus
        {
            get { return _iStatus; }
            set { _iStatus = value; }
        }
        public string sUsuarioCreacion
        {
            get { return _sUsuarioCreacion; }
            set { _sUsuarioCreacion = value; }
        }
        public string sUsuarioMod
        {
            get { return _sUsuarioMod; }
            set { _sUsuarioMod = value; }
        }
        public string sIP
        {
            get { return _sIP; }
            set { _sIP = value; }
        }
        public int iIdCliente
        {
            get { return _iIdCliente; }
            set { _iIdCliente = value; }
        }
        public string sPaxname
        {
            get { return _spaxname; }
            set { _spaxname = value; }
        }
        public string sLast_name
        {
            get { return _slast_name; }
            set { _slast_name = value; }
        }
        public string sFirst_name
        {
            get { return _sfirst_name; }
            set { _sfirst_name = value; }
        }
        public string sPasatiempos
        {
            get { return _sPasatiempos; }
            set { _sPasatiempos = value; }
        }
        public string sPerfilLinkedin
        {
            get { return _sPerfilLinkedin; }
            set { _sPerfilLinkedin = value; }
        }
        public string sPerfilFacebook
        {
            get { return _sPerfilFacebook; }
            set { _sPerfilFacebook = value; }
        }
        public string sPerfilInstagram
        {
            get { return _sPerfilInstagram; }
            set { _sPerfilInstagram = value; }
        }
        public string sPerfilTwitter
        {
            get { return _sPerfilTwitter; }
            set { _sPerfilTwitter = value; }
        }
        public int iEstadoCivil
        {
            get { return _iEstadoCivil; }
            set { _iEstadoCivil = value; }
        }
        public int iProtocoloCliente
        {
            get { return _iProtocoloCliente; }
            set { _iProtocoloCliente = value; }
        }
        public int iAlergias
        {
            get { return _iAlergias; }
            set { _iAlergias = value; }
        }
        public string sCualesAlergias
        {
            get { return _sCualesAlergias; }
            set { _sCualesAlergias = value; }
        }
        public int iCondicionesMedicasEspeciales
        {
            get { return _iCondicionesMedicasEspeciales; }
            set { _iCondicionesMedicasEspeciales = value; }
        }
        public string sCualesCondicionesMedicasEspeciales
        {
            get { return _sCualesCondicionesMedicasEspeciales; }
            set { _sCualesCondicionesMedicasEspeciales = value; }
        }
        public int iFobias
        {
            get { return _iFobias; }
            set { _iFobias = value; }
        }
        public string sCualesFobias
        {
            get { return _sCualesFobias; }
            set { _sCualesFobias = value; }
        }
        public int iMultiplesNacionalidades
        {
            get { return _iMultiplesNacionalidades; }
            set { _iMultiplesNacionalidades = value; }
        }
        public string sCualesMultiplesNacionalidades
        {
            get { return _sCualesMultiplesNacionalidades; }
            set { _sCualesMultiplesNacionalidades = value; }
        }
        public string sRestriccionesAlimenticias
        {
            get { return _sRestriccionesAlimenticias; }
            set { _sRestriccionesAlimenticias = value; }
        }
        public string sDeportes
        {
            get { return _sDeportes; }
            set { _sDeportes = value; }
        }
    }
}