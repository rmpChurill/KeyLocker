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
            var helpCommand = new HelpCommand();
            var basicState = new BasicState(new ICommand[] { helpCommand });

            core.AllCommands = new ICommand[] { helpCommand };

            core.PushState(basicState);
            core.Run();
        }
    }
}
