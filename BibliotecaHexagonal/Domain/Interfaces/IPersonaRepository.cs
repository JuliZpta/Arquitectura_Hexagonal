using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibliotecaHexagonal.Domain.Interfaces;

public interface IRepositorioPersona
{
    Task<Persona?> ObtenerPorIdAsync(int id);
    Task<Persona?> ObtenerPorCedulaAsync(string cedula);
    Task<List<Persona>> ObtenerTodosAsync();
    Task AgregarAsync(Persona persona);
    Task ActualizarAsync(Persona persona);
    Task EliminarAsync(Persona persona);
} 