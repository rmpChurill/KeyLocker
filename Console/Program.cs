namespace KeyLocker.Console
{
    using System;

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
            new ConsoleCore().Run();
        }
    }
}
