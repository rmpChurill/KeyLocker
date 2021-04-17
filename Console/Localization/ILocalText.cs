namespace KeyLocker.Console.Localization
{
    /// <summary>
    /// Implementierende Klassen stellen eine Sammlung von lokalisierten Texten dar.
    /// </summary>
    public interface ILocalText
    {
        /// <summary>
        /// Holt den Text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        string GetText(TextCode text);
    }
}