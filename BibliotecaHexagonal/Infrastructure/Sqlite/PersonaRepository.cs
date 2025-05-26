using BibliotecaHexagonal.Domain;
using BibliotecaHexagonal.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaHexagonal.Infrastructure.Sqlite;

public class RepositorioPersona : IRepositorioPersona
{
    private readonly BibliotecaDbContext _contexto;
    public RepositorioPersona(BibliotecaDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task<Persona?> ObtenerPorIdAsync(int id)
        => await _contexto.Personas.FindAsync(id);

    public async Task<Persona?> ObtenerPorCedulaAsync(string cedula)
        => await _contexto.Personas.FirstOrDefaultAsync(p => p.Cedula == cedula);

    public async Task<List<Persona>> ObtenerTodosAsync()
        => await _contexto.Personas.ToListAsync();

    public async Task AgregarAsync(Persona persona)
    {
        _contexto.Personas.Add(persona);
        await _contexto.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Persona persona)
    {
        _contexto.Personas.Update(persona);
        await _contexto.SaveChangesAsync();
    }

    public async Task EliminarAsync(Persona persona)
    {
        _contexto.Personas.Remove(persona);
        await _contexto.SaveChangesAsync();
    }
} 