using System.Data.SqlClient;
using Dapper;

public class DB
{
    private static string _connectionString = @"Server=localhost;DartaBase=Uni;Trusted_Connection=True;";
    public static void RegistroEst(Estudiantes estudiante)
    {
        string sql = "INSERT INTO Estudiantes (Nombre, Apellido, Foto, FechaNac, Bio, Cursada) VALUES (@pnombre, @papellido, @pfoto, @pfnac, @pbio, @pcursada)";
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            db.Execute(sql, new{pnombre = estudiante.Nombre, papellido = estudiante.Apellido, pfoto = estudiante.Foto, pfnac = estudiante.FechaNac, pbio = estudiante.Bio, pcursada = estudiante.Cursada});
        }
        
    }
    public static void RegistroUni(Universidades universidad)
    {
        string sql = "INSERT INTO Universidades (Nombre, Foto, Ubicacion, Tipo, Ubicacion, Descripcion) VALUES (@pnombre, @pfoto, @pubi, @ptipo, @pdescripcion)";
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            db.Execute(sql, new{pnombre = universidad.Nombre, pfoto = universidad.Foto, pubi = universidad.Ubicación, ptipo = universidad.Tipo, pdescripcion = universidad.Descripcion});
        }
        
    }
    public static void RegistroProf(Profesores profesor)
    {
        string sql = "INSERT INTO Estudiantes (Nombre, Apellido, Foto, FechaNac, Bio) VALUES (@pnombre, @papellido, @pfoto, @pfnac, @pbio,)";
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            db.Execute(sql, new{pnombre = profesor.Nombre, papellido = profesor.Apellido, pfoto = profesor.Foto, pfnac = profesor.FechaNac, pbio = profesor.Bio});
        }
    }
    public static void RegistroUsuario(Usuarios usuario)
    {
        string sql = "INSERT INTO Usuario (NombreUsuario, Tipo, Contraseña, Mail, IdExterno) VALUES (@pusuario, @ptipo, @pcontraseña, @pmail, @pidext)";
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            db.Execute(sql, new{usuario.NombreUsuario = pusuario, usuario.Tipo = ptipo, usuario.Contraseña = pcontraseña, usuario.Mail = pmail, usuario.IdExterno = pidext});
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
    public static void ActualizarInfoEst(int pId, string pnombre, string papellido, string pfoto, string pfnac, string pbio, string pcursada)
    {
        string sql = "UPDATE Estudiantes SET Nombre = @pnombre, Apellido = @papellido, Foto = @pfoto, FechaNac = @pfnac, Bio = @pbio, Cursada = @pcursada WHERE Id = @pId";
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            db.Execute(sql, new{pnombre = estudiante.Nombre, papellido = estudiante.Apellido, pfoto = estudiante.Foto, pusuario = estudiante.NombreUsuario, pfnac = estudiante.FechaNac, pbio = estudiante.Bio, pcursada = estudiante.Cursada});
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
    public static Universidades MostrarInfoCarrera(int pId)
    {
        Carreras carrera = null;
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Carreras WHERE Id = @pId";
            carrera = db.Query<Carreras>(sql, new{pId = carrera.Id});
        }
        return carrera;
    }
    public static Becas BecasXUni(int pIdUni)
    {
        List<Becas> becas = new List<Becas>();
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Becas WHERE IdUniversidad = @pIdUni";
            becas = db.Query<Becas>(sql, new{pIdUni = beca.IdUniversidad}).ToList();
        }
        return becas;
    }
    public static Condiciones CondicionesXUni(int pIdUni)
    {
        List<Condiciones> condiciones = new List<Condiciones>();
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Condiciones WHERE IdUniversidad = @pIdUni";
            condiciones = db.Query<Condiciones>(sql, new{pIdUni = condiciones.IdUniversidad}).ToList();
        }
        return condiciones;
    }
    public List<Preguntas> ObtenerPreguntasTest()
    {
        List<Preguntas> preguntas = new List<Preguntas>();
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Preguntas";
            preguntas = db.Query<Preguntas>(sql, new{pNombre = Preguntas.Nombre}).ToList();
        }
        return preguntas;
    }
    public List<Universidades> Busqueda(string pDatoIng) //corregir y crear filtros
    {
        int resultados = 0;
        List<Universidades> universidades = new List<Universidades>();
        List<Carreras> carreras = new List<Carreras>();
        List<Estudiantes> estudiantes = new List<Estudiantes>();
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Universidades WHERE Nombre LIKE @pDatoIng%";
            universidades = db.Query<Preguntas>(sql, new{pDatoIng = universidades.Nombre}).ToList();
        }
        if(universidades.Count() > 0)
        return universidades;
        else
        {
            using(SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Carreras WHERE Nombre LIKE @pDatoIng%";
                carreras = db.Query<Carreras>(sql, new{pDatoIng = carreras.Nombre}).ToList();
            }
        }
        if (carreras.Count() > 0)
        return carreras;
        else
        {
            using(SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Estudiantes WHERE NombreUsuario LIKE @pDatoIng%";
                estudiantes = db.Query<Estudiantes>(sql, new{pDatoIng = Estudiantes.NombreUsuario}).ToList();
            }
        };
        if (estudiantes.Count() > 0)
        return estudiantes;
        else return -1;    
    }
    
}
