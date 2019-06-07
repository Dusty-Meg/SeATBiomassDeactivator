using System.IO;
using Microsoft.Extensions.Configuration;

namespace SeATBiomassDeactivator
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);

            IConfigurationRoot configuration = builder.Build();

            Program2 program2 = new Program2();
            program2.Main(configuration.GetConnectionString("Database"));
        }
    }
}
