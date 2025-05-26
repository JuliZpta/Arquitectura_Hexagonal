using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibliotecaHexagonal.Domain.Interfaces;

public interface IRepositorioMovimiento
{
    Task<List<Movimiento>> ObtenerTodosAsync();
    Task AgregarAsync(Movimiento movimiento);
    Task<List<Movimiento>> ObtenerPorPersonaIdAsync(int personaId);
    Task<List<Movimiento>> ObtenerPorMaterialIdAsync(int materialId);
} 