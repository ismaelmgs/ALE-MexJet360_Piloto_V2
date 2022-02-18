using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    public static class Enumeraciones
    {
        public enum TipoOperacion : int
        {
            Consultar = 1,
            Insertar = 2,
            Actualizar = 3,
            Eliminar = 4,
            Validar = 5,
            Clonar = 6
        };

        public enum TipoAccion : int
        {
            Consultar = 1,
            Insertar = 2,
            Actualizar = 3,
            Eliminar = 4,
            Acceso = 5,
            Exportar = 6
        };

        public enum TipoMonedaSyteLine : int
        {
            CTEMXN = 1,
            CTEUSD = 2
        }

        public enum operacionBoton
        {
            Insertar,
            Actualizar,
            Eliminar
        };

        public enum SeCobraFerrys
        {
            Ninguno = 0,
            Todos = 1,
            Reposicionamiento = 2
        }

        public enum TiposEspera
        {
            Libre = 0,
            HorasPorVuelo = 1,
            FactorHrsVuelo = 2
        }

        public enum ConceptosRemision
        {
            VueloNacional = 1,
            VueloInternacional = 2,
            EsperaNacional = 3,
            EsperaInternacional = 4,
            PernoctaNacional = 5,
            PernoctaInternacional = 6
        }

        public enum CobroCombustible
        {
            Ninguno = -1,
            Rango = 0,
            Promedio = 1,
            Real = 2,
            HorasDescuento = 3
        }

       
        public enum Pantallas : int
        {
            Cliente = 10,
            Producto = 11,
            Comisariato = 12,
            ComisariatoProducto = 13,
            RangoCombustible = 14,
            Aeropuerto = 15,
            Area = 16,
            Designador = 17,
            DistanciasOrtodromicas = 18,
            Flota = 19,
            GrupoModelo = 20,
            Inflacion = 21,
            Marcas = 22,
            Modelo = 23,
            Motivo = 24,
            Piloto = 25,
            Sector = 26,
            ServiciosConCargo = 27,
            TipoCambio = 28,
            TipoCliente = 29,
            TipoContacto = 30,
            TipoDemora = 31,
            TipoFactura = 32,
            TipoPierna = 33,
            Titulo = 34,
            Rol = 35,
            Usuario = 36,
            ContactosPreferencias = 37,
            NotaCredito = 38,
            Remisiones = 39,
            AltaNotasCredito = 40,
            ConsultaNotasCredito = 41,
            PermisosRol = 42,
            MonitorCliente = 43,
            ConsultaRemisiones = 44,
            GeneracionRemisiones = 45,
            Aeronaves = 46,
            TramoPactado = 47,
            TUA = 48,
            Parametros = 49,
            Consultas = 50,
            BitacorasTransferidas = 51,
            CombustibleMensualInternacional = 52,
            CombustibleSemanal = 53,
            MonitorImagen = 54,
            Paquete = 55,
            FechaPico = 56,
            Contrato = 57,
            Vendedor = 58,
            SolicitudVuelo = 59,
            ConsultaSolicitudes = 60,
            ConsultaCasos = 61,
            Audit = 62,
            BitacoraAudit = 63,
            AltaContratos = 64,
            NotasdeCredito = 65,
            ConsultaContrato = 66,
            FBO = 67,
            RecepcionComision = 68,
            ErroresBitacoras = 69,
            GastosInternos = 70,
            MonitorComisariato = 71,
            ConsultaTarifas = 72,
            PagoAPHIS = 73,
            PagoSENEAM = 74,
            TraspasoHora = 75,
            DashboardATC  = 76,
            Prefactura = 77,
            ConsultaPrefactura = 78,
            FacturaProveedor = 80,
            VuelosSinBitacora = 81,
            HorasVoladasCliente = 82,
            MonitorDespacho = 91,
            CotizacionesVuelo = 95,
            MonitorTrafico = 96,
            MonitorAtencionClientes = 99,
            Notifications = 117,
            ListaDifusion = 100,
            PersonaDifusion = 101,
            Membresias = 126,
            FactorFijo = 133
        }
        public enum Mensajes : int
        {
            Insercion = 0,
            Modificacion = 1,
            Eliminacion = 2,
            Aviso = 3,
            Clonacion = 4,
            Cancelacion = 5,
            TiempMinVulBN = 6,
            RangAcomDiaFer = 7,
            RepSalAntSal = 8,
            CanBase = 9,
            CanNoBase = 10
        }
        public enum Bloqueo : int
        {
            Permitido = 1,
            Bloqueado = 0
        }

        public enum EnumUserAccountControl : int
        {
            /* Comentarios sobre la enumeración.
            *  Diccionario de como funciona los codigos.
            *
            512     = 512               = Normal account
            514     = 512 + 2           = Normal account, disabled
            546     = 512 + 32 + 2      = Normal account, disabled, no password required
            2080    = 2048 + 32         = Interdomain trust, no password required
            66048   = 65536 + 512       = Normal account. password never expires
            66050   = 65536 + 512 + 2   = Normal account. password never expires, disabled
            66080   = 65536 + 512 + 32  = Normal account. password never expires, no password required
            *
            */

            /// <summary>
            /// The logon script is executed. 
            ///</summary>
            SCRIPT = 0x00000001,

            /// <summary>
            /// The user account is disabled. 
            ///</summary>
            ACCOUNTDISABLE = 514,//0x00000002,

            /// <summary>
            /// The home directory is required. 
            ///</summary>
            HOMEDIR_REQUIRED = 0x00000008,

            /// <summary>
            /// The account is currently locked out. 
            ///</summary>
            LOCKOUT = 528,//0x00000010,

            /// <summary>
            /// No password is required. 
            ///</summary>
            PASSWD_NOTREQD = 0x00000020,

            /// <summary>
            /// The user cannot change the password. 
            ///</summary>
            /// <remarks>
            /// Note:  You cannot assign the permission settings of PASSWD_CANT_CHANGE by directly modifying the UserAccountControl attribute. 
            /// For more information and a code example that shows how to prevent a user from changing the password, see User Cannot Change Password.
            // </remarks>
            PASSWD_CANT_CHANGE = 0x00000040,

            /// <summary>
            /// The user can send an encrypted password. 
            ///</summary>
            ENCRYPTED_TEXT_PASSWORD_ALLOWED = 0x00000080,

            /// <summary>
            /// This is an account for users whose primary account is in another domain. This account provides user access to this domain, but not 
            /// to any domain that trusts this domain. Also known as a local user account. 
            ///</summary>
            TEMP_DUPLICATE_ACCOUNT = 0x00000100,

            /// <summary>
            /// This is a default account type that represents a typical user. 
            ///</summary>
            NORMAL_ACCOUNT = 0x00000200,

            /// <summary>
            /// This is a permit to trust account for a system domain that trusts other domains. 
            ///</summary>
            INTERDOMAIN_TRUST_ACCOUNT = 0x00000800,

            /// <summary>
            /// This is a computer account for a computer that is a member of this domain. 
            ///</summary>
            WORKSTATION_TRUST_ACCOUNT = 0x00001000,

            /// <summary>
            /// This is a computer account for a system backup domain controller that is a member of this domain. 
            ///</summary>
            SERVER_TRUST_ACCOUNT = 0x00002000,

            /// <summary>
            /// Not used. 
            ///</summary>
            Unused1 = 0x00004000,

            /// <summary>
            /// Not used. 
            ///</summary>
            Unused2 = 0x00008000,

            /// <summary>
            /// The password for this account will never expire. 
            ///</summary>
            DONT_EXPIRE_PASSWD = 0x00010000,

            /// <summary>
            /// This is an MNS logon account. 
            ///</summary>
            MNS_LOGON_ACCOUNT = 0x00020000,

            /// <summary>
            /// The user must log on using a smart card. 
            ///</summary>
            SMARTCARD_REQUIRED = 0x00040000,

            /// <summary>
            /// The service account (user or computer account), under which a service runs, is trusted for Kerberos delegation. Any such service 
            /// can impersonate a client requesting the service. 
            ///</summary>
            TRUSTED_FOR_DELEGATION = 0x00080000,

            /// <summary>
            /// The security context of the user will not be delegated to a service even if the service account is set as trusted for Kerberos delegation. 
            ///</summary>
            NOT_DELEGATED = 0x00100000,

            /// <summary>
            /// Restrict this principal to use only Data Encryption Standard (DES) encryption types for keys. 
            ///</summary>
            USE_DES_KEY_ONLY = 0x00200000,

            /// <summary>
            /// This account does not require Kerberos pre-authentication for logon. 
            ///</summary>
            DONT_REQUIRE_PREAUTH = 0x00400000,

            /// <summary>
            /// The user password has expired. This flag is created by the system using data from the Pwd-Last-Set attribute and the domain policy. 
            ///</summary>
            PASSWORD_EXPIRED = 0x00800000,

            /// <summary>
            /// The account is enabled for delegation. This is a security-sensitive setting; accounts with this option enabled should be strictly 
            /// controlled. This setting enables a service running under the account to assume a client identity and authenticate as that user to 
            /// other remote servers on the network.
            ///</summary>
            TRUSTED_TO_AUTHENTICATE_FOR_DELEGATION = 0x01000000,

            /// <summary>
            /// 
            /// </summary>
            PARTIAL_SECRETS_ACCOUNT = 0x04000000,

            /// <summary>
            /// 
            /// </summary>
            USE_AES_KEYS = 0x08000000
        }
    }
}