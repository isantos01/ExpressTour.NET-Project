using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public class DatabaseService
    {
        private ExpressTourDataContext db; // Instancia del contexto

        public DatabaseService()
        {
            db = new ExpressTourDataContext(); // Se conecta automáticamente usando la cadena de conexión del .dbml
        }

        public void TestConexion()
        {
            try
            {
                db.Connection.Open();  // Abre la conexión manualmente
                Console.WriteLine("Conexión exitosa.");
                db.Connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error de conexión: " + ex.Message);
            }
        }
    }
}