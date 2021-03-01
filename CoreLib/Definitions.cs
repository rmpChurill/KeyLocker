namespace KeyLocker
{
    /// <summary>
    /// Enthält Definitionen für Buchstabenfruppen.
    /// </summary>
    public static class Definitions
    {
        /// <summary>
        /// Enthält Kleinbuchstaben.
        /// </summary>
        public static char[] UpperCaseChars = "ACBDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        /// <summary>
        /// Enthält Großbuchstaben.
        /// </summary>
        public static char[] LowerCaseChars = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

        /// <summary>
        /// Enthält Ziffern.
        /// </summary>
        public static char[] Digits = "0123456789".ToCharArray();

        /// <summary>
        /// Enthält Sonderzeichen.
        /// </summary>
        public static char[] SpecialCharacters = "^+*~#-.:,;!\"§$%&/()=?\\{[]}".ToCharArray();
    }
}
