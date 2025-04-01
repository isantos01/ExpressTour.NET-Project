using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using System.Configuration;

namespace BusinessLayer
{
    public class DatabaseService
    {
        // Instancia del contexto de datos para interactuar con la base de datos
        private readonly ExpressTourDataContext db;

        // Constructor de la clase
        public DatabaseService()
        {
            // Inicializa el contexto con la cadena de conexión almacenada en App.config
            string connectionString = ConfigurationManager.ConnectionStrings["ExpressTourConnectionString"].ConnectionString;
            db = new ExpressTourDataContext(connectionString);

        }

        // Método para probar la conexión con la base de datos
        public string TestConexion()
        {
            try
            {
                // Abre la conexión a la base de datos
                db.Connection.Open();

                // Cierra la conexión después de abrirla (prueba exitosa)
                db.Connection.Close();

                // Devuelve un mensaje indicando que la conexión fue exitosa
                return "Conexión exitosa.";
            }
            catch (Exception ex)
            {
                // Si hay un error, devuelve el mensaje con la descripción del error
                return "Error de conexión: " + ex.Message;
            }
        }
    }
}
