using LaTiendaIS.Shared.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTiendaIs.SpecFlow.Server
{
    public class ServidorHelper
    {
        public static string Hostname { get; private set; }
        public static HttpClient ArrancarServidor()
        {
            Hostname = $"http://localhost:4568";
            //var factory = new Factory<> ;
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<DBLaTiendaContext>));

                        services.Remove(descriptor);

                        services.AddDbContext<DBLaTiendaContext>(options =>
                        {
                            options.UseInMemoryDatabase("InMemoryDbForTesting");
                        });

                        var sp = services.BuildServiceProvider();

                        using (var scope = sp.CreateScope())
                        {
                            var scopedServices = scope.ServiceProvider;
                            var db = scopedServices.GetRequiredService<DBLaTiendaContext>();

                            db.Database.EnsureCreated();

                            try
                            {
                                //db.Database.Migrate();
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                        }
                    });
                });
            var client = application.CreateClient();
            return client;
        }
    }
}
