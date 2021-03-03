namespace KeyLocker.Console
{
    using KeyLocker.Console.Commands;
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

            core.PushState(KnownStates.BasicState);
            core.Run();
        }
    }
}
