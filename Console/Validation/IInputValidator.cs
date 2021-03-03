namespace KeyLocker.Console.Validation
{
    /// <summary>
    /// Implementierende Klassen stellen Methoden bereit, um eine Nutzereingabe zu validieren.
    /// </summary>
    public interface IInputValidator 
    {
        /// <summary>
        /// Validiert eine Nutzereingabe.
        /// </summary>
        /// <param name="userInput">Die zu validierende Eingabe.</param>
        /// <returns>True, wenn die Eingabe gültig ist, sonst false.</returns>
        bool IsValid(string userInput);
    }
}