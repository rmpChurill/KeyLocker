using System.Collections.Generic;

namespace KeyLocker.Console.States
{
    /// <summary>
    /// Der Grundzustand des Programms.
    /// </summary>
    public class BasicState : State
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="parent">Die <see cref="ConsoleCore"/>-Instanz, die mit dieser Instanz verknüpft ist.</param>
        /// <param name="allowedActions">Die in diesem Zustand erlaubten Aktionen.</param>
        public BasicState(ConsoleCore parent,
                          IEnumerable<Action> allowedActions) : base(parent, allowedActions)
        {
        }
    }
}