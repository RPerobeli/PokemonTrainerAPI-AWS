using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonTrainerAPIAWS.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetUrlProducao(this IConfiguration config)
        {
            return config.GetValue<string>("UrlAws:Prod");
        }
    }
}
