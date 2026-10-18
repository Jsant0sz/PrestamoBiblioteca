using System.Reflection;
using Microsoft.Data.Sqlite;

namespace incializadorBaseDatos.Persistencia;

internal sealed class InicializadorBaseDatos
{
    public string RutaBaseDatos { get; } = Environment.GetEnvironmentVariable("PRESTAMO_BIBLIOTECA_DB")
    //Intancia con una variable ambiente y global, se puede tener una variable que se pueda utilizar en cualquier parte y le va a devolver algo, donde se vana encontrar y donde se van aguardar datos
      ?? Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "UMG",
            "PrestamoBiblioteca",
            "Biblioteca.db");

        public string CadenaConexion => new SqliteConnectionStringBuilder
    {
        DataSource = RutaBaseDatos,
        ForeignKeys = true
    }.ToString();
    // Indica a la propiedad, cual va a ser la serie de caraceres que se va a usar, para poder abrir la base: cadena coneccion va hacer una instancia a un objeto y va a devolver un dato

     public void Inicializar()
    {
        var directorio = Path.GetDirectoryName(RutaBaseDatos);
        if (!string.IsNullOrEmpty(directorio)) Directory.CreateDirectory(directorio);
        using var conexion = new SqliteConnection(CadenaConexion);
        conexion.Open();

        using var comando = conexion.CreateCommand();
        comando.CommandText = LeerScriptTablas();
        comando.ExecuteNonQuery();
    }
// Utiliza, en el scrip y lo va a ejecutar, lo primero que se hace es crear el directorio fisico en el APPlocarData, si la carpeta no existe la crea si ya esta ta bien, revisa el directorio si es valido
//en el suing, se define un objeto y se isntancia, que proviene con la base de datos y la ruta. el adaptador e suna libreria de Mircrosft
// Se le pasa tdo del archivo de base de datos, al momento de ejcutarlo, se va a ejecutar tdo el scrip en la base de base de datos.

    private static string LeerScriptTablas()
    {
        var ensamblado = Assembly.GetExecutingAssembly();
        var recurso = ensamblado.GetManifestResourceNames()
            .Single(nombre => nombre.EndsWith("crear_tablas.sql", StringComparison.OrdinalIgnoreCase));
        using var stream = ensamblado.GetManifestResourceStream(recurso)
            ?? throw new InvalidOperationException("No se encontro el script de tablas.");
        using var lector = new StreamReader(stream);
        return lector.ReadToEnd();   
    }
    // Toma el archivo, lo va a leer y lo colocar en un tipo de dato string, el, extarlo el archivo, cear un buffer, para guardar el contenido del archi y mandarlo
    // prueba o intente pasar el contendiod el archivo sobre el bufffer, o si no que de error, y si tiene exito que se lo asigne a un objeto

    //RETRO ALIMENTACIÓN
    //dA ACCESO A LAS LIBRERIa
}