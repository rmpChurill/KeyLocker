namespace KeyLocker.Console.States
{
    using System.Collections.Generic;

    /// <summary>
    /// Eine Sammlung aller bekannten Zustände.
    /// </summary>
    public static class KnownStates
    {
        private static BasicState BasicState = new BasicState();


        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<State> All
        {
            get
            {

            }
        }
    }
}
