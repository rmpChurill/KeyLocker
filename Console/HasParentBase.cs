namespace KeyLocker.Console
{
    using System.Collections.Generic;

    /// <summary>
    /// Die Basisklasse für alle Klassen, die mit einer Instanz von <see cref="ConsoleCore"/> verknüpft sein sollen.
    /// </summary>
    public class HasParentBase
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="parent">Die <see cref="ConsoleCore"/>-Instanz, die mit dieser Instanz verknüpft ist.</param>
        public HasParentBase(ConsoleCore parent)
        {
            this.Parent = parent;
        }

        /// <summary>
        /// Holt die <see cref="ConsoleCore"/>-Instanz, die mit dieser Instanz verknüpft ist.
        /// </summary>
        protected ConsoleCore Parent
        {
            get;
        }
    }
}
