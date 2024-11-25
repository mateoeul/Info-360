public class Estudiantes
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Foto { get; set; }
    public string? Apellido { get; set; }

    // Aseguramos que FechaNac pueda manejar valores nulos
    public DateOnly? FechaNac { get; set; }

    // Carrera y Cursada ahora permiten valores nulos
    public string? Carrera { get; set; }
    public string? Cursada { get; set; }

    public int IdUsuario { get; set; }

    
}
