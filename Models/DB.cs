using System.Data.SqlClient;
using Dapper;

public class DB
{
    private static string _connectionString = @"Server=localhost;DartaBase=Uni;Trusted_Connection=True;";
    public static void RegistroEst(string pnombre, string papellido, string pfoto, string pusuario, DateOnly pfnac, string pmail, string pbio, int @pcursada)
    {
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "INSERT INTO Estudiantes (Nombre, Apellido, Foto, NombreUsuario, FechaNac, Mail, Bio, Cursada) VALUES (@pnombre, @papellido, @pfoto, p@usuario, @pfnac, @pmail, @pbio, @pcursada)";
            db.Execute(sql, new{sql, pnombre = Estudiantes.Nombre, papellido,pfoto,pusuario,pfnac,pmail,pbio, pcursada});
        }
    }
}