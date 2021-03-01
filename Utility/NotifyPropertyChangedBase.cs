namespace KeyLocker.Utility
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Basisklasse für Objekte, die eine Änderung einer Eigenschaft publizieren wollen.
    /// </summary>
    public class NotifyPropertyChangedBase
    {
        /// <summary>
        /// Wird ausgelöst, wenn 
        /// </summary>
        public event EventHandler<PropertyChangedEventArgs> PropertyChanged;

        /// <summary>
        /// Löst <see cref="PropertyChanged"/> aus.
        /// </summary>
        /// <param name="propertyName">Der Name der geänderten Eigenschaft.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs() { PropertyName = propertyName });
        }
    }
}