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
        public static async void PrePopulation(IApplicationBuilder app)
        {

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                await SeedDataAsync(serviceScope.ServiceProvider.GetService<EstudosAzureApiDockerContext>());
            }
        }
        private static async Task SeedDataAsync(EstudosAzureApiDockerContext context)
        {
            Console.WriteLine("Aplicando Migrations.... #Rodando atualizações no bando de dados");

            context.Database.Migrate();

            if (!await context.Colours.AnyAsync())
            {
                Console.WriteLine("Adicionando dados na tabela Colour");
                context.Colours.AddRange(
                    new Colour() { ColourName = "Green" },
                    new Colour() { ColourName = "Red" },
                    new Colour() { ColourName = "Blue" },
                    new Colour() { ColourName = "Yellow" },
                    new Colour() { ColourName = "Purple" },
                    new Colour() { ColourName = "Orange" }
                    );
                await context.SaveChangesAsync();
                Console.WriteLine("Dados adiconado com sucesso.... #Rodando atualizações no bando de dados ... Finalizado");
                return;
            }

            Console.WriteLine("Banco de Dados preparado para uso....");
        }
    }

}
