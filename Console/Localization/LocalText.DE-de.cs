using System;

namespace KeyLocker.Console.Localization
{
    /// <summary>
    /// Stellt lokalisiert Texte auf Deutsch bereit.
    /// </summary>
    public class LocalTextDEde : ILocalText
    {
        /// <inheritdoc />
        public string GetText(TextCode text)
        {
            switch (text)
            {
                default:
                    throw new NotImplementedException($"No localization for Text {text}!");
            }
        }
    }
}