namespace KeyLocker.Console
{
    /// <summary>
    /// Hauptklasse des Konsolenprogramms.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Haupteinsprungspunkt des Konsolenprogramms.
        /// </summary>
        public static void Main(string[] args)
        {
            new ConsoleCore(args).Run();
        }
    }
}
