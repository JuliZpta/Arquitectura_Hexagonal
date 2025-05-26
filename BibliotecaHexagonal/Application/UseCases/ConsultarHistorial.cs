using BibliotecaHexagonal.Domain;
using BibliotecaHexagonal.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibliotecaHexagonal.Application.UseCases;

public class ConsultarHistorialCasoUso
{
    private readonly IRepositorioMovimiento _repositorioMovimiento;
    public ConsultarHistorialCasoUso(IRepositorioMovimiento repositorioMovimiento)
    {
        _repositorioMovimiento = repositorioMovimiento;
    }

    public async Task<List<Movimiento>> EjecutarAsync()
    {
        return await _repositorioMovimiento.ObtenerTodosAsync();
    }
} 