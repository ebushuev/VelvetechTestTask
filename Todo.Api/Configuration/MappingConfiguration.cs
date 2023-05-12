using FastExpressionCompiler;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Todo.Api.Configuration
{
    public static class MappingConfiguration
    {
        public static void ConfigureMapping(this IServiceCollection _)
        {
            TypeAdapterConfig.GlobalSettings.Compiler = exp => exp.CompileFast();
        }
    }
}
