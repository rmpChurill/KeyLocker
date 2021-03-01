namespace KeyLocker
{
    /// <summary>
    /// Verschiedene Arten der Benutzung von Zeichengruppen.
    /// </summary>
    public enum Usage
    {
        /// <summary>
        /// Zeichengruppe ist erlaubt, aber optional.
        /// </summary>
        Allow,

        /// <summary>
        /// Zeichengruppe ist nicht erlaubt, keines der Zeichen darf enthalten sein.
        /// </summary>
        Forbid,

        /// <summary>
        /// Zeichengruppe ist benötigt, mindestens eines der Zeichen muss enthalten sein.
        /// </summary>
        Require,
    }
}
