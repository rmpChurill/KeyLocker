namespace KeyLocker
{
    using KeyLocker.Utility;

    using System;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Stellt verschiedene Cryptografische Funktionen zur Verfügung.
    /// </summary>
    public static class Crypto
    {
        /// <summary>
        /// Die Länge des Salts.
        /// </summary>
        private const int SaltLength = 16;

        /// <summary>
        /// Abzahl Bits im Schlüssel.
        /// </summary>
        private const int Keysize = 256;

        /// <summary>
        /// Die Anzahl der Iterationen für die Passworterzeugung.
        /// </summary>
        private const int DerivationIterations = 1000;

        /// <summary>
        /// Erzeugt ein zufälliges Passwort nach den Einstellungen von <paramref name="settings"/>.
        /// </summary>
        /// <param name="settings">Die zu nutzenden Einstellungen.</param>
        /// <returns>Ein zufälliges Passwort nach den Einstellungen von <paramref name="settings"/>.</returns>
        public static string GeneratePassword(PasswordSettings settings)
        {
            var rng = new CryptoRandomGenerator();
            var len = rng.GenerateRandomUint(settings.MinLength, settings.MaxLength + 1);
            var res = new char[len];
            var allowedChars = settings.GetAllowedCharacters();
            var indices = new int[res.Length];

            for (var i = 0; i < indices.Length; i++)
            {
                indices[i] = i;
            }

            for (var i = 0; i < indices.Length - 1; i++)
            {
                var indexToSwap = rng.GenerateRandomUint((uint)i, (uint)indices.Length);
                var buf = indices[indexToSwap];
                indices[indexToSwap] = indices[i];
                indices[i] = buf;
            }

            var index = 0;

            if (settings.Digits == Usage.Require)
            {
                res[indices[index]] = rng.RandomCharFrom(Definitions.Digits);

                index++;
            }

            if (settings.UpperCaseChars == Usage.Require)
            {
                res[indices[index]] = rng.RandomCharFrom(Definitions.UpperCaseChars);

                index++;
            }

            if (settings.SpecialCharacters == Usage.Require)
            {
                res[indices[index]] = rng.RandomCharFrom(Definitions.SpecialCharacters);

                index++;
            }

            while (index < indices.Length)
            {
                res[indices[index]] = rng.RandomCharFrom(allowedChars);

                index++;
            }

            return new string(res);
        }


        /// <summary>
        /// Berechnet einen Hash mit Salt.
        /// </summary>
        /// <param name="text">Der zu hashende Text.</param>
        /// <param name="salt">Das "Salt".</param>
        /// <returns></returns>
        public static string ComputeSaltedHash(string text, string salt)
        {
            return string.Concat(text, salt).GetHashCode().ToString("X");
        }

        /// <summary>
        /// Berechnet einen Salt-String.
        /// </summary>
        /// <returns></returns>
        public static string ComputeRandomSalt()
        {
            var rnd = new Random();
            var res = new char[SaltLength];

            for (int i = 0, max = SaltLength; i < max; i++)
            {
                res[i] = (char)rnd.Next(char.MaxValue);
            }

            return new string(res).GetHashCode().ToString("X");
        }

        /// <summary>
        /// Codiert <paramref name="text"/>.
        /// </summary>
        /// <param name="text">Der zu verschlüsselnde Text.</param>
        /// <param name="appSettings">Die zu nutzenden Einstellungen.</param>
        /// <returns>Der codierte String.</returns>
        public static string Encode(string text, AppSettings appSettings)
        {
            return Encrypt(text, appSettings.SaltedPasswordHash);
        }

        /// <summary>
        /// Decodiert <paramref name="text"/>.
        /// </summary>
        /// <param name="text">Der zu entschlüsselnde Text.</param>
        /// <param name="appSettings">Die zu nutzenden Einstellungen.</param>
        /// <returns>Der decodierte String.</returns>
        public static string Decode(string text, AppSettings appSettings)
        {
            return Decrypt(text, appSettings.SaltedPasswordHash);
        }

        /// <summary>
        /// Verschlüsselt <paramref name="plainText"/> und nutzt <paramref name="passPhrase"/> als Schlüssel.
        /// </summary>
        /// <param name="plainText">Der zu verschlüsselnde Text.</param>
        /// <param name="passPhrase">Der Schlüssel.</param>
        /// <returns>Der verschlüsselte Text.</returns>
        public static string Encrypt(string plainText, string passPhrase)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                return string.Empty;
            }

            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            var saltStringBytes = GenerateEntropyBits(128);
            var ivStringBytes = GenerateEntropyBits(128);
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            using var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations);

            var keyBytes = password.GetBytes(Keysize / 8);

            using var symmetricKey = new RijndaelManaged
            {
                BlockSize = 128,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };

            using var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes);
            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.

            var cipherTextBytes = saltStringBytes;

            cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
            cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
            memoryStream.Close();
            cryptoStream.Close();

            return Convert.ToBase64String(cipherTextBytes);
        }

        /// <summary>
        /// Entschlüsselt <paramref name="cipherText"/> mit <paramref name="passPhrase"/> als Schlüssel.
        /// </summary>
        /// <param name="cipherText">Der verschlüsselte Text.</param>
        /// <param name="passPhrase">Der Schlüssel.</param>
        /// <returns>Den entschlüsselten Text.</returns>
        public static string Decrypt(string cipherText, string passPhrase)
        {
            if (string.IsNullOrEmpty(cipherText))
            {
                return string.Empty;
            }

            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            using var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations);

            var keyBytes = password.GetBytes(Keysize / 8);

            using var symmetricKey = new RijndaelManaged
            {
                BlockSize = 256,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };

            using var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes);
            using var memoryStream = new MemoryStream(cipherTextBytes);
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

            var plainTextBytes = new byte[cipherTextBytes.Length];
            var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

            memoryStream.Close();
            cryptoStream.Close();

            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }

        /// <summary>
        /// Erzeugt <paramref name="length"/> zufällige Bits.
        /// </summary>
        /// <param name="length">Die Anzahl der zu erzeugenden Bits. Nur durch 8 teilbare Werte sind zulässig.</param>
        /// <returns>256 zufällige Bits.</returns>
        private static byte[] GenerateEntropyBits(int length)
        {
            var randomBytes = new byte[length / 8];

            using var rngCsp = new RNGCryptoServiceProvider();

            rngCsp.GetBytes(randomBytes);

            return randomBytes;
        }
    }
}