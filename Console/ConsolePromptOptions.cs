namespace KeyLocker.Console
{
    using System;

    using KeyLocker.Console.Validation;

    /// <summary>
    /// Optionen für eine Konsolenausgabe.
    /// </summary>
    public class ConsolePromptOptions : ConsoleWriteOptions
    {
        /// <summary>
        /// Holt einen Wert, der angibt, ob die Eingabe versteckt werden soll oder nicht.
        /// </summary>
        public bool Hidden
        {
            get;
            set;
        }

        /// <summary>
        /// Holt den <see cref="IInputValidator"/> der die Eingabe validieren soll.
        /// </summary>
        public IInputValidator? Validator
        {
            get;
            set;
        }

        /// <summary>
        /// Holt einen Wert, der angibt, ob eine leere Eingabe unabhängig von <see cref="Validator"/>
        /// akzeptiert werden soll und dann <code>default(string)</code> zurückgibt.
        /// </summary>
        public bool AllowSkip
        {
            get;
            set;
        }
    }
}
