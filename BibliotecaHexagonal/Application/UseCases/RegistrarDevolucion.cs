using BibliotecaHexagonal.Domain;
using BibliotecaHexagonal.Domain.Interfaces;

namespace BibliotecaHexagonal.Application.UseCases;

public class RegistrarDevolucionCasoUso
{
    private readonly IRepositorioMaterial _repositorioMaterial;
    private readonly IRepositorioPersona _repositorioPersona;
    private readonly IRepositorioMovimiento _repositorioMovimiento;
    public RegistrarDevolucionCasoUso(IRepositorioMaterial repositorioMaterial, IRepositorioPersona repositorioPersona, IRepositorioMovimiento repositorioMovimiento)
    {
        _repositorioMaterial = repositorioMaterial;
        _repositorioPersona = repositorioPersona;
        _repositorioMovimiento = repositorioMovimiento;
    }

    public async Task<string> EjecutarAsync(string cedula, string titulo)
    {
        var persona = await _repositorioPersona.ObtenerPorCedulaAsync(cedula);
        if (persona == null) return "Persona no encontrada";
        var material = await _repositorioMaterial.ObtenerPorTituloAsync(titulo);
        if (material == null) return "Material no encontrado";
        var movimientos = await _repositorioMovimiento.ObtenerPorPersonaIdAsync(persona.Id);
        bool tienePrestamo = movimientos.Any(m => m.Tipo == TipoMovimiento.Prestamo && m.MaterialId == material.Id &&
            !movimientos.Any(d => d.Tipo == TipoMovimiento.Devolucion && d.MaterialId == m.MaterialId && d.Fecha > m.Fecha));
        if (!tienePrestamo) return "No hay préstamo activo de este material para esta persona";
        material.CantidadActual++;
        await _repositorioMaterial.ActualizarAsync(material);
        await _repositorioMovimiento.AgregarAsync(new Movimiento
        {
            MaterialId = material.Id,
            PersonaId = persona.Id,
            Tipo = TipoMovimiento.Devolucion,
            Fecha = DateTime.Now
        });
        return "Devolución registrada";
    }
} 