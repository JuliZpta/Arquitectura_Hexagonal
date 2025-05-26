using Microsoft.EntityFrameworkCore;
using BibliotecaHexagonal.Domain;

namespace BibliotecaHexagonal.Infrastructure.Sqlite;

public class BibliotecaDbContext : DbContext
{
    public DbSet<Material> Materiales { get; set; }
    public DbSet<Persona> Personas { get; set; }
    public DbSet<Movimiento> Movimientos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=biblioteca.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Material>()
            .HasIndex(m => m.Titulo)
            .IsUnique();
        modelBuilder.Entity<Persona>()
            .HasIndex(p => p.Cedula)
            .IsUnique();
    }
} 