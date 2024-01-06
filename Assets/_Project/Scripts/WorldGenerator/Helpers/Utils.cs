using System;

namespace MatchaIsSpent.WorldGeneration
{
    /// <summary>
    /// This class contains utility methods.
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// This method clears the console.
        /// </summary>
        public static void ClearConsole()
        {
            var logEntries = Type.GetType("UnityEditor.LogEntries,UnityEditor.dll");
            var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            clearMethod.Invoke(null, null);
        }
    }
}
