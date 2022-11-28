using System;
using System.Threading.Tasks;

namespace FunctionalExtensions
{
    public static class FunctionalExtensions
    {
        public static T2 FeedTo<T1, T2>(this T1 input, Func<T1, T2> function) => function(input);

        public static async Task<T2> FeedToAsync<T1, T2>(this Task<T1> input, Func<T1, T2> function) => (await input).FeedTo(function);

        public static void Doing<T>(this T input, Action<T> function) => function(input);
    }
}
