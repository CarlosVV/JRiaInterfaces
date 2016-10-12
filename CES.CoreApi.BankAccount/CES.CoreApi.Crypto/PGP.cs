using System;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using CES.CoreApi.Crypto.Tools;
//using CES.CoreApi.Common.Tools;

namespace CES.CoreApi.Crypto
{
    public class PGP
    {
        //@@2012-10-19 lb SCR# 1605211
        //@@2013-09-03 lb SCR# 1858211 Fixed incorrect logic to validate digital signatures
        //@@2015-11-17 lb SCR# 2437511 Refactored

        //SPGP.BAS
        //
        //version 2.5.0.0, 28 June 2000
        //added spgpGetPreferences, spgpSetPreferences & related constants
        //added spgpKeyPropADK & related constants
        //added spgpUISelectKeysDialog, spgpPreferencesDialog
        //added spgpKeyPropRevocationKey
        //--------------------------------------------------------
        //version 2.4.1.0, 29 February 2000
        //changed spgpSubKeyGenerate parameters, changed alias names for
        //spgpEncode, spgpEncodeFile, spgpKeyImport, spgpKeyImportFile
        //--------------------------------------------------------
        //version 2.4.0.0, 24 February 2000
        //added functions spgpKeyGenerate, spgpSubKeyGenerate, spgpSdkApiVersion
        //--------------------------------------------------------
        //version 2.3.0.0, 31 December 99
        //dded functions spgpUIEncode & spgpUIEncodefile
        //added function spgpEstimatePassphraseQuality
        //added UI dialog functions, ordinal values
        //--------------------------------------------------------
        //version 2.2.3.3, 14 November 99
        //added function spgpKeyRemove
        //--------------------------------------------------------
        //version 2.2.3.0, 24 Septempber 99
        //added functions spgp_KeyImport, spgp_KeyImportFile spgpKeyPassChange,
        //spgpKeyEnable, spgpKeyDisable, spgp_Encode, spgp_EncodeFile
        //--------------------------------------------------------
        //version 2.2, 14 July 99
        //added functions spgpKeySign, spgpKeySigRemove, spgpKeyPropUserID,
        //spgpKeyPropSig, spgpKeyRingToFile
        //--------------------------------------------------------
        //version 2.1.1.0, 7 June 99
        //added functions spgpDetachedSigCreate & spgpDetachedSigVerify
        //added function spgpKeyRingCount
        //added default hash for signing, and related private const intant
        //--------------------------------------------------------
        //version 2.1.0.0, 1 June 99
        //added function spgpVersion
        //added functions spgpAnalyzeEx, spgpAnalyzeFileEx & related Constants
        //fixed keyimportfile, again
        //pursuant to the above, added //ByRef// to params where needed
        //introduced confusing new version numbering, just what we need
        //--------------------------------------------------------
        //version 2.0e, 7 Mar 99
        //added Preferences functions and constants
        //--------------------------------------------------------
        //version 2.0, 21 Feb 99
        //now supports PGP 6.x
        //added ciphering algorithms
        //fixed keyimportfile broken in 1.1
        //abbreviated keyprops in keyimport functions
        //changed some key/signature properties to private const intants
        //keyexport functions can use compatible-format mode
        //--------------------------------------------------------
        //version 1.1, 4 Feb 99
        //fixed armor/clearsigning problem
        //primary Key ID no longer required
        //added hashing algorithms
        //added spgpAnalyzeFile()
        //--------------------------------------------------------
        //
        //note that while some of these constants have the same name as
        //variables found in the pgp *.h files, they do not all have the same
        //values -- these are not meant to be translations of those files!

        /// <summary>
        /// The underlying library to use
        /// </summary>
        public enum PGPLibraryEnum
        {
            /// <summary>
            /// The original freeware library based in C Dll. For this library to work the PGP Freeware v. 6.5.3 program, from Network Associated must be installed in the target computer.
            /// Also, the sppgp.dll file must be copied to the Windows System directory
            /// </summary>
            Freeware  =0,

            /// <summary>
            /// Provided by the SecureBlackBox .NET library, from Eldos company, version 9.* and above. Minimum required files are SecureBlackbox.dll and SecureBlackbox.PGP.dll in
            /// the application's target folder. If it is intended to provided IDEA support, then SecureBlackbox.IDEA.dll file must be present and also TElBuiltInExtendedCryptoProvider.SetAsDefault() method,
            /// from SBCryptoProvBuiltInEx namespace must be called.
            /// </summary>
            SecureBlackBox = 1
        }

        //public key algorithms
        private const int PGPPublicKeyAlgorithm_Invalid = 0; //this (0) may not be correct, but how to test it? anyone got an invalid algorithm laying around?
        private const int PGPPublicKeyAlgorithm_RSA = 1;
        private const int PGPPublicKeyAlgorithm_RSAEncryptOnly = 2;
        private const int PGPPublicKeyAlgorithm_RSASignOnly = 3;
        private const int PGPPublicKeyAlgorithm_ElGamal = 4; //A.K.A. Diffie-Hellman
        private const int PGPPublicKeyAlgorithm_DSA = 5;

        //symmetric ciphers
        private const int PGPCipherAlgorithm_IDEA = 1;
        private const int PGPCipherAlgorithm_3DES = 2;
        private const int PGPCipherAlgorithm_CAST5 = 3;

        //hashing algorithms
        private const int PGPHashAlgorithm_Default = 0;
        private const int PGPHashAlgorithm_MD5 = 1;
        private const int PGPHashAlgorithm_SHA = 2;
        private const int PGPHashAlgorithm_RIPEMD160 = 3;
        private const int PGPHashAlgorithm_SHADouble = 4; // not available in PGP 6

        //trust levels
        private const int PGPKeyTrust_Undefined = 0;
        private const int PGPKeyTrust_Unknown = 1;
        private const int PGPKeyTrust_Never = 2;
        private const int PGPKeyTrust_Marginal = 5;
        private const int PGPKeyTrust_Complete = 6;
        private const int PGPKeyTrust_Ultimate = 7;

        //validity levels
        private const int PGPValidity_Unknown = 0;
        private const int PGPValidity_Invalid = 1;
        private const int PGPValidity_Marginal = 2;
        private const int PGPValidity_Complete = 3;

        //analysis results
        private const int PGPAnalyze_Encrypted = 0;            // Encrypted message
        private const int PGPAnalyze_Signed = 1;               // Signed message
        private const int PGPAnalyze_DetachedSignature = 2;    // Detached signature
        private const int PGPAnalyze_Key = 3;                  // Key data
        private const int PGPAnalyze_Unknown = 4;              // Non-pgp message
        // these are for spgpAnalyzeEx only
        private const int PGPAnalyze_EncryptedConventional = 5; // Like it says
        private const int PGPAnalyze_EncryptedNoKeys = 6;      // Key-encrypted to keys not on local ring

        //signature status
        private const int SIGNED_GOOD = 0;
        private const int SIGNED_NOT = 1;
        private const int SIGNED_BAD = 2;
        private const int SIGNED_NO_KEY = 3;

        //preferences, old style
        private const int PGPPref_PublicKeyring = 0;
        private const int PGPPref_PrivateKeyring = 1;
        private const int PGPPref_RandomSeedFile = 2;
        private const int PGPPref_DefaultKeyID = 3;

        //----------------------------------------------------------------------------------------------------
        // preferences, jeune ecole
        private const int spgpPref_PublicKeyring = 1;
        private const int spgpPref_PrivateKeyring = 2;
        private const int spgpPref_RandomSeedFile = 4;
        private const int spgpPref_DefaultKeyID = 8;
        private const int spgpPref_GroupsFile = 16;

        // for spgpPreferencesDialog, these select which page to open
        private const int spgpPrefsPage_GeneralPrefs = 0;
        private const int spgpPrefsPage_KeyringPrefs = 1;
        private const int spgpPrefsPage_EmailPrefs = 2;
        private const int spgpPrefsPage_HotkeyPrefs = 3;
        private const int spgpPrefsPage_KeyserverPrefs = 4;
        private const int spgpPrefsPage_CAPrefs = 5;
        private const int spgpPrefsPage_AdvancedPrefs = 6;

        // Key Properties Flags
        // string properties
        private const int spgpKeyProp_KeyID = 0x1;       // 1
        private const int spgpKeyProp_UserID = 0x2;       // 2
        private const int spgpKeyProp_Fingerprint = 0x4;       // 4
        private const int spgpKeyProp_CreationTimeStr = 0x8;       // 8
        private const int spgpKeyProp_ExpirationTimeStr = 0x10;     // 16
        // numeric properties
        private const int spgpKeyProp_Keybits = 0x80;      // 128
        private const int spgpKeyProp_KeyAlg = 0x100;     // 256
        private const int spgpKeyProp_Trust = 0x200;     // 512
        private const int spgpKeyProp_Validity = 0x400;     // 1024
        private const int spgpKeyProp_CreationTime = 0x800;     // 2048
        private const int spgpKeyProp_ExpirationTime = 0x1000;    // 4096
        // boolean properties
        private const int spgpKeyProp_IsSecret = 0x8000;    // & cetera
        private const int spgpKeyProp_IsAxiomatic = 0x10000;
        private const int spgpKeyProp_IsRevoked = 0x20000;
        private const int spgpKeyProp_IsDisabled = 0x40000;
        private const int spgpKeyProp_IsExpired = 0x80000;
        private const int spgpKeyProp_IsSecretShared = 0x100000;
        private const int spgpKeyProp_CanEncrypt = 0x200000;
        private const int spgpKeyProp_CanDecrypt = 0x400000;
        private const int spgpKeyProp_CanSign = 0x800000;
        private const int spgpKeyProp_CanVerify = 0x1000000;
        private const int spgpKeyProp_HasRevoker = 0x2000000;
        private const int spgpKeyProp_HasADK = 0x4000000;
        private const int spgpKeyProp_HasSubKey = 0x8000000;

