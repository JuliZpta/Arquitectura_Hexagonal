namespace BibliotecaHexagonal.Domain;

public enum Rol
{
    Estudiante,
    Profesor,
    Administrativo
}

public class Persona
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Cedula { get; set; } = string.Empty;
    public Rol Rol { get; set; }
} 