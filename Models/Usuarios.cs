using System.Text.Json;

public class Usuarios
{

    [Serializable]
    public int Id {get; set;}
    public char Tipo {get; set;}
    public string Mail {get; set;}
    public string Contraseña {get; set;}
    public int IdExterno {get; set;}

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

    public static Usuarios? FromString(string? json)
    {
        if (json is null)
        {
            return null;
        }
        return JsonSerializer.Deserialize<Usuario>(json);
    }
}
