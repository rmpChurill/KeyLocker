namespace KeyLocker.Console
{
    using System.Collections.Generic;

    /// <summary>
    /// Stellt den Kern der Anwendung dar.
    /// </summary>
    public class ConsoleCore
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse.
        /// </summary>
        public ConsoleCore()
        {
            this.StateStack = new Stack<State>();
        }

        /// <summary>
        /// Holt einen Stack, der eine Reihe von Zuständen speichert.
        /// Der oberste Eintrag ist der aktuelle Zustand.
        /// </summary>
        public Stack<State> StateStack
        {
            get;
        }

        /// <summary>
        /// Startet die Ausführung des Konsolenprogramms in einer Endlosschleife.
        /// </summary>
        public void Run()
        {

        }
    }
}
