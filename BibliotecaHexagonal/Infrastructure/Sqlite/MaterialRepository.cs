using BibliotecaHexagonal.Domain;
using BibliotecaHexagonal.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaHexagonal.Infrastructure.Sqlite;

public class RepositorioMaterial : IRepositorioMaterial
{
    private readonly BibliotecaDbContext _contexto;
    public RepositorioMaterial(BibliotecaDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task<Material?> ObtenerPorIdAsync(int id)
        => await _contexto.Materiales.FindAsync(id);

    public async Task<Material?> ObtenerPorTituloAsync(string titulo)
        => await _contexto.Materiales.FirstOrDefaultAsync(m => m.Titulo == titulo);

    public async Task<List<Material>> ObtenerTodosAsync()
        => await _contexto.Materiales.ToListAsync();

    public async Task AgregarAsync(Material material)
    {
        _contexto.Materiales.Add(material);
        await _contexto.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Material material)
    {
        _contexto.Materiales.Update(material);
        await _contexto.SaveChangesAsync();
    }
} 