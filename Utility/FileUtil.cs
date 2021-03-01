namespace Utility
{
    using System.IO;

    /// <summary>
    /// Stellt Methoden zum Erstellen von Dateien bereit.
    /// </summary>
    public static class FileUtil
    {
        /// <summary>
        /// Erzeugt eine neue Datei mit dem Pfad <paramref name="path"/>. 
        /// Erstellt auch alle übergeordneten Verzeichnisse.
        /// </summary>
        /// <param name="path">Der Pfad zur Datei.</param>
        public static void CreateFile(string path)
        {
            CreateSubDirs(Path.GetDirectoryName(path));
            File.Create(path);
        }

        /// <summary>
        /// Erstellt rekursiv alle Verzeichnisse bis alle Verzeichnisse im Pfad <paramref name="path"/> existieren.
        /// </summary>
        /// <param name="path">Der zu erstellende Pfad.</param>
        private static void CreateSubDirs(string path)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                CreateSubDirs(Path.GetDirectoryName(path));
            }

            Directory.CreateDirectory(path);
        }

    }
}
