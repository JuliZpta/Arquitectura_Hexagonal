using BibliotecaHexagonal.Domain.Interfaces;

namespace BibliotecaHexagonal.Application.UseCases;

public class IncrementarCantidadMaterialCasoUso
{
    private readonly IRepositorioMaterial _repositorioMaterial;
    public IncrementarCantidadMaterialCasoUso(IRepositorioMaterial repositorioMaterial)
    {
        _repositorioMaterial = repositorioMaterial;
    }

    public async Task<bool> EjecutarAsync(string titulo, int cantidad)
    {
        var material = await _repositorioMaterial.ObtenerPorTituloAsync(titulo);
        if (material == null) return false;
        material.CantidadRegistrada += cantidad;
        material.CantidadActual += cantidad;
        await _repositorioMaterial.ActualizarAsync(material);
        return true;
    }
} 