namespace KeyLocker.Utility.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using KeyLocker.Utility.Console.Validation;

    /// <summary>
    /// Eine Hilfsklasse für Ein- und Ausgabe auf der Konsole.
    /// </summary>
    public static class ConsoleHelper
    {
        /// <summary>
        /// Füllt eine Zeile des Terminals mit <paramref name="seperator"/>.
        /// </summary>
        /// <param name="seperator">Das auszugebende Zeichen.</param>
        public static void WriteSeperator(char seperator = '-')
        {
            Console.WriteLine(new string(seperator, Console.WindowWidth));
        }

        /// <summary>
        /// Gibt <paramref name="text"/> aus und wendet die Optionen <paramref name="options"/> an.
        /// </summary>
        /// <param name="text">Der auszugebende Text.</param>
        /// <param name="options">Die anzuwendenden Optionen</param>
        public static void Write(string text, ConsoleWriteOptions? options = null)
        {
            options ??= new ConsoleWriteOptions();

            var oldFg = Console.ForegroundColor;
            var oldBg = Console.BackgroundColor;

            Console.ForegroundColor = options.TextColor ?? oldFg;
            Console.BackgroundColor = options.BackgroundColor ?? oldBg;

            Console.Write(text);

            Console.ForegroundColor = oldFg;
            Console.BackgroundColor = oldBg;
        }

        /// <summary>
        /// Schreibt <paramref name="question"/> (Standardwert ist "> "), liest eine Zeile der Nutzereingabe und gibt diese zurück.
        /// </summary>
        /// <param name="question">Der Text, der vor der Eingabe angezeigt werden soll.</param>
        /// <param name="options">Die anzuwendenden Optionen.</param>
        /// <returns>Die Nutzereingabe.</returns>
        public static string Prompt(string question = "> ", ConsolePromptOptions? options = null)
        {
            options ??= new ConsolePromptOptions();

            string? res;

            while (true)
            {
                res = ReadLine(question, options);

                if (options.Validator != null &&
                    options.AllowSkip &&
                    string.IsNullOrEmpty(res))
                {
                    return string.Empty;
                }

                res ??= string.Empty;

                if (options.Validator == null ||
                    options.Validator.IsValid(res))
                {
                    return res;
                }
            }
        }

        /// <summary>
        /// Schreibt <paramref name="question"/> (Standardwert ist "> ") und liest eine Zeile der Nutzereingabe, 
        /// die als boolescher Wert interpretiert wird.
        /// </summary>
        /// <param name="question">Der Text, der vor der Eingabe angezeigt werden soll.</param>
        /// <param name="options">Die anzuwendenden Optionen.</param>
        /// <returns>True, wenn die Eingabe y oder Y war, false wenn die Eingabe n oder N war.</returns>
        public static bool PromptBool(string question = "> ", ConsolePromptOptions? options = null)
        {
            options ??= new ConsolePromptOptions();

            options.Validator = options.Validator == null
                              ? new IsYesNoValidator()
                              : new LogicalAndValidator(options.Validator, new IsYesNoValidator());

            var res = Prompt(question, options);

            return res == "y" || res == "Y";
        }

        /// <summary>
        /// Schreibt eine Auflistung von Elementen in die Konsole.
        /// </summary>
        /// <typeparam name="T">Der Typ der auszugebenden Items.</typeparam>
        /// <param name="items">Die auszugebenden Items.</param>
        /// <param name="seperator">Der string, der nach jedem Element, außer dem letzten, aus <paramref name="items"/> ausgegeben wird.</param>
        public static void WriteAll<T>(IEnumerable<T> items, string seperator = ", ")
        {
            var itemEnumerator = items.GetEnumerator();

            if (itemEnumerator.MoveNext())
            {
                Console.Write(itemEnumerator.Current);
            }

            while (itemEnumerator.MoveNext())
            {
                Console.Write(seperator);
                Console.Write(itemEnumerator.Current);
            }
        }

        /// <summary>
        /// Liest eine Zeile der Nutzereingabe ein und behandelt Sonderzeichen, etc.
        /// Es werden folgende Eingaben ignoriert:
        /// </summary>
        /// <returns>Die Nutzereingabe.</returns>
        private static string ReadLine(string prompt, ConsolePromptOptions options)
        {
            ConsoleHelper.Write(prompt, options);

            if (!options.Hidden)
            {
                return Console.ReadLine() ?? string.Empty;
            }

            var sb = new StringBuilder();
            var run = true;

            while (run)
            {
                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.Backspace:
                        if (sb.Length > 0)
                        {
                            sb.Remove(sb.Length - 1, 1);
                        }
                        break;
                    case ConsoleKey.Enter:
                        run = false;
                        break;
                    default:
                        sb.Append(key.KeyChar);
                        break;
                }
            }

            Console.WriteLine();

            return sb.ToString();
        }
    }
}
