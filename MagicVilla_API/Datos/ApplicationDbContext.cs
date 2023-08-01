using MagicVilla_API.Modelos;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
            
        }
        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Nombre = "Villa Real",
                    Detalle = "Detalle1",
                    ImagenUrl = "",
                    Ocupantes = 5,
                    MetrosCuadrados = 50,
                    Tarifa = 2000,
                    Amenidad = "",
                    FechaActualizacion = DateTime.Now,
                    FechaCreacion = DateTime.Now
                },
                new Villa()
                {
                    Id = 2,
                    Nombre = "Villa Las Rastras",
                    Detalle = "Detalle2",
                    ImagenUrl = "",
                    Ocupantes = 5,
                    MetrosCuadrados = 70,
                    Tarifa = 3000,
                    Amenidad = "",
                    FechaActualizacion = DateTime.Now,
                    FechaCreacion = DateTime.Now
                }

           );
        }
    }
}
