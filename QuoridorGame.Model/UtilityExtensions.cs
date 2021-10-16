using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

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

        /// <summary>
        /// Creates a deep clone of a serializable object.
        /// Do not use it with user input data.
        /// </summary>
        /// <typeparam name="T">Type of an object.</typeparam>
        /// <param name="object">Object.</param>
        /// <returns>Deep copy of an object.</returns>
        public static T DeepClone<T>(this T @object)
        {
            using var ms = new MemoryStream();
            var formatter = new BinaryFormatter();
#pragma warning disable SYSLIB0011 // Type or member is obsolete
            // Binary formatter is used here to clone only trusted internal objects.
            // Do not use it with user-provided data.
            formatter.Serialize(ms, @object);
#pragma warning restore SYSLIB0011 // Type or member is obsolete
            ms.Position = 0;

#pragma warning disable SYSLIB0011 // Type or member is obsolete
            return (T)formatter.Deserialize(ms);
#pragma warning restore SYSLIB0011 // Type or member is obsolete
        }
    }
}
