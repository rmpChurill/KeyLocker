namespace KeyLocker.Console
{
    using System;

    /// <summary>
    /// Optionen für eine Konsolenausgabe.
    /// </summary>
    public class ConsoleWriteOptions
    {
        /// <summary>
        /// Holt die Textfarbe.
        /// </summary>
        public ConsoleColor? TextColor
        {
            get;
            set;
        }

        /// <summary>
        /// Holt die Hintergrundfarbe.
        /// </summary>
        public ConsoleColor? BackgroundColor
        {
            get;
            set;
        }
    }
}
