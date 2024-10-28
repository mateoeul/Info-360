using System.Data.SqlClient;
using Dapper;

public class DB
{
    private static string _connectionString = @"Server=localhost;DartaBase=Uni;Trusted_Connection=True;";
    public static void RegistroEst(Estudiantes estudiante)
    {
        string sql = "INSERT INTO Estudiantes (Nombre, Apellido, Foto, NombreUsuario, FechaNac, Mail, Bio, Cursada) VALUES (@pnombre, @papellido, @pfoto, p@usuario, @pfnac, @pmail, @pbio, @pcursada)";
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            db.Execute(sql, new{pnombre = estudiante.Nombre, papellido = estudiante.Apellido, pfoto = estudiante.Foto, pusuario = estudiante.NombreUsuario, pfnac = estudiante.FechaNac, pmail = Estudiantes.Mail, pbio = Estudiantes.Bio, pcursada = Estudiantes.Cursada});
        }
        
    }
    public static Estudiantes MostrarInfoEst(int pid)
    {
        Estudiantes estudiante = null;
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Estudiantes WHERE Id = @pId";
            estudiante = db.Query<Estudiantes>(sql, new{pId = estudiante.Id});
        }
        return estudiante;
    }
    public static Universidades MostrarInfoUni(int pid)
    {
        Universidades universidad = null;
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Universidades WHERE Id = @pId";
            universidad = db.Query<Universidades>(sql, new{pId = universidad.Id});
        }
        return universidad;
    }
}