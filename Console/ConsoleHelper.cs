namespace KeyLocker.Console
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Eine Hilfsklasse für Ein- und Ausgabe auf der Konsole.
    /// </summary>
    public static class ConsoleHelper
    {
        /// <summary>
        /// Beginnt eine neue Zeile, schreibt <paramref name="question"/> (Standardwert ist "> "), liest eine Zeile der Nutzereingabe und gibt diese zurück.
        /// </summary>
        /// <param name="question">Der Text, der vor der Eingabe angezeigt werden soll.</param>
        /// <returns>Die Nutzereingabe.</returns>
        public static string Prompt(string question = "> ")
        {
            Console.WriteLine();
            Console.Write(question);

            return Console.ReadLine() ?? string.Empty;
        }

        /// <summary>
        /// Beginnt eine neue Zeile, schreibt <paramref name="question"/> (Standardwert ist "> "), liest eine Zeile der Nutzereingabe, wobei 
        /// die Eingabe abgefangen wird, sodass die Eingabe nicht angezeigt wird und gibt diese zurück.
        /// </summary>
        /// <param name="question">Der Text, der vor der Eingabe angezeigt werden soll.</param>
        /// <returns>Die Nutzereingabe.</returns>
        public static string HiddenPrompt(string question = "> ")
        {
            Console.WriteLine();
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
