namespace KeyLocker
{
    using System;

    public static class RandomGenerator
    {
        private static readonly Random rnd = new Random();

        public static string Generate()
        {
            return Generate(Settings.Instance);
        }

        public static string Generate(Settings settings)
        {
            var allowedChars = Definitions.LowerCaseChars;
            var res = new char[rnd.Next(settings.MinLength, settings.MaxLength + 1)];

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
                allowedChars += Definitions.SpecialCharacters;
            }

            var assingments = new int[res.Length];

            for (var i = 0; i < res.Length - 1; i++)
            {
                var other = rnd.Next(res.Length - i - 1) + i;
                var buf = assingments[other];
                assingments[other] = assingments[i];
                assingments[i] = buf;
            }

            var index = 0;

            if (settings.Digits == Usage.Require)
            {
                if (index >= assingments.Length)
                {
                    throw new NotSupportedException();
                }

                res[assingments[index++]] = RandomChar(Definitions.Digits);
            }

            if (settings.UpperCaseChars != Usage.Forbid)
            {
                if (index >= assingments.Length)
                {
                    throw new NotSupportedException();
                }

                res[assingments[index++]] = RandomChar(Definitions.UpperCaseChars);
            }

            if (settings.SpecialCharacters != Usage.Forbid)
            {
                if (index >= assingments.Length)
                {
                    throw new NotSupportedException();
                }

                res[assingments[index++]] = RandomChar(Definitions.SpecialCharacters);
            }

            while (index < assingments.Length)
            {
                res[assingments[index++]] = RandomChar(allowedChars);
            }

            return new string(res);
        }

        private static char RandomChar(string src)
        {
            return src[rnd.Next(0, src.Length)];
        }
    }
}
