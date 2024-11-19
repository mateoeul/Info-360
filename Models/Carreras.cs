public class Carreras
{
public class Carreras
{
    public int Id { get; set; }
    public int IdUniversidad { get; set; }
    public string? Nombre { get; set; }
    public int Duracion { get; set; }
    public int Costo { get; set; }
    public string? Foto { get; set; }

    // Nueva propiedad para asociar la universidad
    public Universidades Universidad { get; set; }
}
}