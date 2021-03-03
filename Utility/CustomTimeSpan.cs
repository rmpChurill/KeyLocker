namespace KeyLocker.Utility
{
    using System;

    /// <summary>
    /// Stellt eine einfache Zeitspanne dar.
    /// </summary>
    public partial class CustomTimeSpan
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="amount">Die Länge der Spanne.</param>
        /// <param name="kind">Die Art der Spanne.</param>
        public CustomTimeSpan(double amount, CustomTimeSpanKind kind)
        {
            this.Amount = amount;
            this.Kind = kind;
        }

        /// <summary>
        /// Holt die Länge der Spanne.
        /// </summary>
        public double Amount
        {
            get;
        }

        /// <summary>
        /// Holt die Art der Spanne.
        /// </summary>
        public CustomTimeSpanKind Kind
        {
            get;
        }

        /// <summary>
        /// Gibt eine Zeitspann zurück, die eine Zeit von 0 beschreibt.
        /// </summary>
        public static CustomTimeSpan Zero
        {
            get
            {
                return new CustomTimeSpan(0, CustomTimeSpanKind.Seconds);
            }
        }

        /// <summary>
        /// Versucht den String <paramref name="s"/> als einfache Zeitspanne im Format [Zahl][s|m|h|d|W|M|Y] zu lesen.
        /// </summary>
        /// <param name="s">Der zu parsende String.</param>
        /// <param name="result">Das Ergebnis des Parsings oder <see cref="Zero"/> bei Fehlschlag.</param>
        /// <returns>True, wenn das Parsing erfolgreich war, sonst false.</returns>
        public static bool TryParse(string s, out CustomTimeSpan result)
        {
            result = Zero;

            if (string.IsNullOrEmpty(s))
            {
                return false;
            }

            var unit = s[^0];

            if (!double.TryParse(s[0..^1], out var amount))
            {
                return false;
            }

            CustomTimeSpanKind kind;

            switch (unit)
            {
                case 's':
                    kind = CustomTimeSpanKind.Seconds;
                    break;
                case 'm':
                    kind = CustomTimeSpanKind.Minutes;
                    break;
                case 'h':
                    kind = CustomTimeSpanKind.Hours;
                    break;
                case 'd':
                    kind = CustomTimeSpanKind.Days;
                    break;
                case 'W':
                    kind = CustomTimeSpanKind.Weeks;
                    break;
                case 'M':
                    kind = CustomTimeSpanKind.Months;
                    break;
                case 'Y':
                    kind = CustomTimeSpanKind.Years;
                    break;
                default:
                    return false;
            }

            result = new CustomTimeSpan(amount, kind);
            return true;
        }

        /// <summary>
        /// Liest den String <paramref name="s"/> als einfache Zeitspanne im Format [Zahl][s|m|h|d|W|M|Y].
        /// </summary>
        /// <param name="s">Der zu parsende String.</param>
        /// <returns>Eine neue Instanz von <see cref="CustomTimeSpan"/>.</returns>
        public static CustomTimeSpan Parse(string s)
        {
            if (TryParse(s, out var res))
            {
                return res;
            }

            throw new FormatException();
        }
    }
}