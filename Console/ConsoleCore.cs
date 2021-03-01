namespace KeyLocker.Console
{
    using System.Collections.Generic;

    using KeyLocker.Console.Commands;
    using KeyLocker.Console.States;

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
            this.AllCommands = new List<ICommand>();
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
        /// Holt eine Auflistung aller registrirten Befehle.
        /// </summary>
        public IEnumerable<ICommand> AllCommands
        {
            get;
            set;
        }

        /// <summary>
        /// Betritt einen neuen Zustand.
        /// </summary>
        /// <param name="state">Der neue Zustand.</param>
        public void PushState(State state)
        {
            state.OnBegin(this);

            this.StateStack.Push(state);
        }

        /// <summary>
        /// Beendet den letzten Zustand.
        /// </summary>
        public void PopState()
        {
            this.StateStack.Pop().OnEnd(this);
        }

        /// <summary>
        /// Startet die Ausführung des Konsolenprogramms in einer Endlosschleife.
        /// </summary>
        public void Run()
        {
            while (this.StateStack.Count > 0)
            {
                this.StateStack.Peek().OnTick(this);
            }
        }
    }
}
