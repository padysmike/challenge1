using Microsoft.Extensions.Configuration;
using System;

namespace CodingChalenge1
{
    public static class ConfigLoader
    {
        public static FileConfig LoadConfig()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var section = configurationBuilder.GetSection(nameof(FileConfig));
            var config = section.Get<FileConfig>();
            return config;
        }
    }
}
