using BibliotecaHexagonal.Domain;
using BibliotecaHexagonal.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaHexagonal.Infrastructure.Sqlite;

public class RepositorioMovimiento : IRepositorioMovimiento
{
    private readonly BibliotecaDbContext _contexto;
    public RepositorioMovimiento(BibliotecaDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task<List<Movimiento>> ObtenerTodosAsync()
        => await _contexto.Movimientos.Include(m => m.Material).Include(m => m.Persona).ToListAsync();

    public async Task AgregarAsync(Movimiento movimiento)
    {
        _contexto.Movimientos.Add(movimiento);
        await _contexto.SaveChangesAsync();
    }

    public async Task<List<Movimiento>> ObtenerPorPersonaIdAsync(int personaId)
        => await _contexto.Movimientos.Where(m => m.PersonaId == personaId).Include(m => m.Material).ToListAsync();

    public async Task<List<Movimiento>> ObtenerPorMaterialIdAsync(int materialId)
        => await _contexto.Movimientos.Where(m => m.MaterialId == materialId).Include(m => m.Persona).ToListAsync();
} 