        private class spgpPreferenceRec
        {
            public string PublicKeyring = "";
            public string PrivateKeyring = "";
            public string RandomSeedFile = "";
            public string GroupsFile = "";
            public string DefaultKeyID = "";
        }

        private class TSig_Data
        {
            public string Status = "";
            public string UserID = "";
            public string KeyID = "";
            public string DateTimeStr = "";
            public int DateTimeInt = 0;
            public bool Checked = false;
            public bool Verified = false;
            public string KeyValidity = "";
            public bool KeyRevoked = false;
            public bool KeyDisabled = false;
            public bool KeyExpired = false;
            public bool KeyIsOnRing = false;
        }

        //private static bool SIGNED = false; //not used

        //----------------------------------------------------------------------------------------------------
        // function names exported from the dll are case-sensitive.
        // encrypt/decrypt
        [DllImport("spgp.dll")]
        private static extern int spgp_encode(string BufferIn, string BufferOut, int BufferOutLen,
            int Encrypt, int Sign, int SignAlg, int Conventional, int ConventionalAlg, int Armor,
            int TextMode, int Clear, int Compress, int EyesOnly, int MIME, string CryptKeyID,
            string SignKeyID, string SignKeyPass, string ConventionalPass, string Comment,
            string MIMESeparator);
        [DllImport("spgp.dll")]
        private static extern int spgp_encodefile(string FileIn, string FileOut, int Encrypt, int Sign,
            int SignAlg, int Conventional, int ConventionalAlg, int Armor, int TextMode, int Clear,
            int Compress, int EyesOnly, int MIME, string CryptKeyID, string SignKeyID, string SignKeyPass,
            string ConventionalPass, string Comment, string MIMESeparator);

        // old versions
        // Declare Function spgpEncode Lib "spgp.dll" Alias "spgpencode" (ByVal BufferIn As String, ByVal BufferOut As String, ByVal BufferOutLen As Long, ByVal Encrypt As Long, ByVal Sign As Long, ByVal SignAlg As Long, ByVal Conventional As Long, ByVal ConventionalAlg As Long, ByVal Armor As Long, ByVal TextMode As Long, ByVal Clear As Long, ByVal Compress As Long, ByVal EyesOnly As Long, ByVal MIME As Long, ByVal CryptKeyID As String, ByVal SignKeyID As String, ByVal SignKeyPass As String, ByVal ConventionalPass As String, ByVal Comment As String, ByVal MIMESeparator As String) As Long
        // Declare Function spgpEncodeFile Lib "spgp.dll" Alias "spgpencodefile" (ByVal FileIn As String, ByVal FileOut As String, ByVal Encrypt As Long, ByVal Sign As Long, ByVal SignAlg As Long, ByVal Conventional As Long, ByVal ConventionalAlg As Long, ByVal Armor As Long, ByVal TextMode As Long, ByVal Clear As Long, ByVal Compress As Long, ByVal EyesOnly As Long, ByVal MIME As Long, ByVal CryptKeyID As String, ByVal SignKeyID As String, ByVal SignKeyPass As String, ByVal ConventionalPass As String, ByVal Comment As String, ByVal MIMESeparator As String) As Long
        [DllImport("spgp.dll")]
        private static extern int spgpdecode(string BufferIn, string BufferOut, int BufferOutLen, string Pass, string SigProps);
        [DllImport("spgp.dll")]
        private static extern int spgpdecodefile(string FileIn, string FileOut, string Pass, string SigProps);

        // key import/export
        [DllImport("spgp.dll")]
        private static extern int spgpkeyexport(string KeyID, string BufferOut, int BufferOutLen, int ExportPrivate, int ExportCompatible);
        [DllImport("spgp.dll")]
        private static extern int spgpkeyexportfile(string KeyID, string FileOut, int ExportPrivate, int ExportCompatible);
        [DllImport("spgp.dll")]
        private static extern int spgp_keyimport(string BufferIn, string KeyProps, int KeyPropsLen, int Import, int AllProps);
        [DllImport("spgp.dll")]
        private static extern int spgp_keyimportfile(string FileIn, string KeyProps, int KeyPropsLen, int Import, int AllProps);

        // old versions
        // Declare Function spgpKeyImport Lib "spgp.dll" Alias "spgpkeyimport" (ByVal BufferIn As String, ByVal KeyProps As String, ByVal KeyPropsLen As Long) As Long
        // Declare Function spgpKeyImportFile Lib "spgp.dll" Alias "spgpkeyimportfile" (ByVal FileIn As String, ByVal KeyProps As String, ByVal KeyPropsLen As Long) As Long

        // key properties
        [DllImport("spgp.dll")]
        private static extern int spgpkeyprops(string KeyID, string KeyProps, int KeyPropsLen);
        [DllImport("spgp.dll")]
        private static extern int spgpkeyringid(string BufferOut, int BufferOutLen);
        [DllImport("spgp.dll")]
        private static extern int spgpkeyringcount();
        [DllImport("spgp.dll")]
        private static extern int spgpkeyringtofile(string FileOut);
        [DllImport("spgp.dll")]
        private static extern int spgpkeypropuserid(string KeyID, string BufferOut, int BufferOutLen);
        [DllImport("spgp.dll")]
        private static extern int spgpkeypropsig(string UserID, string BufferOut, int BufferOutLen);
        [DllImport("spgp.dll")]
        private static extern int spgpkeypropadk(string KeyHexID, string ADKeyProps, int ADKeyPropsLen, int ADKeyCount, int flags);
        [DllImport("spgp.dll")]
        private static extern int spgpkeyproprevocationkey(string KeyHexID, string RevKeyProps, int RevKeyPropsLen, int RevKeyCount, int flags);

        // error strings
        [DllImport("spgp.dll")]
        private static extern int spgpgeterrorstring(int theError, string BufferOut);

        // analyze
        [DllImport("spgp.dll")]
        private static extern int spgpanalyze(string BufferIn);
        [DllImport("spgp.dll")]
        private static extern int spgpanalyzefile(string FileIn);
        [DllImport("spgp.dll")]
        private static extern int spgpanalyzeex(string BufferIn, string BufferOut, int BufferOutLen);
        [DllImport("spgp.dll")]
        private static extern int spgpanalyzefileex(string FileIn, string BufferOut, int BufferOutLen);

        // prefs
        [DllImport("spgp.dll")]
        private static extern int spgpsetpreference(int Preference, string BufferIn);
        [DllImport("spgp.dll")]
        private static extern int spgpgetpreference(int Preference, string BufferOut);
        [DllImport("spgp.dll")]
        private static extern int spgpsetpreferences(spgpPreferenceRec Prefs, int flags);
        [DllImport("spgp.dll")]
        private static extern int spgpgetpreferences(spgpPreferenceRec Prefs, int flags);
        [DllImport("spgp.dll")]
        private static extern int spgppreferencesdialog(int ShowPage, int WindowHandle);

        // misc
        [DllImport("spgp.dll")]
        private static extern int spgpkeyisonring(string KeyID);
        [DllImport("spgp.dll")]
        private static extern int spgpversion();
        [DllImport("spgp.dll")]
        private static extern int spgpsdkapiversion();
        [DllImport("spgp.dll")]
        private static extern int spgpestimatepassphrasequality(string PassPhrase);
        //Declare Function spgpPGPPath Lib "spgp.dll" Alias "spgppgppath" (ByVal Path As String) As Long

        // key manipulation
        [DllImport("spgp.dll")]
        private static extern int spgpkeypasschange(string KeyID, string OldPhrase, string NewPhrase);
        [DllImport("spgp.dll")]
        private static extern int spgpkeyenable(string KeyID);
        [DllImport("spgp.dll")]
        private static extern int spgpkeydisable(string KeyID);
        [DllImport("spgp.dll")]
        private static extern int spgpkeyremove(string KeyID);
        [DllImport("spgp.dll")]
        private static extern int spgpkeysigremove(string KeyHexID, string UserID, string SignHexID);
        [DllImport("spgp.dll")]
        private static extern int spgpkeysign(string KeyHexID, string UserID, string SignKeyID, string SignKeyPass,
            int Expires, int Exportable, int Trust, int Validity);
        [DllImport("spgp.dll")]
        private static extern int spgpkeygenerate(string UserID, string PassPhrase, string NewKeyHexID, int KeyAlg,
            int CipherAlg, int Size, int Expires, int FastGeneration, int FailWithoutEntropy, int WinHandle);
        [DllImport("spgp.dll")]
        private static extern int spgpsubkeygenerate(string MasterKeyHexID, string MasterKeyPass, string NewSubKeyHexID,
            int KeyAlg, int Size, int ExpiresIn, int FastGeneration, int FailWithoutEntropy, int WinHandle);

