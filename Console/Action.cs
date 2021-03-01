namespace KeyLocker.Console
{
    using System.Collections.Generic;

    /// <summary>
    /// Stellt eine ausführbare Aktion dar. 
    /// Ableitende Klassen stellen Methoden und Eigenschaften zur Identifikation der Aktion bereit.
    /// </summary>
    public abstract class Action : HasParentBase
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="parent">Die <see cref="ConsoleCore"/>-Instanz, die mit dieser Instanz verknüpft ist.</param>
        public Action(ConsoleCore parent) : base(parent)
        {

        }

        /// <summary>
        /// Holt einen Text, der in der Hilfe als Beschreibung angezeigt werden soll.
        /// </summary>
        public abstract string HelpDescritpion
        {
            get;
        }

        /// <summary>
        /// Holt einen Wert, der angibt, ob bei dem Vergleich einer Nutzereingabe mit <see cref="Command"/> oder 
        /// einem Element aus <see cref="Aliases"/> die Groß-/Kleinschreibung beachtet werden soll oder nicht.
        /// </summary>
        public abstract bool IsCaseSensitive
        {
            get;
        }

        /// <summary>
        /// Holt den Befehl, der mit dieser Aktion verknüpft werden soll.
        /// </summary>
        public abstract string Command
        {
            get;
        }

        /// <summary>
        /// Holt eine Auflistung von alternativen Befehlen für diese Aktion.
        /// </summary>
        public abstract IEnumerable<string>? Aliases
        {
            get;
        }

        /// <summary>
        /// Führt die Aktion aus.
        /// </summary>
        /// <param name="arg">Der Teil der Nutzereingabe, der dem Namen des Befehls (oder einem Alias) folgte.</param>
        public abstract void Execute(string arg);
    }
}
