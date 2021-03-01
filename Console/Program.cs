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
            Console.WriteLine("KeyLocker V0.0.1");

            if (args.Length != 0)
            {
                Console.WriteLine("Sorry, Arguments are not yet supported :(");
            }


        }
    }
}
