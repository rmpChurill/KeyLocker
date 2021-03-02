namespace KeyLocker.Console.States
{
    using System.Collections.Generic;

    using KeyLocker.Console.Commands;

    /// <summary>
    /// Der Grundzustand des Programms.
    /// </summary>
    public class BasicState : State
    {
        protected override IEnumerable<ICommand> AllowedCommands
        {
            get
            {
                yield return KnownCommands.HelpCommand;
                yield return KnownCommands.NewEntryCommand;
            }
        }
    }
}