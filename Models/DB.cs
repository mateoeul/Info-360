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
        string sql = "INSERT INTO Universidades (Nombre, Foto, Ubicacion, Tipo, Ubicacion, Descripcion, Valoracion) VALUES (@pnombre, @pfoto, @pubi, @ptipo, @pdescripcion @pValoracion)";
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            db.Execute(sql, new{pnombre = universidad.Nombre, pfoto = universidad.Foto, pubi = universidad.Ubicación, ptipo = universidad.Tipo, pdescripcion = universidad.Descripcion, pvaloracion = universidad.Valoración|});
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
    public List<Universidades> Busqueda(string pDatoIng, Busqueda pbusqueda) 
    {
        List<ResultadoBusqueda> resultados = new List<ResultadoBusqueda>();
        if(pbusqueda.Valoraciones == null)
        busqueda.Valoraciones = 0;
        
        if(busqueda.Tipo == todo)
        {
            using(SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Universidades WHERE Nombre LIKE @pDatoIng%";
                resultados.Universidadesr = db.Query<Universidades>(sql, new{pDatoIng = universidades.Nombre}).ToList();
            }        
            using(SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Carreras WHERE Nombre LIKE @pDatoIng%";
                resultados.Carrerasr = db.Query<Carreras>(sql, new{pDatoIng = carreras.Nombre}).ToList();
            }          
            if (resultados.Carrerasr.Count == 0 && resultados.Universidadesr.Count == 0)
            return null;
            else 
            return resultados;
        }
        else if(busqueda.Tipo == carrera)
        {
            using(SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql = "SELECT Id FROM Carreras WHERE Nombre LIKE @pDatoIng%" 
                resultados.Carrerasr = db.Query<Carreras>(sql, new{pId = carrera.Id}).ToList();
            }
        }
        else if (busqueda.Tipo == uni && busqueda.TipoUni == null )
        {
            using(SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql = "SELECT Id FROM Universidades WHERE Nombre LIKE @pDatoIng% AND Valoraciones >= @pbusqueda.Valoraciones";
                resultados.Universidadesr = db.Query<Universidades>(sql, new{pDatoIng = universidades.Nombre, ptipo = universidades.Tipo,}).ToList();
            }
            if (resultados.Universidadesr.Count == 0)
            return null;
            else 
            return resultados;
        }
        else if(busqueda.Tipo == uni && busqueda.TipoUni == publica)
        {
            using(SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql = "SELECT Id FROM Universidades WHERE Nombre LIKE @pDatoIng% AND Tipo = @pbusqueda.Tipo AND Valoraciones >= @pbusqueda.Valoraciones";
                resultados.Universidadesr = db.Query<Universidades>(sql, new{pDatoIng = universidades.Nombre, ptipo = universidades.Tipo,}).ToList();
            }
            if (resultados.Universidadesr.Count == 0)
            return null;
            else 
            return resultados;
        }
        
        
    }
    public bool VerificarLogin(string pmail, string pcontraseña)
    {
        Usuarios usuario = null;
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT Mail, Contraseña FROM Usuarios WHERE Mail = @pmail AND Contraseña = @pcontraseña";
            usuario = db.Query<Usuarios>(sql, new{pmail = usuario.Mail, pcontraseña = usuario.Contraseña});
        }
        if(usuario != null)
        return true;
        else
        return false;
    }

}

