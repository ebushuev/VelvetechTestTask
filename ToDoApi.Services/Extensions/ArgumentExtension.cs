using System;

namespace TodoApi.Services.Extensions
{
    public static class Argument
    {
        public static void Id(long id)
        {
            if (id < 1)
                throw new ArgumentException($"Parameter ${nameof(id)} is not an Id.");
        }

        public static void NotNull<T>(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException($"Parameter ${typeof(T).Name} is null.");
        }
    }
}
