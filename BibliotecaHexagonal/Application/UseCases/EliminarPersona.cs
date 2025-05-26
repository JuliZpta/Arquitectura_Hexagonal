using BibliotecaHexagonal.Domain.Interfaces;

namespace BibliotecaHexagonal.Application.UseCases;

public class EliminarPersonaCasoUso
{
    private readonly IRepositorioPersona _repositorioPersona;
    private readonly IRepositorioMovimiento _repositorioMovimiento;
    public EliminarPersonaCasoUso(IRepositorioPersona repositorioPersona, IRepositorioMovimiento repositorioMovimiento)
    {
        _repositorioPersona = repositorioPersona;
        _repositorioMovimiento = repositorioMovimiento;
    }

    public async Task<bool> EjecutarAsync(string cedula)
    {
        var persona = await _repositorioPersona.ObtenerPorCedulaAsync(cedula);
        if (persona == null) return false;
        var movimientos = await _repositorioMovimiento.ObtenerPorPersonaIdAsync(persona.Id);
        bool tienePrestamos = movimientos.Any(m => m.Tipo == Domain.TipoMovimiento.Prestamo &&
            !movimientos.Any(d => d.Tipo == Domain.TipoMovimiento.Devolucion && d.MaterialId == m.MaterialId && d.Fecha > m.Fecha));
        if (tienePrestamos) return false;
        await _repositorioPersona.EliminarAsync(persona);
        return true;
    }
} 