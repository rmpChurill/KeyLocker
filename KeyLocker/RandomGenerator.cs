namespace KeyLocker
{
    using System;
    using System.Security.Cryptography;

    public static class RandomGenerator
    {
        private static readonly RNGCryptoServiceProvider cryptoServiceProvider = new RNGCryptoServiceProvider();

        public static string Generate(PasswordSettings settings)
        {
            var res = new char[GenerateRandomInteger(settings.MinLength, settings.MaxLength + 1)];
            var allowedChars = GetAllowedCharacters(settings);
            var indices = new int[res.Length];

            for (var i = 0; i < indices.Length; i++)
            {
                indices[i] = i;
            }

            for (var i = 0; i < indices.Length - 1; i++)
            {
                var indexToSwap = GenerateRandomInteger(i, indices.Length);
                var buf = indices[indexToSwap];
                indices[indexToSwap] = indices[i];
                indices[i] = buf;
            }

            var index = 0;

            if (settings.Digits == Usage.Require)
            {
                res[indices[index]] = RandomCharFrom(Definitions.Digits);

                index++;
            }

            if (settings.UpperCaseChars == Usage.Require)
            {
                res[indices[index]] = RandomCharFrom(Definitions.UpperCaseChars);

                index++;
            }

            if (settings.SpecialCharacters == Usage.Require)
            {
                res[indices[index]] = RandomCharFrom(Definitions.SpecialCharacters);

                index++;
            }

            while (index < indices.Length)
            {
                res[indices[index]] = RandomCharFrom(allowedChars);

                index++;
            }

            return new string(res);
        }

        private static string GetAllowedCharacters(PasswordSettings settings)
        {
            var allowedChars = Definitions.LowerCaseChars;

            if (settings.Digits != Usage.Forbid)
            {
                allowedChars += Definitions.Digits;
            }

            if (settings.UpperCaseChars != Usage.Forbid)
            {
                allowedChars += Definitions.UpperCaseChars;
            }

            if (settings.SpecialCharacters != Usage.Forbid)
            {
                allowedChars += settings.SpecialCharacters;
            }

            foreach (var c in settings.ForbiddenCharacters)
            {
                var index = allowedChars.IndexOf(c);

                if (index >= 0)
                {
                    allowedChars = allowedChars.Remove(index, 1);
                }
            }

            return allowedChars;
        }

        private static char RandomCharFrom(string src)
        {
            return src[GenerateRandomPositiveInteger(src.Length)];
        }

        private static int GenerateRandomInteger(int min, int max)
        {
            return GenerateRandomPositiveInteger(max - min) + min;
        }

        private static int GenerateRandomPositiveInteger(int max)
        {
            return (int)(GenerateRandomInt() % (uint)max);
        }

        private static uint GenerateRandomInt()
        {
            var buf = new byte[4];

            cryptoServiceProvider.GetBytes(buf);

            var res = 0u;

            res = (res | buf[0]) << 8;
            res = (res | buf[1]) << 8;
            res = (res | buf[2]) << 8;
            res = (res | buf[3]);

            return res;
        }
    }
}
