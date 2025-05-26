using BibliotecaHexagonal.Domain;
using BibliotecaHexagonal.Domain.Interfaces;

namespace BibliotecaHexagonal.Application.UseCases;

public class RegistrarMaterialCasoUso
{
    private readonly IRepositorioMaterial _repositorioMaterial;
    public RegistrarMaterialCasoUso(IRepositorioMaterial repositorioMaterial)
    {
        _repositorioMaterial = repositorioMaterial;
    }

    public async Task<bool> EjecutarAsync(string titulo, int cantidad)
    {
        var existente = await _repositorioMaterial.ObtenerPorTituloAsync(titulo);
        if (existente != null) return false;
        var material = new Material
        {
            Titulo = titulo,
            FechaRegistro = DateTime.Now,
            CantidadRegistrada = cantidad,
            CantidadActual = cantidad
        };
        await _repositorioMaterial.AgregarAsync(material);
        return true;
    }
} 