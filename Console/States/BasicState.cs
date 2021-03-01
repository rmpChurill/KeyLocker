namespace KeyLocker.Console.States
{
    using System.Collections.Generic;

    using KeyLocker.Console.Commands;

    /// <summary>
    /// Der Grundzustand des Programms.
    /// </summary>
    public class BasicState : State
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="allowedCommands">Die in diesem Zustand ausführbaren Befehle.</param>
        public BasicState(IEnumerable<ICommand> allowedCommands) : base(allowedCommands)
        {
        }
    }
}