using System;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace API.Data
{
    public static class DatabasePreparation
    {
        public static async void PrePopulation(IApplicationBuilder app) {

            using (var serviceScope = app.ApplicationServices.CreateScope()) {
                await SeedDataAsync(serviceScope.ServiceProvider.GetService<EstudosAzureApiDockerContext>());
            }
        }
        private static async Task SeedDataAsync(EstudosAzureApiDockerContext context)
        {
            Console.WriteLine("Aplicando Migrations.... #Rodando atualizações no bando de dados");

            context.Database.Migrate();

            if (! await context.Colours.AnyAsync())
            {
                Console.WriteLine("Adicionando dados na tabela Colour");
                context.Colours.AddRange(
                    new Colour() { ColourName = "Grenn" },
                    new Colour() { ColourName = "Red" },
                    new Colour() { ColourName = "Blue" }
                    );
                context.SaveChanges();
                return;
            }

            Console.WriteLine("Bando preparado....");
        }
    }
    
}