        // signatures
        [DllImport("spgp.dll")]
        private static extern int spgpdetachedsigcreate(string FileIn, string SigFile, string SignKeyID,
            string SignKeyPass, string Comment, int SignAlg, int Armor);
        [DllImport("spgp.dll")]
        private static extern int spgpdetachedsigverify(string SigFile, string SignedFile, string SigProps);

        // User Interface
        [DllImport("spgp.dll")]
        private static extern int spgpuiencode(string BufferIn, string BufferOut, int BufferOutLen, int Encrypt,
            int Sign, int SignAlg, int Conventional, int ConventionalAlg, int Clear, int Compress, int EyesOnly,
            int MIME, string Comment, string MIMESeparator, int WindowHandle);
        [DllImport("spgp.dll")]
        private static extern int spgpuiencodefile(string FileIn, string FileOut, int Encrypt, int Sign,
            int SignAlg, int ConventionalEncrypt, int ConventionalAlg, int Armor, int TextMode, int Clear,
            int Compress, int EyesOnly, int MIME, string Comment, string MIMESeparator, int WindowHandle);

        [DllImport("spgp.dll")]
        private static extern int spgpuirecipientsdialog(string RecipientHexID, int RecipientHexIDLen,
            string Caption, string Reserved1, int DisplayMarginalValidity, int Reserved2, int Reserved3, int Reserved4, int Reserved5, int WindowHandle);
        [DllImport("spgp.dll")]
        private static extern int spgpuisigningpassphrasedialog(string SelectedKeyHexID, string SelectedKeyPass,
            string DefaultKeyID, int FindMatchingKey, int WindowHandle);
        [DllImport("spgp.dll")]
        private static extern int spgpuiconfirmationpassphrasedialog(string PassPhrase, int ShowPassphraseQuality,
            int MinimumPassphraseQuality, int MinimumPassphraseLength, int WindowHandle);
        [DllImport("spgp.dll")]
        private static extern int spgpuikeypassphrasedialog(string KeyID, string PassPhrase, int WindowHandle);
        [DllImport("spgp.dll")]
        private static extern int spgpuiselectkeysdialog(string KeyID, string KeyProps, string Prompt,
            int KeyPropsLen, int ShowKeyRing, int flags, int WinHandle);

        // Clipboard functions (use PGPsc55, PGPsc60, or PGPsc.dll)
        //Declare Function spgpEncodeClipboard Lib "spgp.dll" Alias "spgpencodeclipboard" (ByVal Flags As Long, ByVal Parent As Long) As Long
        //Declare Function spgpDecodeclipboard Lib "spgp.dll" Alias "spgpdecodeclipboard" (ByVal Flags As Long, ByVal Parent As Long) As Long
        //Declare Function spgpKeyImportClipboard Lib "spgp.dll" Alias "spgpkeyimportclipboard" (ByVal Flags As Long, ByVal Parent As Long) As Long

        //----------------------------------------------------------------------
        // Function ordinal values, for those who would rather import by ordinal
        //----------------------------------------------------------------------

        //  Function Name      Ordinal

        //  spgpencode             1
        //  spgpencodefile         2
        //  spgp_encode            3
        //  spgp_encodefile        4

        //  spgpdecode             5
        //  spgpdecodefile         6

        //  spgpkeyexport          7
        //  spgpkeyexportfile      8
        //  spgpkeyimport          9
        //  spgp_keyimport         10
        //  spgpkeyimportfile      11
        //  spgp_keyimportfile     12

        //  spgpkeyprops           13
        //  spgpkeyringid          14
        //  spgpkeyringtofile      15

        //  spgpgeterrorstring     16

        //  spgpanalyze            17
        //  spgpanalyzeex          18
        //  spgpanalyzefile        19
        //  spgpanalyzefileex      20

        //  spgpkeyisonring        21
        //  spgpsetpreference      22
        //  spgpgetpreference      23
        //  spgpversion            24
        //  spgpdetachedsigcreate  25
        //  spgpdetachedsigverify  26
        //  spgpkeyringcount       27
        //  spgpkeyenable          28
        //  spgpkeydisable         29
        //  spgpkeypasschange      30
        //  spgpkeysign            31
        //  spgpkeypropuserid      32
        //  spgpkeypropsig         33
        //  spgpkeyremove          34
        //  spgpkeysigremove       35

        //  spgpuiencode           36
        //  spgpuiencodefile       37
        //  spgpuirecipientsdialog 38
        //  spgpuisigningpassphrasedialog      39
        //  spgpuiconfirmationpassphrasedialog 40
        //  spgpuikeypassphrasedialog          41
        //  spgpestimatepassphrasequality      42

        //  spgpkeygenerate        43
        //  spgpsubkeygenerate     44

        //  spgpsdkapiversion      45

        //  spgpsetpreferences     46
        //  spgpgetpreferences     47
        //  spgppreferencesdialog  48

        //  spgpuiselectkeysdialog 49
        //  spgpkeypropadk         50
        //  spgpkeyproprevocationkey 51

        private static PGPLibraryEnum _defaultImplementation = PGPLibraryEnum.SecureBlackBox;

        private static bool _staticDataInitialized = false;
        private static string _defaultConnectionString = "";

        /// <summary>
        /// Establishes the implementation for new PGP objecst
        /// </summary>
        public static PGPLibraryEnum DefaultImplementation
        {
            get { return _defaultImplementation; }
            set { _defaultImplementation = value; }
        }

        /// <summary>
        /// Initializes the SecureBlackBox library
        /// </summary>
        /// <param name="licenses"></param>
        /// <param name="connectionString"></param>
        /// <param name="IDEAsupport"></param>
        public static void Initialize(string[] licenses, string defaultConnectionString)
        {
            bool IDEAsupport = true;

            if (_staticDataInitialized) throw new Exception("Library already initialized!");

            _defaultConnectionString = defaultConnectionString;

            if (licenses == null || licenses.Length == 0)
                return;

            var lics = from string license in licenses where !string.IsNullOrWhiteSpace(license) select license;

            if (lics.Count() == 0)
                return;

            foreach(string lic in lics)
                SBUtils.Unit.SetLicenseKey(lic);            

            if(IDEAsupport)
                SBCryptoProvBuiltInEx.TElBuiltInExtendedCryptoProvider.SetAsDefault();

            _staticDataInitialized = true;
        }

        /// <summary>
        /// Ensure the SecureBlackBox library. Throws an exception otherwise
        /// </summary>
        private bool secureBlackBox_checkIni(bool throwExceptionIfNot)
        {
            if (!_staticDataInitialized)
            {
                if (throwExceptionIfNot)
                    throw new CryptoException("SecureBlackBox has not been initialized. Call Initialize() static method first.");

                return false;
            }

            return true;
        }

