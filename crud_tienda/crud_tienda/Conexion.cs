using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace crud_tienda
{
    class Conexion
    {
        public static MySqlConnection conexion()
        {
            string servidor = "localhost";
            string bd = "tienda";
            string usuario = "pastor";
            string password = "30405110";

            string cadenaConexion = "Database=" + bd + "; Data source=" + servidor + "; User Id= " + usuario + "; Password= " + password + "";

            try
            {
                MySqlConnection ConexionBD = new MySqlConnection(cadenaConexion);   
                return ConexionBD;
            }
            catch (MySqlException ex )
            {
                Console.WriteLine($"Error = {ex.Message}");
                return null;
            }
        }

    }
}
