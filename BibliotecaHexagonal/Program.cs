using BibliotecaHexagonal.Domain;
using BibliotecaHexagonal.Domain.Interfaces;
using BibliotecaHexagonal.Infrastructure.Sqlite;
using BibliotecaHexagonal.Application.UseCases;
using Microsoft.EntityFrameworkCore;

class Program
{
    static async Task Main(string[] args)
    {
        var dbContext = new BibliotecaDbContext();
        await dbContext.Database.MigrateAsync();

        var repositorioMaterial = new RepositorioMaterial(dbContext);
        var repositorioPersona = new RepositorioPersona(dbContext);
        var repositorioMovimiento = new RepositorioMovimiento(dbContext);

        var registrarMaterial = new RegistrarMaterialCasoUso(repositorioMaterial);
        var registrarPersona = new RegistrarPersonaCasoUso(repositorioPersona);
        var eliminarPersona = new EliminarPersonaCasoUso(repositorioPersona, repositorioMovimiento);
        var registrarPrestamo = new RegistrarPrestamoCasoUso(repositorioMaterial, repositorioPersona, repositorioMovimiento);
        var registrarDevolucion = new RegistrarDevolucionCasoUso(repositorioMaterial, repositorioPersona, repositorioMovimiento);
        var incrementarCantidad = new IncrementarCantidadMaterialCasoUso(repositorioMaterial);
        var consultarHistorial = new ConsultarHistorialCasoUso(repositorioMovimiento);

        while (true)
        {
            Console.WriteLine("\n--- Biblioteca ---");
            Console.WriteLine("1. Registrar material");
            Console.WriteLine("2. Registrar persona");
            Console.WriteLine("3. Eliminar persona");
            Console.WriteLine("4. Registrar préstamo");
            Console.WriteLine("5. Registrar devolución");
            Console.WriteLine("6. Incrementar cantidad de material");
            Console.WriteLine("7. Consultar historial");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");
            var opcion = Console.ReadLine();
            if (opcion == "0") break;
            switch (opcion)
            {
                case "1":
                    Console.Write("Título: ");
                    var titulo = Console.ReadLine()!;
                    Console.Write("Cantidad: ");
                    int.TryParse(Console.ReadLine(), out var cantidad);
                    var okMat = await registrarMaterial.EjecutarAsync(titulo, cantidad);
                    Console.WriteLine(okMat ? "Material registrado" : "Ya existe un material con ese título");
                    break;
                case "2":
                    Console.Write("Nombre: ");
                    var nombre = Console.ReadLine()!;
                    Console.Write("Cédula: ");
                    var cedula = Console.ReadLine()!;
                    Console.WriteLine("Rol: 1-Estudiante 2-Profesor 3-Administrativo");
                    var rol = Console.ReadLine();
                    Rol rolEnum = rol switch { "1" => Rol.Estudiante, "2" => Rol.Profesor, _ => Rol.Administrativo };
                    var okPer = await registrarPersona.EjecutarAsync(nombre, cedula, rolEnum);
                    Console.WriteLine(okPer ? "Persona registrada" : "Ya existe una persona con esa cédula");
                    break;
                case "3":
                    Console.Write("Cédula: ");
                    var cedulaDel = Console.ReadLine()!;
                    var okDel = await eliminarPersona.EjecutarAsync(cedulaDel);
                    Console.WriteLine(okDel ? "Persona eliminada" : "No se puede eliminar (no existe o tiene préstamos activos)");
                    break;
                case "4":
                    Console.Write("Cédula: ");
                    var cedulaPrest = Console.ReadLine()!;
                    Console.Write("Título material: ");
                    var tituloPrest = Console.ReadLine()!;
                    var msgPrest = await registrarPrestamo.EjecutarAsync(cedulaPrest, tituloPrest);
                    Console.WriteLine(msgPrest);
                    break;
                case "5":
                    Console.Write("Cédula: ");
                    var cedulaDev = Console.ReadLine()!;
                    Console.Write("Título material: ");
                    var tituloDev = Console.ReadLine()!;
                    var msgDev = await registrarDevolucion.EjecutarAsync(cedulaDev, tituloDev);
                    Console.WriteLine(msgDev);
                    break;
                case "6":
                    Console.Write("Título: ");
                    var tituloInc = Console.ReadLine()!;
                    Console.Write("Cantidad a agregar: ");
                    int.TryParse(Console.ReadLine(), out var cantInc);
                    var okInc = await incrementarCantidad.EjecutarAsync(tituloInc, cantInc);
                    Console.WriteLine(okInc ? "Cantidad incrementada" : "Material no encontrado");
                    break;
                case "7":
                    var historial = await consultarHistorial.EjecutarAsync();
                    foreach (var mov in historial)
                    {
                        Console.WriteLine($"{mov.Fecha:yyyy-MM-dd HH:mm} - {mov.Tipo} - {mov.Material?.Titulo} - {mov.Persona?.Nombre} ({mov.Persona?.Cedula})");
                    }
                    break;
                default:
                    Console.WriteLine("Opción inválida");
                    break;
            }
        }
    }
}
