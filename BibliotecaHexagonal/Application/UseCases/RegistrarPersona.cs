using BibliotecaHexagonal.Domain;
using BibliotecaHexagonal.Domain.Interfaces;

namespace BibliotecaHexagonal.Application.UseCases;

public class RegistrarPersonaCasoUso
{
    private readonly IRepositorioPersona _repositorioPersona;
    public RegistrarPersonaCasoUso(IRepositorioPersona repositorioPersona)
    {
        _repositorioPersona = repositorioPersona;
    }

    public async Task<bool> EjecutarAsync(string nombre, string cedula, Rol rol)
    {
        var existente = await _repositorioPersona.ObtenerPorCedulaAsync(cedula);
        if (existente != null) return false;
        var persona = new Persona
        {
            Nombre = nombre,
            Cedula = cedula,
            Rol = rol
        };
        await _repositorioPersona.AgregarAsync(persona);
        return true;
    }
} 