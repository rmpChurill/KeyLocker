using System;
using System.Collections.Generic;

namespace KeyLocker.Console
{
    /// <summary>
    /// Stellt einen Zustand des Programms dar.
    /// Implementierende Klassen stellen Methoden und Eigenschaften zur Identifikation der Zustände und 
    /// der Behandlung von Übergängen dar.
    /// </summary>
    public abstract class State : HasParentBase
    {
        /// <summary>
        /// Aktionen, die in diesem Zustand erlaubt sind.
        /// </summary>
        private List<Action> allowedActions;

        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="parent">Die <see cref="ConsoleCore"/>-Instanz, die mit dieser Instanz verknüpft ist.</param>
        /// <param name="allowedActions">Die in diesem Zustand erlaubten Aktionen.</param>
        public State(ConsoleCore parent, IEnumerable<Action> allowedActions) : base(parent)
        {
            this.allowedActions = new List<Action>(allowedActions);
        }

        /// <summary>
        /// Behandelt die Eingabe eines Nutzers.
        /// </summary>
        /// <param name="input">Die Nutzereingabe.</param>
        public void HandleUserInput(string input)
        {
            var i = 0;

            while (char.IsWhiteSpace(input[i])) { i++; }

            var command = input.Substring(0, i);
            var actionToRun = default(Action);

            foreach (var action in this.allowedActions)
            {
                var comparison = action.IsCaseSensitive
                               ? StringComparison.Ordinal
                               : StringComparison.OrdinalIgnoreCase;

                if (action.Command.Equals(command, comparison))
                {
                    actionToRun = action;
                    break;
                }

                var aliases = action.Aliases;

                if (aliases != default)
                {
                    foreach (var alias in aliases)
                    {
                        if (alias.Equals(command, comparison))
                        {
                            actionToRun = action;
                        }
                    }
                }

                if (actionToRun != default)
                {
                    var arg = input.Substring(i);

                    this.OnActionRun(actionToRun, arg);

                    actionToRun.Execute(arg);
                }
                else
                {
                    this.OnActionNotFound(input);
                }
            }
        }

        /// <summary>
        /// Wird aufgerufen wenn für eine Eingabe keine Aktion gefunden wurden.
        /// </summary>
        /// <param name="input">Die Nutzereingabe.</param>
        public virtual void OnActionNotFound(string input)
        {
            //// TODO
        }

        /// <summary>
        /// Wird aufgerufen wenn eine Aktion begonnen wird.
        /// </summary>
        /// <param name="action">Die Aktion, die als nächstes ausgeführt wird.</param>
        /// <param name="argument">Die Eingabe für diese Aktion.</param>
        /// <returns>True, wenn die Aktion ausgeführt soll, sonst false.</returns>
        public virtual bool OnActionRun(Action action, string argument)
        {
            return true;
        }

        /// <summary>
        /// Wird aufgerufen wenn der Zustand begonnen wird, also wenn die Instanz zu <see cref="ConsoleCore.StateStack"/> hinzugefügt wird.
        /// </summary>
        public virtual void OnBegin()
        {
        }

        /// <summary>
        /// Wird aufgerufen wenn der Zustand beendet wird, also wenn die Instanz von <see cref="ConsoleCore.StateStack"/> entfernt wird.
        /// </summary>
        public virtual void OnEnd()
        {
        }
    }
}
