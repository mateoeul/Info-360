using System.Data.SqlClient;
using Dapper;

public class DB
{
    private static string _connectionString = @"Server=localHost;DataBase=Uni;Trusted_Connection=True;";
    /*public static void RegistroUni(Universidades universidad)
    {
        string sql = "INSERT INTO Universidades (Nombre, Foto, Ubicacion, Tipo, Ubicacion, Descripcion, Valoracion) VALUES (@pnombre, @pfoto, @pubi, @ptipo, @pdescripcion, @pidusuario)";
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            db.Execute(sql, new{pnombre = universidad.Nombre, pfoto = universidad.Foto, pubi = universidad.Ubicación, ptipo = universidad.Tipo, pdescripcion = universidad.Descripcion, pidusuario = universidad.IdUsuario});
        }
        
    }
    public static void RegistroProf(Profesores profesor)
    {
        string sql = "INSERT INTO Estudiantes (Nombre, Apellido, Foto, FechaNac, Bio) VALUES (@pnombre, @papellido, @pfoto, @pfnac, @pbio, @pidusuario)";
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            db.Execute(sql, new{pnombre = profesor.Nombre, papellido = profesor.Apellido, pfoto = profesor.Foto, pfnac = profesor.FechaNac, pbio = profesor.Bio, pidusuario = profesor.IdUsuario});
        }
    }*/
    public static void RegistroEst(Estudiantes estudiante)
    {
        string sql = "INSERT INTO Estudiantes (Nombre, Apellido, Foto, FechaNac, Bio, Cursada) VALUES (@pnombre, @papellido, @pfoto, @pfnac, @pbio, @pcursada, @pidusuario)";
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            db.Execute(sql, new{pnombre = estudiante.Nombre, papellido = estudiante.Apellido, pfoto = estudiante.Foto, pfnac = estudiante.FechaNac, pbio = estudiante.Bio, pcursada = estudiante.Cursada, pidusuario = estudiante.IdUsuario});
        }
        
    }
    public static void RegistroUsuario(Usuarios usuario)
    {
        string sql = "INSERT INTO Usuario (NombreUsuario, Tipo, Contraseña, Mail) VALUES (@pusuario, @ptipo, @pcontraseña, @pmail)";
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            db.Execute(sql, new{ptipo = usuario.Tipo, pcontraseña = usuario.Contraseña, pmail = usuario.Mail});
        }
    }  
    public static int ObtenerIdUsuario(string mail)
    {
        int idUsuario = 0;
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT Id FROM Usuarios WHERE Mail = @pmail";
            idUsuario = db.QueryFirstOrDefault<int>(sql, new { pmail = mail });
        }
        return idUsuario;
    }
    public static Estudiantes MostrarInfoEst(int pId)
    {
        Estudiantes estudiante = null;
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Estudiantes WHERE Id = @pId";
            estudiante = db.QueryFirstOrDefault<Estudiantes>(sql, new{pId = estudiante.Id});
        }
        return estudiante;
    }
    public static void ActualizarInfoEst(Estudiantes estudiante, string pnombre, string papellido, string pfoto, string pfnac, string pbio, string pcursada)
    {
        string sql = "UPDATE Estudiantes SET Nombre = @pnombre, Apellido = @papellido, Foto = @pfoto, FechaNac = @pfnac, Bio = @pbio, Cursada = @pcursada WHERE Id = @pId";
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            db.Execute(sql, new{pnombre = estudiante.Nombre, papellido = estudiante.Apellido, pfoto = estudiante.Foto, pfnac = estudiante.FechaNac, pbio = estudiante.Bio, pcursada = estudiante.Cursada});
        }
    }
    public static Universidades MostrarInfoUni(int pId)
    {
        Universidades universidad = new Universidades();
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Universidades WHERE Id = @pId";
            universidad = db.QueryFirstOrDefault<Universidades>(sql, new{pId = universidad.Id});
        }
        return universidad;
    }
    public static Carreras MostrarInfoCarrera(int pId)
    {
        Carreras carrera = null;
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Carreras WHERE Id = @pId";
            carrera = db.QueryFirstOrDefault<Carreras>(sql, new{pId = carrera.Id});
        }
        return carrera;
    }
    public static List<Becas> BecasXUni(int idUni)
    {
        List<Becas> becas = new List<Becas>();
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Becas WHERE IdUniversidad = @pIdUni";
            becas = db.Query<Becas>(sql, new{pIdUni = idUni}).ToList();
        }
        return becas;
    }
    public static List<Condiciones> CondicionesXUni(int idUni)
    {
        List<Condiciones> condiciones = new List<Condiciones>();
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Condiciones WHERE IdUniversidad = @pIdUni";
            condiciones = db.Query<Condiciones>(sql, new{pIdUni = idUni}).ToList();
        }
        return condiciones;
    }
    public static List<Preguntas> ObtenerPreguntasTest()
    {
        List<Preguntas> preguntas = new List<Preguntas>();
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Preguntas";
            preguntas = db.Query<Preguntas>(sql).ToList();
        }
        return preguntas;
    }
        public static ResultadoBusqueda Busqueda(string datoIng)
    {
        ResultadoBusqueda resultados = new ResultadoBusqueda 
        {
            Universidadesr = new List<Universidades>(),
            Carrerasr = new List<Carreras>()
        };
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Universidades WHERE Nombre LIKE @pDatoIng";
            resultados.Universidadesr = db.Query<Universidades>(sql, new { pDatoIng = datoIng + "%" }).ToList();
            sql = "SELECT * FROM Carreras WHERE Nombre LIKE @pDatoIng";
            resultados.Carrerasr = db.Query<Carreras>(sql, new {pDatoIng = "%" + datoIng + "%" }).ToList();
        }        
        return resultados;        
    }
    public static bool VerificarLogin(string mail, string contra)
    {
        Usuarios usuario = null;
        usuario = null;
        using(SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT Mail, Contraseña FROM Usuarios WHERE Mail = @pmail AND Contraseña = @pcontraseña";
            usuario = db.QueryFirstOrDefault<Usuarios>(sql, new{pmail = mail, pcontraseña = contra});
        }
        if(usuario != null)
        return true;
        else
        return false;
    }
    /*
    public static ResultadoBusqueda Busqueda(string datoIng, Busqueda pbusqueda) 
    {
        ResultadoBusqueda resultados = new ResultadoBusqueda();
        resultados = null;
        if(pbusqueda.Valoraciones == null)
        pbusqueda.Valoraciones = 0;
        if(pbusqueda.PrecioMax == null)
        pbusqueda.PrecioMax = int.MaxValue;
        if(pbusqueda.PrecioMin == null)
        pbusqueda.PrecioMin = 0;
        
        if(pbusqueda.Tipo == 't') //busca carreras y universidades de todo tipo
        {
            using(SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Universidades WHERE Nombre LIKE @pDatoIng%";
                resultados.Universidadesr = db.Query<Universidades>(sql, new{pDatoIng = datoIng}).ToList();
            }        
            using(SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Carreras WHERE Nombre LIKE @pDatoIng%";
                resultados.Carrerasr = db.Query<Carreras>(sql, new{pDatoIng = datoIng}).ToList();
            }          
            if (resultados.Carrerasr.Count == 0 && resultados.Universidadesr.Count == 0)
            return null;
            else 
            return resultados;
        }
        
        else if(pbusqueda.Tipo == 'c') //busca carreras
        {
            using(SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql = "SELECT Id FROM Carreras WHERE Nombre LIKE @pDatoIng% AND Valoraciones >= @pvaloraciones AND Costo >= @pmin AND Costo <= @pmax";
                resultados.Carrerasr = db.Query<Carreras>(sql, new{pmin = pbusqueda.PrecioMin, pmax = pbusqueda.PrecioMax, pvaloraciones = pbusqueda.Valoraciones}).ToList();
            }
        }
        else if (pbusqueda.Tipo == 'u' && pbusqueda.TipoUni == null ) //busca universidades de todo tipo
        {
            using(SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql = "SELECT Id FROM Universidades WHERE Nombre LIKE @pDatoIng% AND Valoraciones >= @pvaloraciones";
                resultados.Universidadesr = db.Query<Universidades>(sql, new{pDatoIng = datoIng, pvaloraciones = pbusqueda.Valoraciones}).ToList();
            }
            if (resultados.Universidadesr.Count == 0)
            return null;
            else 
            return resultados;
        }
        else if(pbusqueda.Tipo == 'u' && pbusqueda.TipoUni != null) //busca universidades de un tipo (publica o privada)
        {   
            using(SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql = "SELECT Id FROM Universidades WHERE Nombre LIKE @pDatoIng% AND Tipo = @ptipo AND Valoraciones >= @pvaloraciones";
                resultados.Universidadesr = db.Query<Universidades>(sql, new{pDatoIng = datoIng, ptipo = pbusqueda.Tipo, pvaloraciones = pbusqueda.Valoraciones}).ToList();
            }
            if (resultados.Universidadesr.Count == 0)
            return null;
            else 
            return resultados;
        }
        return resultados;
    }

*/

}

