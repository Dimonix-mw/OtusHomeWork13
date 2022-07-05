using Microsoft.Extensions.DependencyInjection;
using OtusHomeWork13.Infrastructure.Services;

namespace OtusHomeWork13
{
    static class Program {
        
        static void Main(string[] args)
        {
            var provider = Configure();
            var programService = provider.GetService<IProgramService>();
            programService.Start();
        }

        private static IServiceProvider Configure()
        {
            var services = new ServiceCollection();
            services.AddScoped<IProgramService, ProgramService>();
            return services.BuildServiceProvider();
        }

    }
}