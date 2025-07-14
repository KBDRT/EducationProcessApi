using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.IntegrationTests.Database
{
    public static class TestDBHelper
    {

        public static DbContextOptions<ApplicationContext> GetContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                                    .AddEntityFrameworkInMemoryDatabase()
                                    .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("InMemoryDb")
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }



    }
}
