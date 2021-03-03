﻿namespace KeyLocker.Console.Commands
{
    using System.Collections.Generic;

    /// <summary>
    /// Stellt eine ausführbare Aktion dar. 
    /// Ableitende Klassen stellen Methoden und Eigenschaften zur Identifikation der Aktion bereit.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Holt einen Text, der in der Hilfe als Beschreibung angezeigt werden soll.
        /// </summary>
        public string HelpDescritpion
        {
            get;
        }

        /// <summary>
        /// Holt einen Wert, der angibt, ob bei dem Vergleich einer Nutzereingabe mit <see cref="Command"/> oder 
        /// einem Element aus <see cref="Aliases"/> die Groß-/Kleinschreibung beachtet werden soll oder nicht.
        /// </summary>
        public bool IsCaseSensitive
        {
            get;
        }

        /// <summary>
        /// Holt den Befehl, der mit dieser Aktion verknüpft werden soll.
        /// </summary>
        public string Command
        {
            get;
        }

        /// <summary>
        /// Holt eine Auflistung von alternativen Befehlen für diese Aktion.
        /// </summary>
        public char? Alias
        {
            get;
        }

        /// <summary>
        /// Führt die Aktion aus.
        /// </summary>
        /// <param name="core">Die ausführende <see cref="ConsoleCore"/>-Instanz.</param>
        /// <param name="arg">Der Teil der Nutzereingabe, der dem Namen des Befehls (oder einem Alias) folgte.</param>
        public void Execute(ConsoleCore core, string arg);
    }
}