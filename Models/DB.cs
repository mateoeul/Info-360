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
    public static Estudiantes MostrarInfoEst(int pId)
    {
        Estudiantes estudiante = null;
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Estudiantes WHERE Id = @pId";
            estudiante = db.Query<Estudiantes>(sql, new{pId = estudiante.Id});
        }
        return estudiante;
    }
    public static void ActualizarInfoEst(int pId, string pnombre, string papellido, string pfoto, string usuario, string pfnac, string pmail, string pbio, string pcursada)
    {
        string sql = "UPDATE Estudiantes SET Nombre = @pnombre, Apellido = @papellido, Foto = @pfoto, NombreUsuario = @pusuario, FechaNac = @pfnac, Mail = @pmail, Bio = @pbio, Cursada = @pcursada WHERE Id = @pId";
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            db.Execute(sql, new{pnombre = estudiante.Nombre, papellido = estudiante.Apellido, pfoto = estudiante.Foto, pusuario = estudiante.NombreUsuario, pfnac = estudiante.FechaNac, pmail = Estudiantes.Mail, pbio = Estudiantes.Bio, pcursada = Estudiantes.Cursada});
        }
    }
    public static Universidades MostrarInfoUni(int pId)
    {
        Universidades universidad = null;
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Universidades WHERE Id = @pId";
            universidad = db.Query<Universidades>(sql, new{pId = universidad.Id});
        }
        return universidad;
    }
    public static List<Carreras> ObtenerCarrerasXNombre(string pNombre)
    {
        List<Carreras> carreras = new List<Carreras>();
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Carreras WHERE Nombre = @pNombre";
            carreras = db.Query<Carreras>(sql, new{pNombre = Carreras.Nombre}).ToList();
        }
        return carreras;
    }
    public List<Preguntas> ObtenerPreguntasTest()
    {
        List<Preguntas> preguntas = new List<Preguntas>();
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Preguntas";
            carreras = db.Query<Preguntas>(sql, new{pNombre = Preguntas.Nombre}).ToList();
        }
        return carreras;
    }

}