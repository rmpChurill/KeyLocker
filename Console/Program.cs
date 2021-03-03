namespace KeyLocker.Console
{
    using System;
    using KeyLocker.Console.States;

    /// <summary>
    /// Hauptklasse des Konsolenprogramms.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Haupteinsprungspunkt des Konsolenprogramms.
        /// </summary>
        /// <param name="args">Die Aufrufparameter.</param>
        public static void Main(string[] args)
        {
            var core = new ConsoleCore();

            Console.WriteLine("Welcome to KeyLocker\nVersion 0.1");

            core.PushState(KnownStates.BasicState);
            core.Run();
        }
    }
}
