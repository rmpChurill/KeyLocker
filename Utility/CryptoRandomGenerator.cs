namespace KeyLocker.Utility
{
    using System.Security.Cryptography;

    /// <summary>
    /// Enthält einige statische Methoden für die Erzeugung von Kryptografischen Zufallswerten.
    /// </summary>
    public class CryptoRandomGenerator
    {
        /// <summary>
        /// Der Provider für Crypto-Services.
        /// </summary>
        private readonly RNGCryptoServiceProvider cryptoServiceProvider;

        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse.
        /// </summary>
        public CryptoRandomGenerator()
        {
            this.cryptoServiceProvider = new RNGCryptoServiceProvider();
        }

        /// <summary>
        /// Gibt ein zufälliges Zeichen aus <paramref name="src"/> zurück.
        /// </summary>
        /// <param name="src">Das Array, aus dem ein zufälliges Element entnommen wird.</param>
        /// <returns>Ein zufälliges Zeichen aus <paramref name="src"/>.</returns>
        public char RandomCharFrom(char[] src)
        {
            var pos = this.GenerateRandomUint((uint)src.Length);

            return src[(int)pos];
        }

        /// <summary>
        /// Erzeugt einen zufälligen <see cref="uint"/> im Bereich [min, max).
        /// </summary>
        /// <param name="min">Das Minimum (inklusiv).</param>
        /// <param name="max">Das Maximum (exklusiv).</param>
        /// <returns>Einen zufälligen <see cref="uint"/> im Bereich [min, max).</returns>
        public uint GenerateRandomUint(uint min, uint max)
        {
            if (min > max)
            {
                var tmp = max;
                max = min;
                min = tmp;
            }

            return this.GenerateRandomUint(max - min) + min;
        }

        /// <summary>
        /// Erzeugt einen zufälligen <see cref="uint"/> im Bereich [0, max).
        /// </summary>
        /// <param name="max">Das Maximum (exklusiv).</param>
        /// <returns>Einen zufälligen <see cref="uint"/> im Bereich [0, max).</returns>
        public uint GenerateRandomUint(uint max)
        {
            return this.GenerateRandomUint() % max;
        }

        /// <summary>
        /// Erzeugt einen zufälligen <see cref="uint"/> aus einer cryptografiesicheren Quelle.
        /// </summary>
        /// <returns>Ein zufälliger <see cref="uint"/>.</returns>
        public uint GenerateRandomUint()
        {
            var buf = new byte[4];

            this.cryptoServiceProvider.GetBytes(buf);

            var res = (uint)buf[3] |
                ((uint)buf[2]) << 8 |
                ((uint)buf[1]) << 16 |
                ((uint)buf[0]) << 24;

            return res;
        }
    }
}
