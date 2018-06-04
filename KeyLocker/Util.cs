using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace KeyLocker
{
    public static class Util
    {
        private const int SaltLength = 16;

        // This constant is used to determine the keysize of the encryption algorithm in bits.
        // We divide this by 8 within the code below to get the equivalent number of bytes.
        private const int Keysize = 256;

        // This constant determines the number of iterations for the password bytes generation function.
        private const int DerivationIterations = 1000;

        public static string ComputeSaltedHash(string text)
        {
            return string.Concat(text, Settings.Instance.Salt).GetHashCode().ToString("X");
        }

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

        internal static void Set(PropertyInfo property, object obj, string data)
        {
            if (property.PropertyType.Equals(typeof(string)))
            {
                property.SetValue(obj, data);
            }
            else if (property.PropertyType.IsEnum)
            {
                property.SetValue(obj, Enum.Parse(property.PropertyType, data));
            }
            else if (property.PropertyType.Equals(typeof(int)))
            {
                property.SetValue(obj, int.Parse(data));
            }
            else if (property.PropertyType.Equals(typeof(bool)))
            {
                property.SetValue(obj, bool.Parse(data));
            }
            else if(property.PropertyType.Equals(typeof(DateTime)))
            {
                property.SetValue(obj, DateTime.Parse(data));
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public static string Encode(string text)
        {
            return Encrypt(text, Settings.Instance.SaltedPasswordHash);
        }

        public static string Decode(string text)
        {
            return Decrypt(text, Settings.Instance.SaltedPasswordHash);
        }

        public static string Encrypt(string plainText, string passPhrase)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                return string.Empty;
            }

            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate256BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
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
                        }
                    }
                }
            }
        }

        public static bool CreateNewPassword()
        {
            var newPasswordCreated = false;

            using (var dialog = new NewPasswordDialog())
            {
                var result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    Settings.Instance.Salt = Util.ComputeRandomSalt();
                    Settings.Instance.SaltedPasswordHash = Util.ComputeSaltedHash(dialog.Password);
                    newPasswordCreated = true;
                }
            }

            return newPasswordCreated;
        }

        public static bool RequestPassword()
        {
            var passwordMatched = false;
            var cancelRequested = false;
            var failures = 0;

            while (!passwordMatched && !cancelRequested)
            {
                using (var dialog = new PasswordDialog())
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        if (dialog.HashedPassword.Equals(Settings.Instance.SaltedPasswordHash))
                        {
                            passwordMatched = true;
                        }
                        else
                        {
                            MessageBox.Show("Wrong Password!");
                            failures++;
                        }
                    }
                    else if (dialog.DialogResult == DialogResult.Cancel)
                    {
                        cancelRequested = true;
                    }
                }
            }

            return passwordMatched;
        }

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

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }

        public static void CreateFile(string path)
        {
            CreateSubDirs(Path.GetDirectoryName(path));
            File.Create(path);
        }

        private static void CreateSubDirs(string path)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                CreateSubDirs(Path.GetDirectoryName(path));
            }

            Directory.CreateDirectory(path);
        }

        public static bool IsOutdated(DateTime date, DateTime compare, int time, TimeUnit timeUnit)
        {
            switch (timeUnit)
            {
                case TimeUnit.Days:
                    return (compare - date).TotalDays > time;
                case TimeUnit.Months:
                    return (compare.Year - date.Year - 1) * 12 + date.Month - compare.Month > time;
                case TimeUnit.Years:
                    return compare.Year - date.Year > time;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}