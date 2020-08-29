using System;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class EstudosAzureApiDockerContext : DbContext
    {
        public EstudosAzureApiDockerContext(DbContextOptions<EstudosAzureApiDockerContext> options) : base(options)
        {

        }

        public DbSet<Colour> Colours { get; set; }
    }
}
