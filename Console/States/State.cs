namespace KeyLocker.Console.States
{
    using System;
    using System.Collections.Generic;

    using KeyLocker.Console.Commands;

    /// <summary>
    /// Stellt einen Zustand des Programms dar.
    /// Implementierende Klassen stellen Methoden und Eigenschaften zur Identifikation der Zustände und 
    /// der Behandlung von Übergängen dar.
    /// </summary>
    public abstract class State
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="allowedCommands">Die in diesem Zustand ausführbaren Befehle.</param>
        public State(IEnumerable<ICommand> allowedCommands)
        {
            this.AllowedCommands = new List<ICommand>(allowedCommands);
        }

        /// <summary>
        /// Holt eine Auflistung von Befehlen, die in diesem Zustand ausgeführt werden können.
        /// </summary>
        protected IEnumerable<ICommand> AllowedCommands
        {
            get;
        }

        /// <summary>
        /// Behandelt die Eingabe eines Nutzers.
        /// </summary>
        /// <param name="core">Die ausführende <see cref="ConsoleCore"/>-Instanz.</param>
        /// <param name="input">Die Nutzereingabe.</param>
        public void HandleUserInput(ConsoleCore core, string input)
        {
            var i = 0;

            while (i < input.Length && !char.IsWhiteSpace(input[i])) { i++; }

            var command = input[0..i];
            var actionToRun = default(ICommand);

            foreach (var action in this.AllowedCommands)
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
                    var arg = input[i..];

                    if (this.OnActionRun(core, actionToRun, arg))
                    {
                        actionToRun.Execute(core, arg);
                    }
                }
                else
                {
                    this.OnActionNotFound(core, input);
                }
            }
        }

        /// <summary>
        /// Wird aufgerufen wenn für eine Eingabe keine Aktion gefunden wurden.
        /// </summary>
        /// <param name="core">Die ausführende <see cref="ConsoleCore"/>-Instanz.</param>
        /// <param name="input">Die Nutzereingabe.</param>
        public virtual void OnActionNotFound(ConsoleCore core, string input)
        {
            Console.WriteLine($"\"{input}\" is no valid command!");
        }

        /// <summary>
        /// Wird aufgerufen wenn eine Aktion begonnen wird.
        /// </summary>
        /// <param name="core">Die ausführende <see cref="ConsoleCore"/>-Instanz.</param>
        /// <param name="action">Die Aktion, die als nächstes ausgeführt wird.</param>
        /// <param name="argument">Die Eingabe für diese Aktion.</param>
        /// <returns>True, wenn die Aktion ausgeführt soll, sonst false.</returns>
        public virtual bool OnActionRun(ConsoleCore core, ICommand action, string argument)
        {
            return true;
        }

        /// <summary>
        /// Wird aufgerufen wenn der Zustand begonnen wird, also wenn die Instanz zu <see cref="ConsoleCore.StateStack"/> hinzugefügt wird.
        /// </summary>
        /// <param name="core">Die ausführende <see cref="ConsoleCore"/>-Instanz.</param>
        public virtual void OnBegin(ConsoleCore core)
        {
        }

        /// <summary>
        /// Behandelt einen Ausführungsschritt.
        /// </summary>
        /// <param name="core">Die ausführende <see cref="ConsoleCore"/>-Instanz.</param>
        public virtual void OnTick(ConsoleCore core)
        {
            var input = ConsoleHelper.Prompt();

            this.HandleUserInput(core, input);
        }

        /// <summary>
        /// Wird aufgerufen wenn der Zustand beendet wird, also wenn die Instanz von <see cref="ConsoleCore.StateStack"/> entfernt wird.
        /// </summary>
        /// <param name="core">Die ausführende <see cref="ConsoleCore"/>-Instanz.</param>
        public virtual void OnEnd(ConsoleCore core)
        {
        }
    }
}
