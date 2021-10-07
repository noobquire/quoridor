using System.Collections.Generic;

namespace QuoridorGame.Model
{
    /// <summary>
    /// Utility with miscellaneous extension methods. 
    /// </summary>
    public static class UtilityExtensions
    {
        /// <summary>
        /// Converts two-dimensional array to single-dimensional enumeration.
        /// </summary>
        /// <typeparam name="T">Type of array items.</typeparam>
        /// <param name="matrix">Two-dimensional array.</param>
        /// <returns>Enumeration of array items.</returns>
        public static IEnumerable<T> Flatten<T>(this T[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    yield return matrix[i, j];
                }
            }
        }
    }
}