        private int secureBlackBox_encodeFile(string fileIn, string fileOut, bool encrypt, bool sign, string comment)
        {
            try
            {
                    //Try to get the key from database
                string strCnn = _defaultConnectionString;

                if(!string.IsNullOrWhiteSpace(_connectionString))
                    strCnn = _connectionString;

                SBPGP.TElPGPWriter wr = new SBPGP.TElPGPWriter();
                wr.Armor = _armor;
                wr.UseOldPackets = true;

                switch (_hashingAlgorithm)
                {
                    case HashingAlgorithmsEnum.HashAlgorithm_Default:
                        wr.HashAlgorithm = 0;//Auto. It will be MD5 since UseOldPackets is true
                        break;
                    case HashingAlgorithmsEnum.HashAlgorithm_MD5:
                        wr.HashAlgorithm = 1;
                        break;
                    case HashingAlgorithmsEnum.HashAlgorithm_RIPEMD160:
                        wr.HashAlgorithm = 3;
                        break;
                    case HashingAlgorithmsEnum.HashAlgorithm_SHA:
                        wr.HashAlgorithm = 8;
                        break;
                    default:
                        throw new CryptoException("Hash algorithm not suppported.");
                }

                switch (_symmetricCipherAlgorithm)
                {
                    case SymmetricCipherAlgorithmsEnum.CipherAlgorithm_IDEA:
                        wr.SymmetricKeyAlgorithm = 1; //IDEA is 1. If wanted, then
                /*
                 * 
    add a reference to SecureBlackbox.IDEA.dll assembly
    add "SBCryptoProvBuiltInEx" to the list of the used namespaces
    call TElBuiltInExtendedCryptoProvider.SetAsDefault() method 
                 * */
                        break;
                    case SymmetricCipherAlgorithmsEnum.CipherAlgorithm_3DES:
                        wr.SymmetricKeyAlgorithm = 2;
                        break;
                    case SymmetricCipherAlgorithmsEnum.CipherAlgorithm_CAST5:
                        wr.SymmetricKeyAlgorithm = 3;
                        break;
                    default:
                        throw new CryptoException("Ciphering algorithm not suppported.");
                }


                wr.Compress = _compress;
                wr.TextCompatibilityMode = _textMode;
                wr.EncryptionType = SBPGP.TSBPGPEncryptionType.etPublicKey;
                wr.UseNewFeatures = false;
                wr.Filename = (new FileInfo(fileIn)).Name;

                const int bufferLength = 102400; //100Kb
                byte[] bytesBuffer = new byte[bufferLength];

                if (encrypt)
                {
                    SBPGPKeys.TElPGPPublicKey pk = new SBPGPKeys.TElPGPPublicKey();

                    using (System.Data.SqlClient.SqlConnection cnn = new SqlConnection(strCnn))
                    {
                        using (SqlCommand cmd = new System.Data.SqlClient.SqlCommand("sys_sp_PGPKeyDef_GetContent", cnn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@sKeyID", SqlDbType.VarChar, 20).Value = _cryptoKeyID;

                            cnn.Open();
                            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                            {
                                long totalBytesRead = 0;
                                int bytesRead = 0;

                                if (dr.Read())
                                {
                                    using (MemoryStream ms = new MemoryStream())
                                    {
                                        do
                                        {
                                            bytesRead = (int)dr.GetBytes(0, totalBytesRead, bytesBuffer, 0, bufferLength);
                                            totalBytesRead += bytesRead;

                                            if (bytesRead != 0) ms.Write(bytesBuffer, 0, bytesRead);

                                        } while (bytesRead != 0);

                                        ms.Flush();
                                        ms.Seek(0, SeekOrigin.Begin);

                                        pk.LoadFromStream(ms);
                                    }
                                }
                                dr.Close();
                            }
                            cnn.Close();
                        }
                    }

                    wr.EncryptingKeys = new SBPGPKeys.TElPGPKeyring();
                    wr.EncryptingKeys.AddPublicKey(pk);
                }

                if (sign)
                {
                    SBPGPKeys.TElPGPSecretKey sk = new SBPGPKeys.TElPGPSecretKey();

                    using (System.Data.SqlClient.SqlConnection cnn = new SqlConnection(strCnn))
                    {
                        using (SqlCommand cmd = new System.Data.SqlClient.SqlCommand("sys_sp_PGPKeyDef_GetContent", cnn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@sKeyID", SqlDbType.VarChar, 20).Value = _signKeyID;

                            cnn.Open();
                            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                            {
                                long totalBytesRead = 0;
                                int bytesRead = 0;

                                if (dr.Read())
                                {
                                    using (MemoryStream ms = new MemoryStream())
                                    {
                                        do
                                        {
                                            bytesRead = (int)dr.GetBytes(0, totalBytesRead, bytesBuffer, 0, bufferLength);
                                            totalBytesRead += bytesRead;

                                            if (bytesRead != 0) ms.Write(bytesBuffer, 0, bytesRead);

                                        } while (bytesRead != 0);

                                        ms.Flush();
                                        ms.Seek(0, SeekOrigin.Begin);

                                        sk.LoadFromStream(ms);
                                    }
                                }
                                dr.Close();
                            }
                            cnn.Close();
                        }
                    }

                    //@@2013-04-09 lb SCR# 1740111 Changed assignment of passphrase
                    secureBlackBox_ApplyPassphraseToSK(sk, _signPassword);

                    wr.SigningKeys = new SBPGPKeys.TElPGPKeyring();
                    wr.SigningKeys.AddSecretKey(sk);                    
                }

                if (_armor)
                {
                    wr.ArmorBoundary = "PGP MESSAGE"; //@@2015-11-12 lb SCR#2437511-Changed default boundasry text for Eldos' implementation
                    wr.ArmorHeaders.Add("Version: N/A");
                    
                    if (!string.IsNullOrWhiteSpace(comment))                    
                        wr.ArmorHeaders.Add(string.Format("Comment: {0}", comment));
                }

                if (sign && !encrypt)
                {
                    if (_clear)
                        wr.ClearTextSignFile(fileIn, fileOut);
                    else
                        wr.SignFile(fileIn, fileOut, false);
                }
                else if (!sign && encrypt)
                    wr.EncryptFile(fileIn, fileOut);
                else if (sign && encrypt)
                    wr.EncryptAndSignFile(fileIn, fileOut);

                return 0;
            }
            catch (CryptoException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CryptoException("Unexpected Error on SecureBlackBox: " + ex.Message);
            }
        }

        /// <summary>
        /// Applies the indicated passphrase to the key and all of its subkeys, to compensate a bug in the library
        /// when calling the Passphrase property of a secret key which results it is not enough.
        /// This we avoid ot use the OnKeyPassphrase event of the TElPGPWriter object because there is the posiblity of falling in an infinite loop
        /// if the correct password is not provided and Cancel argument is not set to true.
        /// 
        /// More information on this bug: http://www.eldos.com/forum/read.php?FID=7&TID=159
        /// </summary>
        /// <param name="sk"></param>
        /// <param name="passphrase"></param>
        private void secureBlackBox_ApplyPassphraseToSK(SBPGPKeys.TElPGPSecretKey sk, string passphrase)
        {
            //@@2013-04-09 lb SCR# 1740111 Created
            sk.Passphrase = passphrase;

            for (int x = 0; x < sk.SubkeyCount; x++)
                sk.get_Subkeys(x).Passphrase = passphrase;
        }

        private int secureBlackBox_decodeFile(string fileIn, string fileOut, string keyPwd)
        {
            try
            {
                //Try to get the key from database
                string strCnn = _defaultConnectionString;

                if (!string.IsNullOrWhiteSpace(_connectionString))
                    strCnn = _connectionString;

                SBPGP.TElPGPReader rd = new SBPGP.TElPGPReader();

                rd.Passphrase = (keyPwd ?? "");
                rd.KeyPassphrase = _cryptoKeyPassphrase;

                rd.OnEncrypted += new SBPGP.TSBPGPEncryptedEvent(secureBlackBox_OnEncrypted);
                rd.OnSigned += new SBPGP.TSBPGPSignedEvent(secureBlackBox_OnSigned);
                rd.OnSignatures += new SBPGPStreams.TSBPGPSignaturesEvent(secureBlackBox_OnSignatures);

                rd.OutputFile = fileOut;
                rd.DecryptAndVerifyFile(fileIn);

                return 0;
            }
            catch (CryptoException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CryptoException("Unexpected Error on SecureBlackBox: " + ex.Message);
            }
        }

        void secureBlackBox_OnSignatures(object Sender, SBPGPKeys.TElPGPSignature[] Signatures, SBPGPStreams.TSBPGPSignatureValidity[] Validities)
        {
            //@@2012-08-14 lb The policy here is that we will accept as valid if at least one signature is valid.

            if (string.IsNullOrEmpty(_signKeyID)) return;
            if(Validities == null || Validities.Length == 0) return;//There is nothing to do here

            if (!(from SBPGPStreams.TSBPGPSignatureValidity validity in Validities where validity == SBPGPStreams.TSBPGPSignatureValidity.svValid select validity).Any())
                throw new CryptoException("Not one of attached signatures is valid");
        }

        void secureBlackBox_OnSigned(object Sender, byte[][] KeyIDs, SBPGPUtils.TSBPGPSignatureType SignatureType)
        {
            if (KeyIDs == null || KeyIDs.Length == 0) return;
            if (string.IsNullOrEmpty(_signKeyID)) return;

            SBPGP.TElPGPReader rd = (SBPGP.TElPGPReader)Sender;

            if (rd.VerifyingKeys != null && rd.VerifyingKeys.PublicCount != 0) return;

            //Try to get the key from database
            string strCnn = _defaultConnectionString;

            if (!string.IsNullOrWhiteSpace(_connectionString))
                strCnn = _connectionString;

            const int bufferLength = 102400; //100Kb
            byte[] bytesBuffer = new byte[bufferLength];

            SBPGPKeys.TElPGPPublicKey pk = null;

            using (System.Data.SqlClient.SqlConnection cnn = new SqlConnection(strCnn))
            {
                using (SqlCommand cmd = new System.Data.SqlClient.SqlCommand("sys_sp_PGPKeyDef_GetContent", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@sKeyID", SqlDbType.VarChar, 20).Value = _signKeyID;

                    cnn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                    {
                        long totalBytesRead = 0;
                        int bytesRead = 0;

                        if (dr.Read())
                        {
                            totalBytesRead = 0;
                            pk = new SBPGPKeys.TElPGPPublicKey();

                            using (MemoryStream ms = new MemoryStream())
                            {
                                do
                                {
                                    bytesRead = (int)dr.GetBytes(0, totalBytesRead, bytesBuffer, 0, bufferLength);
                                    totalBytesRead += bytesRead;

                                    if (bytesRead != 0) ms.Write(bytesBuffer, 0, bytesRead);

                                } while (bytesRead != 0);

                                ms.Flush();
                                ms.Seek(0, SeekOrigin.Begin);

                                try { pk.LoadFromStream(ms); }
                                catch (Exception ex)
                                {
                                    throw new Exception("Unable to load key from database: " + ex.Message);
                                }
                            }

                            //Get the key ID
                            rd.VerifyingKeys = new SBPGPKeys.TElPGPKeyring();
                            rd.VerifyingKeys.AddPublicKey(pk);
                        }
                        dr.Close();
                    }
                    cnn.Close();
                }
            }            
        }

        private void secureBlackBox_OnEncrypted(object Sender, byte[][] KeyIDs, bool IntegrityProtected, bool PassphraseUsed)
        {
            if (KeyIDs == null || KeyIDs.Length == 0 ) return;

            SBPGP.TElPGPReader rd = (SBPGP.TElPGPReader)Sender;

            if (rd.DecryptingKeys != null && rd.DecryptingKeys.SecretCount != 0) return;

            //Try to get the key from database
            string strCnn = _defaultConnectionString;

            if (!string.IsNullOrWhiteSpace(_connectionString))
                strCnn = _connectionString;

            const int bufferLength = 102400; //100Kb
            byte[] bytesBuffer = new byte[bufferLength];

            SBPGPKeys.TElPGPSecretKey sk = null;

            using (System.Data.SqlClient.SqlConnection cnn = new SqlConnection(strCnn))
            {
                using (SqlCommand cmd = new System.Data.SqlClient.SqlCommand("sys_sp_PGPKeyDef_GetContent", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@sKeyID", SqlDbType.VarChar, 20).Value = _cryptoKeyID;

                    cnn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                    {
                        long totalBytesRead = 0;
                        int bytesRead = 0;

                        if (dr.Read())
                        {
                            sk = new SBPGPKeys.TElPGPSecretKey();
                            totalBytesRead = 0;

                            using (MemoryStream ms = new MemoryStream())
                            {
                                do
                                {
                                    bytesRead = (int)dr.GetBytes(0, totalBytesRead, bytesBuffer, 0, bufferLength);
                                    totalBytesRead += bytesRead;

                                    if (bytesRead != 0) ms.Write(bytesBuffer, 0, bytesRead);

                                } while (bytesRead != 0);

                                ms.Flush();
                                ms.Seek(0, SeekOrigin.Begin);

                                try { sk.LoadFromStream(ms); }
                                catch (Exception ex)
                                {
                                    throw new Exception("Unable to load key from database: " + ex.Message);
                                }

                            }

                            //Get the key ID
                            rd.DecryptingKeys = new SBPGPKeys.TElPGPKeyring();

                            rd.DecryptingKeys.AddSecretKey(sk);
                        }
                        dr.Close();
                    }
                    cnn.Close();
                }
            }
        }

        private static TSig_Data ParseSigData(string SigProps)
        {
            // (takes as an argument the tab-delimited string produced by
            // decode/decodefile functions & parses it to populate a TSig_Data structure)

            int pos1 = 0, pos2 = 0, sublen = 0;
            TSig_Data Sig = new TSig_Data();

            if (SigProps == null) SigProps = "";

            if (SigProps.Trim() == "\0")
            {
                //SIGNED = false;
                return Sig;
            }

            // Status - the apparent status of the signature:
            // SIGNED_NOT      -unsigned
            // SIGNED_GOOD     -signing key found, data intact
            // SIGNED_BAD      -data not intact
            // SIGNED_NO_KEY   -signing key not found, data unverified
            //  Global Const SIGNED_GOOD = 0
            //  Global Const SIGNED_NOT = 1
            //  Global Const SIGNED_BAD = 2
            //  Global Const SIGNED_NO_KEY = 3

            pos1 = 1;

            pos2 = SigProps.IndexOf((char)9) + 1;
            sublen = pos2 - 1;

            switch (StringTools.vbLeft(SigProps, 1))
            {
                case "0":
                    Sig.Status = "SIGNED_GOOD"; break;
                case "1":
                    Sig.Status = "SIGNED_NOT"; break;
                case "2":
                    Sig.Status = "SIGNED_BAD"; break;
                case "3":
                    Sig.Status = "SIGNED_NO_KEY"; break;
                default:
                    Sig.Status = "SIGNED_NOT"; break;
            }

            // if there appears to be a signature
            if (Sig.Status != "" && Sig.Status != "SIGNED_NOT")
            {
                //SIGNED = true;

                // UserID - primary user id of signing key
                pos1 = pos2 + 1;
                pos2 = SigProps.IndexOf((char)9, pos2);
                sublen = pos2 - pos1;
                Sig.UserID = StringTools.vbMid(SigProps, pos1, sublen).Trim();

                // KeyID - key id of signing key
                pos1 = pos2 + 1;
                pos2 = SigProps.IndexOf((char)9, pos2);
                sublen = pos2 - pos1;
                Sig.KeyID = StringTools.vbMid(SigProps, pos1, sublen).Trim();

                // DateTimeStr - date & time as a ctime-format string (local time)
                pos1 = pos2 + 1;
                pos2 = SigProps.IndexOf((char)9, pos2);
                sublen = pos2 - pos1;
                Sig.DateTimeStr = StringTools.vbMid(SigProps, pos1, sublen).Trim();

                // DateTimeInt - date & time as a ctime-style number (GMT/UTC)
                pos1 = pos2 + 1;
                pos2 = SigProps.IndexOf((char)9, pos2);
                sublen = pos2 - pos1;
                Sig.DateTimeInt = int.Parse(StringTools.vbMid(SigProps, pos1, sublen).Trim());

                //Checked - is signing key available/is message properly formatted?
                pos1 = pos2 + 1;
                pos2 = SigProps.IndexOf((char)9, pos2);
                sublen = pos2 - pos1;
                Sig.Checked = bool.Parse(StringTools.vbMid(SigProps, pos1, sublen).Trim());

                // Verified - is Checked true and is the data intact?
                // ( this is the one to check: if this is true, the sig. is good )
                pos1 = pos2 + 1;
                pos2 = SigProps.IndexOf((char)9, pos2);
                sublen = pos2 - pos1;
                Sig.Verified = bool.Parse(StringTools.vbMid(SigProps, pos1, sublen).Trim());

                // KeyValidity - validity level of signing key:
                pos1 = pos2 + 1;
                pos2 = SigProps.IndexOf((char)9, pos2);
                sublen = pos2 - pos1;
                switch (StringTools.vbMid(SigProps, pos1, sublen).Trim())
                {
                    case "0":
                        Sig.KeyValidity = "Unknown"; break;
                    case "1":
                        Sig.KeyValidity = "Invalid"; break;
                    case "2":
                        Sig.KeyValidity = "Marginal"; break;
                    case "3":
                        Sig.KeyValidity = "Complete"; break;
                    default:
                        Sig.KeyValidity = "Uknown"; break;
                }

                // misc. key problems

                // KeyRevoked
                pos1 = pos2 + 1;
                pos2 = SigProps.IndexOf((char)9, pos2);
                sublen = pos2 - pos1;
                Sig.KeyRevoked = bool.Parse(StringTools.vbMid(SigProps, pos1, sublen).Trim());

                // KeyDisabled
                pos1 = pos2 + 1;
                pos2 = SigProps.IndexOf((char)9, pos2);
                sublen = pos2 - pos1;
                Sig.KeyDisabled = bool.Parse(StringTools.vbMid(SigProps, pos1, sublen).Trim());

                // KeyExpired
                pos1 = pos2 + 1;
                pos2 = SigProps.IndexOf((char)9, pos2);
                sublen = pos2 - pos1;
                Sig.KeyExpired = bool.Parse(StringTools.vbMid(SigProps, pos1, sublen).Trim());
            }

            return Sig;
        }

        //This function encrypts one file
        //public static bool PGPKeyEncryptFile(string file_In, string file_Out, string keyID,
        //    string keyPwd, string comment, int textMode, int compress)
        //{
        //    PGP pgp = new Common.Encrypting.PGP();
        //    pgp.Clear = true;
        //    pgp.Armor = true;
        //    pgp.CryptoKeyID = keyPwd;
        //    pgp.TextMode =  (textMode != 0);
        //    pgp.Compress = compress != 0;
        //    pgp.SymmetricCipherAlgorithm = SymmetricCipherAlgorithmsEnum.CipherAlgorithm_IDEA;
        //    pgp.HashingAlgorithm = HashingAlgorithmsEnum.HashAlgorithm_MD5;

        //    return pgp.EncryptOneFile(file_In, file_Out, comment);
        //}

        #region PGPKeyEncryptFile overloads

        //public static bool PGPKeyEncryptFile(string file_In, string file_Out, string keyID,
        //    string keyPwd, string comment, int textMode)
        //{
        //    return PGPKeyEncryptFile(file_In, file_Out, keyID,
        //    keyPwd, comment, textMode, 0);
        //}

        //public static bool PGPKeyEncryptFile(string file_In, string file_Out, string keyID,
        //    string keyPwd, string comment)
        //{
        //    return PGPKeyEncryptFile(file_In, file_Out, keyID,
        //    keyPwd, comment, 0);
        //}

        #endregion

        ////This function descrypte one file
        //public static bool PGPKeyDecryptFile(string file_In, string file_Out, string keyPwd)
        //{
        //    string errInfo = string.Format("File In={0}, File Out={1}",
        //    file_In, file_Out);

        //    try
        //    {
        //        int i = 0;
        //        //Delete the output file if it exists
        //        if (File.Exists(file_Out)) File.Delete(file_Out);

        //        i = spgpdecodefile(file_In, file_Out, keyPwd, "");
        //        //check signature properties
        //        if (!File.Exists(file_Out) || i != 0)
        //        {
        //            Logger.Error("File could no be decrypted. Info: " + errInfo);
        //            return false;
        //        }
        //        else
        //            return true;

        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error("File could no be decrypted. Info: " + errInfo + ". Details: " + ex.Message);
        //        return false;
        //    }
        //}

        //This function decrypts one file
        public bool PGPKeyDecryptAndVerifyFile(string file_In, string file_Out, string keyPwd)
        {
            string error;
            string errInfo = string.Format("File In={0}, File Out={1}",
            file_In, file_Out);

            try
            {
                int i = 0;
                //Delete the output file if it exists
                if (File.Exists(file_Out)) File.Delete(file_Out);

                switch (_libraryUsed)
                {
                    case PGPLibraryEnum.SecureBlackBox:
                        i = secureBlackBox_decodeFile(file_In, file_Out, keyPwd);
                        break;
                    case PGPLibraryEnum.Freeware:
                        i = spgpdecodefile(file_In, file_Out, keyPwd, "");
                        break;
                }
                
                //check signature properties
                if (!File.Exists(file_Out) || i != 0)
                {
                    error = "File could no be decrypted. Info: " + errInfo;

                    if (_throwExOnError)
                        throw new CryptoException(error);

                    if(_logger != null)
                        _logger.LogError(error);

                    return false;
                }
                else
                    return true;

            }
            catch (CryptoException ex)
            {
                if (_throwExOnError)
                    throw ex;

                if (_logger != null)
                    _logger.LogError(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                error = "File could no be decrypted. Info: " + errInfo + ". Details: " + ex.Message;

                if (_throwExOnError)
                    throw new CryptoException(error);

                if (_logger != null)
                    _logger.LogError(error);
                return false;
            }
        }

        // This routine can handle one or files. If more than one file, each file must be separated by "|".
        // This routine will encrypt and rename each file, according to parameter strPostPendToFileName. The
        // parameter strFiles will be replaced with the new file names, but only if all files are successfully processed.
        // This routine can optionally delete each source file. Note: The files are only deleted if all files are
        // successfully processed. Note - The deleted files will not show up in the Recycle Bin.
        //public static bool PGPKeyEncrypt(ref string encrFiles, string appendToNewFile,
        //    bool deleteSourceFiles, string keyID, string keyPwd, string comment, int textMode,
        //    int compress)
        //{
        //    try
        //    {
        //        StringBuilder sbNewFile = new StringBuilder();
        //        StringBuilder sbNewFiles = new StringBuilder();

        //        string[] files = encrFiles.Split(new char[] { '|' });

        //        foreach (string file in files)
        //        {
        //            if (file.Length > 0)
        //            {
        //                sbNewFiles.Append('|');

        //                //Get the new file name
        //                sbNewFile.Remove(0, sbNewFile.Length);
        //                sbNewFile.Append(file); sbNewFile.Append(appendToNewFile);

        //                //try to encrypt
        //                if (!PGPKeyEncryptFile(file, sbNewFile.ToString(), keyID,
        //                    keyPwd, comment))
        //                    return false;

        //                sbNewFiles.Append(sbNewFile);
        //            }
        //        }
        //        sbNewFiles.Remove(0, 1); //Remove first | character
        //        //Delete files?
        //        if (deleteSourceFiles)
        //        {
        //            foreach (string file in files)
        //                if (file.Length > 0) File.Delete(file);
        //        }

        //        encrFiles = sbNewFiles.ToString(); //Function behave successfull
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error("System was unable to encrypt files. Files: " + encrFiles + ", Append: " + appendToNewFile + ", KeyID = "
        //            + keyID + ", Pwd: " + keyPwd + ". Details: " + ex.Message);
        //        return false;
        //    }
        //}

        #region PGPKeyEncrypt overloads

        //public static bool PGPKeyEncrypt(ref string strFiles, string strPostPendToFileName,
        //    bool blnDeleteSourceFiles, string strKeyID, string strKeyPass, string strComment, int lTextMode)
        //{
        //    return PGPKeyEncrypt(ref strFiles, strPostPendToFileName, blnDeleteSourceFiles, strKeyID,
        //        strKeyPass, strComment, lTextMode, 0);
        //}

        //public static bool PGPKeyEncrypt(ref string strFiles, string strPostPendToFileName,
        //    bool blnDeleteSourceFiles, string strKeyID, string strKeyPass, string strComment)
        //{
        //    return PGPKeyEncrypt(ref strFiles, strPostPendToFileName, blnDeleteSourceFiles, strKeyID,
        //        strKeyPass, strComment, 0);
        //}

        #endregion

        /// <summary>
        /// Hashing algorithms
        /// </summary>
        public enum HashingAlgorithmsEnum
        {
            HashAlgorithm_Default = 0,
            HashAlgorithm_MD5 = 1,
            HashAlgorithm_SHA = 2,
            HashAlgorithm_RIPEMD160 = 3,
            HashAlgorithm_SHADouble = 4 // not available in PGP 6
        }

        /// <summary>
        /// Simmetric Cipher Algorithms
        /// </summary>
        public enum SymmetricCipherAlgorithmsEnum
        {
            CipherAlgorithm_IDEA = 1,
            CipherAlgorithm_3DES = 2,
            CipherAlgorithm_CAST5 = 3
        }

        #region This part of the code corresponds to the class when an object of this type is instantiated

        #region private members

        private HashingAlgorithmsEnum _hashingAlgorithm = HashingAlgorithmsEnum.HashAlgorithm_MD5;
        private SymmetricCipherAlgorithmsEnum _symmetricCipherAlgorithm = SymmetricCipherAlgorithmsEnum.CipherAlgorithm_IDEA;
        private bool _armor = true;
        private bool _textMode = false;
        private bool _clear = true;
        private bool _compress = false;
        private string _cryptoKeyID = "";
        private string _signKeyID = "";
        private string _signPassword = "";

        private string _cryptoKeyPassphrase = "";

        private bool _logSuccess = true;
        private bool _throwExOnError = false; //Throws an exception in all operations in case of errors

        private PGPLibraryEnum _libraryUsed = PGPLibraryEnum.Freeware; //The underlying library used
        private string _connectionString = ""; //Database connection string if keys are stored in a database repository

        private ILogger _logger = null;

        #endregion

        #region constructors

        /// <summary>
        /// Creates a PGP utility specifying the provider, the connection string where PGP keys are defined, in the systblPGPKeys table and a custom logger
        /// </summary>
        /// <param name="usedLibrary"></param>
        /// <param name="connectionString"></param>
        /// <param name="logger"></param>
        public PGP(PGPLibraryEnum usedLibrary, string connectionString, ILogger logger)
        {
            this.LibraryUsed = usedLibrary;
            this._connectionString = connectionString;
        }

        /// <summary>
        /// Creates a PGP utility specifying the provider, the connection string where PGP keys are defined, in the systblPGPKeys table and not logger
        /// </summary>
        /// <param name="usedLibrary"></param>
        /// <param name="connectionString"></param>
        public PGP(PGPLibraryEnum usedLibrary, string connectionString) : this(usedLibrary, connectionString, null) {}

        /// <summary>
        /// Creates a PGP utility specifying the provider, no connection string where PGP keys are defined (will not work for certain implementations), unless a default was specified.
        /// </summary>
        /// <param name="usedLibrary"></param>
        public PGP(PGPLibraryEnum usedLibrary):this(usedLibrary, ""){}

        /// <summary>
        /// Creates a PGP utility requesting for default provider (actually SecureBlackbox), no connection string where PGP keys are defined (will not work for certain implementations), unless a default was specified.
        /// </summary>
        public PGP() : this(_defaultImplementation) { }

        #endregion

        #region public properties

        /// <summary>
        /// Gets/sets the hash algorithm to use
        /// </summary>
        public HashingAlgorithmsEnum HashingAlgorithm
        {
            get { return _hashingAlgorithm; }
            set { _hashingAlgorithm = value; }
        }

        /// <summary>
        /// Gets/sets the symmetic algorithm to use
        /// </summary>
        public SymmetricCipherAlgorithmsEnum SymmetricCipherAlgorithm
        {
            get { return _symmetricCipherAlgorithm; }
            set { _symmetricCipherAlgorithm = value; }
        }

        /// <summary>
        /// Gests/Sets if armor is used
        /// </summary>
        public bool Armor
        {
            get { return _armor; }
            set { _armor = value; }
        }

        /// <summary>
        /// Gets/Sets if text mode is established. Specifies if whitespaces must be trimmed from the signature
        /// </summary>
        /// <remarks>
        /// Several versions of PGP have bug in their implementations that results in creation of incorrect text signatures.     The following paragraph is taken from RFC 2440: 
        /// «PGP 2.6.X and 5.0 do not trim trailing whitespace from a "canonical text" signature.They only remove it from cleartext signatures.These signatures are not OpenPGP compliant -- OpenPGP requires trimming the whitespace.If you wish to interoperate with PGP 2.6.X or PGP 5, you may wish to accept these non-compliant signatures.» 
        /// One can say that this bug also exists in PGP6.5 and PGP8.0 implementations.It is a good idea to set this property to True if you want to interoperate with those versions of PGP.Set this property to False if you need to create OpenPGP-compliant messages. 
        /// </remarks>
        public bool TextMode
        {
            get { return _textMode; }
            set { _textMode = value; }
        }

        public bool Clear
        {
            get { return _clear; }
            set { _clear = value; }
        }

        public bool Compress
        {
            get { return _compress; }
            set { _compress = value; }
        }

        /// <summary>
        /// Returns the passphrase associated to a given key, stored in the database, in the systblPGPKeys table
        /// </summary>
        /// <returns></returns>
        private string getPassphraseFromDb(string connectionString, string keyID)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new System.Data.SqlClient.SqlCommand("sys_sp_PGPKeyDef_GetPassphrase", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@sKeyID", SqlDbType.VarChar, 20).Value = keyID;
                    cmd.Parameters.Add("@sKeyPassphrase", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@lRetVal", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@nsRetMsg", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;

                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    cnn.Close();

                    if (cmd.Parameters["@lRetVal"].Value == DBNull.Value || ((int)cmd.Parameters["@lRetVal"].Value) != 1)
                        throw new CryptoException("Unable to retrieve from database: " + cmd.Parameters["@nsRetMsg"].Value.ToString());

                    return cmd.Parameters["@sKeyPassphrase"].Value.ToString();
                }
            }
        }

        public string CryptoKeyID
        {
            get { return _cryptoKeyID; }
            set
            {
                if (value != null)
                    _cryptoKeyID = value;
                else
                    _cryptoKeyID = "";

                if (this._libraryUsed == PGPLibraryEnum.SecureBlackBox)
                {
                    //Get the default password
                    _cryptoKeyPassphrase = "";

                    if (string.IsNullOrEmpty(_cryptoKeyID)) return; //there is nothing eles to do here.

                    try
                    {
                        //Try to get the key from database
                        string strCnn = _defaultConnectionString;

                        if (!string.IsNullOrEmpty(_connectionString))
                            strCnn = _connectionString;

                        try { _cryptoKeyPassphrase = getPassphraseFromDb(strCnn, _cryptoKeyID); }
                        catch (Exception ex){ throw new CryptoException("Unable to get encryption key's passphrase: " + ex.Message); }

                    }
                    catch (CryptoException ex)
                    {
                        throw ex;
                    }
                    catch (Exception ex)
                    {
                        throw new CryptoException("Unable to retrieve key passphrasse for given encryption key: " + ex.Message);
                    }
                }
            }
        }

        public string SignKeyID
        {
            get { return _signKeyID; }
            set
            {
                if (value != null)
                    _signKeyID = value;
                else
                    _signKeyID = "";

                //@@2013-04-08 lb SCR# 1739111 Added logic to set the password automatically in case the PGP implementation is SecureBlackBox
                if (this._libraryUsed == PGPLibraryEnum.SecureBlackBox)
                {
                    /*
                     * Although the signing key is independent of the signature passphrase, in implementations like secureblackbox we save in
                     * database the password for full keys, so the password as an internal property is reset and re-established from database.
                     * It can be, however, re-established to any other arbitrary value later.
                     * */

                    //Get the default password
                    _signPassword = "";

                    if (string.IsNullOrEmpty(_signKeyID)) return; //there is nothing eles to do here.

                    try
                    {
                        //Try to get the key from database
                        string strCnn = _defaultConnectionString;

                        if (!string.IsNullOrEmpty(_connectionString))
                            strCnn = _connectionString;

                        try { _signPassword = getPassphraseFromDb(strCnn, _signKeyID); }
                        catch (Exception ex) { throw new CryptoException("Unable to get signing key's passphrase: " + ex.Message); }

                    }
                    catch (CryptoException ex)
                    {
                        throw ex;
                    }
                    catch (Exception ex)
                    {
                        throw new CryptoException("Unable to retrieve key passphrasse for given signing key: " + ex.Message);
                    }
                }
            }
        }

        public string SignPassword
        {
            get { return _signPassword; }
            set
            {
                if (value != null)
                    _signPassword = value;
                else
                    _signPassword = "";
            }
        }

        /// <summary>
        /// Gets/Sets if a message should be written to logs when procedures finish successfully
        /// </summary>
        public bool LogSuccess
        {
            get { return _logSuccess; }
            set { _logSuccess = value; }
        }

        /// <summary>
        /// Gets or sets if an exception is thrown inside operations in case of errors
        /// </summary>
        public bool ThrowExOnError
        {
            get { return _throwExOnError; }
            set { _throwExOnError = value; }
        }

        /// <summary>
        /// Underlying used library
        /// </summary>
        public PGPLibraryEnum LibraryUsed
        {
            get { return _libraryUsed; }
            set 
            {
                switch(value )
                {
                    case PGPLibraryEnum.SecureBlackBox:
                        secureBlackBox_checkIni(true);
                        //Check if initialized
                        break;
                }

                _libraryUsed = value; 
            }
        }

        /// <summary>
        /// Database connection, if such is necesssary
        /// </summary>
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        #endregion

        /// <summary>
        /// Performs the underline process for encrypting, signing or both
        /// </summary>
        /// <param name="fileIn"></param>
        /// <param name="fileOut"></param>
        /// <param name="encrypt"></param>
        /// <param name="sign"></param>
        /// <returns>True if the function finishes successfully</returns>
        private bool processRequest(string fileIn, string fileOut, bool encrypt, bool sign, string comment)
        {
            string error;
            int i = 0;
            string errInfo = string.Format("File In={0}, File Out={1}, Crypto KeyID={2}, Sign ID = {3}, Sign Pwd = <....>, Hashing Algorithm: {4}, Library={5}",
                fileIn, fileOut, _cryptoKeyID, _signKeyID, _hashingAlgorithm, _libraryUsed);

            try
            {
                if (fileIn == null)
                    throw new Exception("Source file has not been specified");
                else if (!File.Exists(fileIn))
                    throw new Exception("Source file " + fileIn + " does not exist");
                else if (fileOut == null)
                    throw new Exception("Destination file has not been specified");
                else if (!encrypt && !sign)
                    throw new Exception("No action requested");
                else if (encrypt && string.IsNullOrEmpty(_cryptoKeyID))//2016-03-03 lb Simplified
                    throw new Exception("Encryption has been requested but Crypto Key has not been provided");
                else if (sign && (string.IsNullOrEmpty(_signKeyID) && string.IsNullOrEmpty(_signPassword)))//2016-03-03 lb Fixed bug
                    throw new Exception("signing has been requested but Signer ID or password has not been provided");

                if (File.Exists(fileOut)) File.Delete(fileOut);

                switch (_libraryUsed)
                {
                    case PGPLibraryEnum.SecureBlackBox:
                        i = secureBlackBox_encodeFile(fileIn, fileOut, encrypt, sign, comment);
                        break;
                    case PGPLibraryEnum.Freeware:
                        // Encrypt/Sign the file
                        i = spgp_encodefile(fileIn, fileOut, (encrypt ? 1 : 0), (sign ? 1 : 0), (int)_hashingAlgorithm, 0, (int)_symmetricCipherAlgorithm,
                            (_armor ? 1 : 0), (_textMode ? 1 : 0), (_clear ? 1 : 0), (_compress ? 1 : 0), 0, 0, _cryptoKeyID, _signKeyID, _signPassword, "",
                            comment, ""); //If everything is ok, this fnuction returns 0
                        break;
                }

                if (!File.Exists(fileOut) || i != 0)
                    throw new Exception(string.Format("File could no be encrypted. Request Info: {0}. Ret-Code: {1}", errInfo,i));

                if (_logSuccess)
                    if (_logger != null)
                        _logger.LogMessage("File " + fileIn + " " + (encrypt && sign ? "encrypted/signed" : (encrypt ? "encrypted" : "signed")) + " sucessfully as " + fileOut);//@@2013-10-29 lb SCR# 1907411 Used Nordea's SCR to include this small fix in the message

                return true;
            }
            catch (CryptoException ex)
            {
                if (_throwExOnError)
                    throw ex;

                if (_logger != null)
                    _logger.LogError(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                error = string.Format("Unexpected error: {0}", ex.Message);

                if (_throwExOnError)
                    throw new CryptoException(error);

                if (_logger != null)
                    _logger.LogError(error);
                return false;
            }
        }

        //Processes the requests of encryption/signing for several files separated by pipes
        private bool processFiles(string filesIn, string newExtension, out string filesOut, bool encrypt, bool sign, string comment)
        {
            string error;
            filesOut = "";
            StringBuilder files_out = null;
            const string NO_FILES = "Source file(s) have not been provided";

            try
            {

                if (filesIn == null || filesIn.Length == 0)
                    throw new Exception(NO_FILES);

                if (newExtension == null)
                    newExtension = ".pgp";
                else
                    if (newExtension[0] != '.') newExtension = "." + newExtension;

                string[] filesFullPaths = filesIn.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                if (filesFullPaths == null || filesFullPaths.Length == 0)
                    throw new Exception(NO_FILES);

                files_out = new StringBuilder();
                foreach (string fileFullPath in filesFullPaths)
                {
                    if (!processRequest(fileFullPath, fileFullPath + newExtension, encrypt, sign, comment))
                        return false;

                    files_out.Append(fileFullPath); files_out.Append(newExtension); files_out.Append('|');
                }

                if (files_out.Length != 0) files_out.Remove(files_out.Length - 1, 1);

                filesOut = files_out.ToString();

                return true;
            }
            catch (CryptoException ex)
            {
                if (_throwExOnError)
                    throw ex;

                if (_logger != null)
                    _logger.LogError(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                error = string.Format("Unexpected error: {0}", ex.Message);

                if (_throwExOnError)
                    throw new CryptoException(error);

                if (_logger != null)
                    _logger.LogError(error);

                return false;
            }
        }

        /// <summary>
        /// Encrypts a single file
        /// </summary>
        /// <param name="fileIn"></param>
        /// <param name="fileOut"></param>
        /// <param name="comment"></param>
        /// <returns>True if the function finishes successfully</returns>
        public bool EncryptOneFile(string fileIn, string fileOut, string comment)
        {
            return processRequest(fileIn, fileOut, true, false, comment);
        }

        /// <summary>
        /// Encrypts a single file
        /// </summary>
        /// <param name="fileIn"></param>
        /// <param name="fileOut"></param>
        /// <param name="comment"></param>
        /// <return>True if the function finishes successfully</return>
        public bool EncryptOneFile(string fileIn, string comment)
        {
            return EncryptOneFile(fileIn, fileIn + ".pgp", comment);
        }

        /// <summary>
        /// Signs a single file
        /// </summary>
        /// <param name="fileIn"></param>
        /// <param name="fileOut"></param>
        /// <param name="comment"></param>
        /// <return>True if the function finishes successfully</return>
        public bool SignOneFile(string fileIn, string fileOut, string comment)
        {
            return processRequest(fileIn, fileOut, false, true, comment);
        }

        /// <summary>
        /// Signs a single file
        /// </summary>
        /// <param name="fileIn"></param>
        /// <param name="fileOut"></param>
        /// <param name="comment"></param>
        /// <return>True if the function finishes successfully</return>
        public bool SignOneFile(string fileIn, string comment)
        {
            return SignOneFile(fileIn, fileIn + ".pgp", comment);
        }

        /// <summary>
        /// Encrypts AND signsa a single file
        /// </summary>
        /// <param name="fileIn"></param>
        /// <param name="fileOut"></param>
        /// <param name="comment"></param>
        /// <return>True if the function finishes successfully</return>
        public bool EncryptAndSignOneFile(string fileIn, string fileOut, string comment)
        {
            return processRequest(fileIn, fileOut, true, true, comment);
        }

        /// <summary>
        /// Encrypts AND signsa a single file
        /// </summary>
        /// <param name="fileIn"></param>
        /// <param name="fileOut"></param>
        /// <param name="comment"></param>
        /// <return>True if the function finishes successfully</return>
        public bool EncryptAndSignOneFile(string fileIn, string comment)
        {
            return EncryptAndSignOneFile(fileIn, fileIn + ".pgp", comment);
        }

        /// <summary>
        /// Encrypts several files separated by pipes
        /// </summary>
        /// <param name="filesIn"></param>
        /// <param name="newExtension"></param>
        /// <param name="comment"></param>
        /// <return>True if the function finishes successfully</return>
        public bool EncryptFiles(string filesIn, string newExtension, out string filesOut, string comment)
        {
            return processFiles(filesIn, newExtension, out filesOut, true, false, comment);
        }

        /// <summary>
        /// Signs several files separated by pipes
        /// </summary>
        /// <param name="filesIn"></param>
        /// <param name="newExtension"></param>
        /// <param name="filesOut"></param>
        /// <param name="comment"></param>
        /// <return>True if the function finishes successfully</return>
        public bool SignFiles(string filesIn, string newExtension, out string filesOut, string comment)
        {
            return processFiles(filesIn, newExtension, out filesOut, false, true, comment);
        }

        /// <summary>
        /// Encrypts AND signs several files separated by pipes
        /// </summary>
        /// <param name="filesIn"></param>
        /// <param name="newExtension"></param>
        /// <param name="filesOut"></param>
        /// <param name="comment"></param>
        /// <return>True if the function finishes successfully</return>
        public bool EncryptAndSignFiles(string filesIn, string newExtension, out string filesOut, string comment)
        {
            return processFiles(filesIn, newExtension, out filesOut, true, true, comment);
        }

        //20160428 hl-begin
        //Larry, I added the following temporary codes to make it work.  Replace with your own codes, when you have time
        #region 20160428 hl temporary codes
        private bool processRequest(byte[] array_In, out byte[] array_Out, bool encrypt, bool sign, string comment)
        {
            string error;
            int i = 0;
            const string ERROR_TMPFILE = "Unable to create temporary files. Please, delete unneeded";
            string file_In = "";
            string file_Out = "";
            array_Out = null;

            string errInfo = string.Format("Crypto KeyID={0}, Sign ID = {1}, Sign Pwd = <....>, Hashing Algorithm: {2}, Library={3}",
                _cryptoKeyID, _signKeyID, _hashingAlgorithm, _libraryUsed);

            try
            {
                if (array_In == null || array_In.Length == 0) throw new Exception("Source byte array cannot be null or empty");

                try { file_In = Path.GetTempFileName(); }
                catch (IOException) { throw new Exception(ERROR_TMPFILE); }

                try { file_Out = Path.GetTempFileName(); }
                catch (IOException) { throw new Exception(ERROR_TMPFILE); }

                if (!encrypt && !sign)
                    throw new Exception("No action requested");
                else if (encrypt && string.IsNullOrEmpty(_cryptoKeyID))//2016-03-03 lb Simplified
                    throw new Exception("Encryption has been requested but Crypto Key has not been provided");
                else if (sign && (string.IsNullOrEmpty(_signKeyID) && string.IsNullOrEmpty(_signPassword)))//2016-03-03 lb Fixed bug
                    throw new Exception("signing has been requested but Signer ID or password has not been provided");

                if (File.Exists(file_Out)) File.Delete(file_Out);

                using (FileStream fs = new FileStream(file_In, FileMode.Create, FileAccess.Write))
                {//20160422 hl, need to put bytes to temp file
                    fs.Write(array_In, 0, array_In.Length); fs.Flush();
                }

                switch (_libraryUsed)
                {
                    case PGPLibraryEnum.SecureBlackBox:
                        i = secureBlackBox_encodeFile(file_In, file_Out, encrypt, sign, comment);
                        break;
                    case PGPLibraryEnum.Freeware:
                        // Encrypt/Sign the file
                        i = spgp_encodefile(file_In, file_Out, (encrypt ? 1 : 0), (sign ? 1 : 0), (int)_hashingAlgorithm, 0, (int)_symmetricCipherAlgorithm,
                            (_armor ? 1 : 0), (_textMode ? 1 : 0), (_clear ? 1 : 0), (_compress ? 1 : 0), 0, 0, _cryptoKeyID, _signKeyID, _signPassword, "",
                            comment, ""); //If everything is ok, this fnuction returns 0
                        break;
                }

                if (!File.Exists(file_Out) || i != 0)
                    throw new Exception(string.Format("Source byte array could no be encrypted. Request Info: {0}. Ret-Code: {1}", errInfo, i));
                else
                {
                    FileInfo fi = new FileInfo(file_Out);
                    array_Out = new byte[fi.Length];

                    using (FileStream fs = fi.OpenRead())
                    {
                        fs.Read(array_Out, 0, array_Out.Length);
                    }
                }

                if (_logSuccess)
                    if (_logger != null)
                        _logger.LogMessage("Source byte array " + (encrypt && sign ? "encrypted/signed" : (encrypt ? "encrypted" : "signed")) + " sucessfully");

                return true;
            }
            catch (CryptoException ex)
            {
                if (_throwExOnError)
                    throw ex;

                if (_logger != null)
                    _logger.LogError(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                error = string.Format("Unexpected error: {0}", ex.Message);

                if (_throwExOnError)
                    throw new CryptoException(error);

                if (_logger != null)
                    _logger.LogError(error);

                return false;
            }
            finally
            {
                if (!string.IsNullOrEmpty(file_In)) File.Delete(file_In);
                if (!string.IsNullOrEmpty(file_Out)) File.Delete(file_Out);
            }
        }

        public bool EncryptByteArray(byte[] array_In, out byte[] array_Out, string comment)
        {
            return processRequest(array_In, out array_Out, true, false, comment);
        }

        //This function decrypts and verifies the signature of an array of bytes
        public bool PGPKeyDecryptAndVerifyByteArray(byte[] array_In, out byte[] array_Out, string keyPwd)
        {
            const string ERROR_TMPFILE = "Unable to create temporary files. Please, delete unneeded";
            string file_In = "";
            string file_Out = "";
            string error;
            array_Out = null;

            try
            {
                int i = 0;

                if (array_In == null || array_In.Length == 0) throw new Exception("Source byte array cannot be null or empty");

                try { file_In = Path.GetTempFileName(); }
                catch (IOException) { throw new Exception(ERROR_TMPFILE); }

                try { file_Out = Path.GetTempFileName(); }
                catch (IOException) { throw new Exception(ERROR_TMPFILE); }

                //Delete the output file if it exists
                if (File.Exists(file_Out)) File.Delete(file_Out);

                using (FileStream fs = new FileStream(file_In, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(array_In, 0, array_In.Length); fs.Flush();
                }

                switch (_libraryUsed)
                {
                    case PGPLibraryEnum.SecureBlackBox:
                        i = secureBlackBox_decodeFile(file_In, file_Out, keyPwd);
                        break;
                    case PGPLibraryEnum.Freeware:
                        i = spgpdecodefile(file_In, file_Out, keyPwd, "");
                        break;
                }

                //check signature properties
                if (!File.Exists(file_Out) || i != 0)
                {
                    error = "Byte array could no be decrypted/verified.";

                    if (_throwExOnError)
                        throw new Exception(error);

                    if (_logger != null)
                        _logger.LogError(error);

                    return false;
                }
                else
                {
                    FileInfo fi = new FileInfo(file_Out);
                    array_Out = new byte[fi.Length];

                    using (FileStream fs = fi.OpenRead())
                    {
                        fs.Read(array_Out, 0, array_Out.Length);
                    }

                    return true;
                }

            }
            catch (CryptoException ex)
            {
                if (_throwExOnError)
                    throw ex;

                if (_logger != null)
                    _logger.LogError(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                error = string.Format("Unexpected error: {0}", ex.Message);

                if (_throwExOnError)
                    throw new CryptoException(error);

                if (_logger != null)
                    _logger.LogError(error);
                return false;
            }
            finally
            {
                if (!string.IsNullOrEmpty(file_In)) File.Delete(file_In);
                if (!string.IsNullOrEmpty(file_Out)) File.Delete(file_Out);
            }
        }
        #endregion

        #endregion
    }
}
