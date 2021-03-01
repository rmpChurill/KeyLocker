namespace KeyLocker.Console
{
    using System;

    /// <summary>
    /// Hauptklasse des Konsolenprogramms.
    /// </summary>
    public class Program
    {
        private const string CommandQuit = "quit";
        private const string CommandQuitShort = "q";

        private const string CommandHelp = "help";
        private const string CommandHelpShort = "help";

        /// <summary>
        /// Haupteinsprungspunkt des Konsolenprogramms.
        /// </summary>
        /// <param name="args">Die Aufrufparameter.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine("KeyLocker V0.0.1");

            if (args.Length != 0)
            {
                Console.WriteLine("Sorry, Arguments are not yet supported :(");
            }

            var quit = false;

            while (!quit)
            {
                var command = ConsoleHelper.Prompt();

                switch (command.ToLowerInvariant())
                {
                    case CommandQuit:
                    case CommandQuitShort:
                        quit = true;
                        break;
                }
            }
        }

        /// <summary>
        /// Zeigt die Hilfe an.
        /// </summary>
        private static void DisplayHelp()
        {
            Console.Write("help:" +
                "" +
                "" +
                "");
        }
    }
}
