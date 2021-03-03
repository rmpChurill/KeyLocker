namespace KeyLocker.Console.States
{
    using System.Collections.Generic;

    /// <summary>
    /// Eine Sammlung aller bekannten Zustände.
    /// </summary>
    public static class KnownStates
    {
        /// <summary>
        /// Der Grundzustand der Anwendung.
        /// </summary>
        /// <returns></returns>
        public static readonly BasicState BasicState = new BasicState();

        /// <summary>
        /// Holt alle bekannnte Zustände.
        /// </summary>
        public static IEnumerable<State> All
        {
            get
            {
                yield return BasicState;
            }
        }
    }
}
