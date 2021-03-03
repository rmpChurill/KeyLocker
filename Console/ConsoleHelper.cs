namespace KeyLocker.Console
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using KeyLocker.Console.Validation;

    /// <summary>
    /// Eine Hilfsklasse für Ein- und Ausgabe auf der Konsole.
    /// </summary>
    public static class ConsoleHelper
    {
        /// <summary>
        /// Gibt <paramref name="text"/> aus und ändert dazu die Textfarbe zu <paramref name="foreground"/> und
        /// die Hintergrundfarbe zu <paramref name="background"/>. Nach der Ausgabe werden die Farben zurückgesetzt.
        /// </summary>
        /// <param name="text">Der auszugebende Text.</param>
        /// <param name="foreground">Die zu nutzende Textfarbe.</param>
        /// <param name="background">Die zu nutzende Hintergrundfarbe. Ist der Parameter null wird die Hintegrundfarbe nicht geändert.</param>
        public static void PrintColor(string text, ConsoleColor foreground, ConsoleColor? background)
        {
            var oldFg = Console.ForegroundColor;
            var oldBg = Console.BackgroundColor;

            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background ?? oldBg;

            Console.Write(text);

            Console.ForegroundColor = oldFg;
            Console.BackgroundColor = oldBg;
        }

        /// <summary>
        /// Schreibt <paramref name="question"/> (Standardwert ist "> "), liest eine Zeile der Nutzereingabe und gibt diese zurück.
        /// </summary>
        /// <param name="question">Der Text, der vor der Eingabe angezeigt werden soll.</param>
        /// <returns>Die Nutzereingabe.</returns>
        public static string Prompt(string question = "> ")
        {
            Console.Write(question);

            return Console.ReadLine() ?? string.Empty;
        }

        /// <summary>
        /// Schreibt <paramref name="question"/> (Standardwert ist "> "), liest eine Zeile der Nutzereingabe.
        /// Diese Eingabe wird durch <paramref name="validator"/> validiert und die Eingabe wird solange wiederholt, bis
        /// die Validierung erfolgreich ist.
        /// </summary>
        /// <param name="validator">Der zu nutzende <see cref="IInputValidator"/></param>
        /// <param name="question">Der Text, der vor der Eingabe angezeigt werden soll.</param>
        /// <returns>Die Nutzereingabe.</returns>
        public static string ValidatedPrompt(IInputValidator validator, string question = "> ")
        {
            string? res;

            while (true)
            {
                Console.Write(question);

                res = Console.ReadLine() ?? string.Empty;

                if (validator.IsValid(res))
                {
                    break;
                }
            }

            return res;
        }

        /// <summary>
        /// Schreibt <paramref name="question"/> (Standardwert ist "> "), liest eine Zeile der Nutzereingabe.
        /// Diese Eingabe wird durch <paramref name="validator"/> validiert und die Eingabe wird solange wiederholt, bis
        /// die Validierung erfolgreich ist.
        /// Außerdem sind leere Eingaben erlaubt. Diese werden nicht validiert sondern führen direkt zu einer Rückgabe von null.
        /// </summary>
        /// <param name="validator">Der zu nutzende <see cref="IInputValidator"/></param>
        /// <param name="question">Der Text, der vor der Eingabe angezeigt werden soll.</param>
        /// <returns>Die Nutzereingabe oder null bei einer leeren Eingabe.</returns>
        public static string? ValidatedPromptOrEmpty(IInputValidator validator, string question = "> ")
        {
            string? res;

            while (true)
            {
                Console.Write(question);

                res = Console.ReadLine();

                if (string.IsNullOrEmpty(res))
                {
                    return null;
                }

                if (validator.IsValid(res))
                {
                    break;
                }
            }

            return res;
        }

        /// <summary>
        /// Schreibt <paramref name="question"/> (Standardwert ist "> ") und liest eine Zeile der Nutzereingabe, 
        /// die als boolescher Wert interpretiert wird.
        /// </summary>
        /// <param name="question">Der Text, der vor der Eingabe angezeigt werden soll.</param>

        /// <returns></returns>
        public static bool PromptBool(string question = "> ")
        {
            var res = ValidatedPrompt(new IsYesNoValidator(), question);

            return res == "y" || res == "Y";
        }

        /// <summary>
        /// Schreibt <paramref name="question"/> (Standardwert ist "> "), liest eine Zeile der Nutzereingabe, wobei 
        /// die Eingabe abgefangen wird, sodass die Eingabe nicht angezeigt wird und gibt diese zurück.
        /// </summary>
        /// <param name="question">Der Text, der vor der Eingabe angezeigt werden soll.</param>
        /// <returns>Die Nutzereingabe.</returns>
        public static string HiddenPrompt(string question = "> ")
        {
            Console.Write(question);

            return HiddenReadLine();
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
        /// Liest eine Zeile der Nutzereingabe ein, ohne die Zeichen im Terminal anzuzeigen.
        /// </summary>
        /// <returns>Die Nutzereingabe.</returns>
        private static string HiddenReadLine()
        {
            var sb = new StringBuilder();
            var key = Console.ReadKey(true);

            while (key.Key != ConsoleKey.Enter)
            {
                sb.Append(key.KeyChar);

                key = Console.ReadKey(true);
            }

            return sb.ToString();
        }
    }
}
