using BibliotecaHexagonal.Domain;
using BibliotecaHexagonal.Domain.Interfaces;

namespace BibliotecaHexagonal.Application.UseCases;

public class RegistrarPrestamoCasoUso
{
    private readonly IRepositorioMaterial _repositorioMaterial;
    private readonly IRepositorioPersona _repositorioPersona;
    private readonly IRepositorioMovimiento _repositorioMovimiento;
    public RegistrarPrestamoCasoUso(IRepositorioMaterial repositorioMaterial, IRepositorioPersona repositorioPersona, IRepositorioMovimiento repositorioMovimiento)
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
        if (material.CantidadActual < 1) return "No hay unidades disponibles";
        var movimientos = await _repositorioMovimiento.ObtenerPorPersonaIdAsync(persona.Id);
        int prestados = movimientos.Count(m => m.Tipo == TipoMovimiento.Prestamo &&
            !movimientos.Any(d => d.Tipo == TipoMovimiento.Devolucion && d.MaterialId == m.MaterialId && d.Fecha > m.Fecha));
        int limite = persona.Rol switch
        {
            Rol.Estudiante => 5,
            Rol.Profesor => 3,
            Rol.Administrativo => 1,
            _ => 1
        };
        if (prestados >= limite) return $"Límite de préstamos alcanzado ({limite})";
        material.CantidadActual--;
        await _repositorioMaterial.ActualizarAsync(material);
        await _repositorioMovimiento.AgregarAsync(new Movimiento
        {
            MaterialId = material.Id,
            PersonaId = persona.Id,
            Tipo = TipoMovimiento.Prestamo,
            Fecha = DateTime.Now
        });
        return "Préstamo registrado";
    }
} 