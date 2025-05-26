namespace BibliotecaHexagonal.Domain;

public enum TipoMovimiento
{
    Prestamo,
    Devolucion
}

public class Movimiento
{
    public int Id { get; set; }
    public int MaterialId { get; set; }
    public int PersonaId { get; set; }
    public TipoMovimiento Tipo { get; set; }
    public DateTime Fecha { get; set; }
    public Material? Material { get; set; }
    public Persona? Persona { get; set; }
} 