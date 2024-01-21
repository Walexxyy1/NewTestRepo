using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.DATALAYER.Helper
{
    public class ConfigHelper
    {
        public string GetConnectionString()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            var root = builder.Build();
            var sampleConnectionString = root.GetConnectionString("DefaultConnection");
            return sampleConnectionString;
        }
        public string GetCardConnectionString()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            var root = builder.Build();
            var sampleConnectionString = root.GetConnectionString("CardConnection");
            return sampleConnectionString;
        }
    }
}

