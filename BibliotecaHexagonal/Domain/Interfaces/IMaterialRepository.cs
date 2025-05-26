using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibliotecaHexagonal.Domain.Interfaces;

public interface IRepositorioMaterial
{
    Task<Material?> ObtenerPorIdAsync(int id);
    Task<Material?> ObtenerPorTituloAsync(string titulo);
    Task<List<Material>> ObtenerTodosAsync();
    Task AgregarAsync(Material material);
    Task ActualizarAsync(Material material);
} 