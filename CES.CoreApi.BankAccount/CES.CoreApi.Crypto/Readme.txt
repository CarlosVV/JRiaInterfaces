//Here it comes a sample for the usage of this library

namespace CES.CoreApi.Crypto
{
    class Class1
    {
        public void Main()
        {
            //Initialize first statickly the library. This must be done only once, during the life of the application!!
            PGP.Initialize(new string[] { "license number comes here" }, "Default connection string");

            #region encryption sample

            PGP pgpObj = new PGP(PGP.PGPLibraryEnum.SecureBlackBox); //do not use PGP.PGPLibraryEnum.Freeware anymore. We are migrating away from this provider

            pgpObj.CryptoKeyID = "0x1D37C15C67D7088E"; //the key for encryption
            pgpObj.SignKeyID = "0x315D812B9A1CDF47"; //the key to sign. Password is encrypted in the database so it is not necessary to specify it; the library will recover it from systblPGPKeys
            pgpObj.SignPassword = ""; //to override the password stored in database. Or simply do not specify it
            pgpObj.SymmetricCipherAlgorithm = PGP.SymmetricCipherAlgorithmsEnum.CipherAlgorithm_CAST5; //The default is SymmetricCipherAlgorithmsEnum.CipherAlgorithm_IDEA
            pgpObj.HashingAlgorithm = PGP.HashingAlgorithmsEnum.HashAlgorithm_MD5;//by default PGP.HashingAlgorithmsEnum.HashAlgorithm_Default
            pgpObj.LogSuccess = true; //logger should have been defined at the moment of instantiating the PGP object. False by default
            pgpObj.ThrowExOnError = false; //Raises an exception on failure

            string fileIn = "source file", fileOut = "output file. if blank it will be same as source, having appended .pgp";

            pgpObj.EncryptAndSignOneFile(fileIn, fileOut, "A comment that will be visible if Armor=true");

            #endregion

            #region #region decryption sample

            PGP pgpObj2 = new PGP(PGP.PGPLibraryEnum.SecureBlackBox); //do not use PGP.PGPLibraryEnum.Freeware anymore. We are migrating away from this provider

            pgpObj2.CryptoKeyID = "0x1D37C15C67D7088E"; //the key for decryption
            pgpObj2.SignKeyID = "0x315D812B9A1CDF47"; //the key which signature we want to validate.
            pgpObj2.SymmetricCipherAlgorithm = PGP.SymmetricCipherAlgorithmsEnum.CipherAlgorithm_CAST5; //The default is SymmetricCipherAlgorithmsEnum.CipherAlgorithm_IDEA
            pgpObj2.HashingAlgorithm = PGP.HashingAlgorithmsEnum.HashAlgorithm_MD5;//by default PGP.HashingAlgorithmsEnum.HashAlgorithm_Default
            pgpObj2.LogSuccess = true; //logger should have been defined at the moment of instantiating the PGP object. False by default
            pgpObj2.ThrowExOnError = false; //Raises an exception on failure

            string fileIn1 = "source file", fileOut2 = "output file. if blank it will be same as source, having appended .pgp";

            pgpObj2.PGPKeyDecryptAndVerifyFile(fileIn1, fileOut2, "A password if it is not stored with our crypto key, in the database");

            #endregion
        }
    }
}
