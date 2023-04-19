using System;

namespace TodoApiDto.Shared.Helpers
{
    public static class ObjectHelper
    {
        /// <summary>
        /// Throwing an <see cref="ArgumentNullException"/> if the <paramref name="obj"/> is null
        /// </summary>
        /// <typeparam name="T">Argument type</typeparam>
        /// <param name="obj">Argument</param>
        /// <param name="paramName">Argument name</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static T ThrowIfNull<T>(this T obj, string paramName) where T : class
            => obj ?? throw new ArgumentNullException(paramName);
    }
}