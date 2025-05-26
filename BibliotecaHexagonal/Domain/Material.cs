namespace BibliotecaHexagonal.Domain;

public class Material
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public DateTime FechaRegistro { get; set; }
    public int CantidadRegistrada { get; set; }
    public int CantidadActual { get; set; }
} 