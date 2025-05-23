using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace Proyecto_De_investigacion.Servicios
{
    public class BaseDeDatosService
    {
        private readonly string connectionString;

        public BaseDeDatosService()
        {
            connectionString = ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString;
        }

        public void Guardar(string prompt, string resultado)
        {
            try
            {
                var conn = new SqlConnection(connectionString);
                conn.Open();

                var cmd = new SqlCommand("INSERT INTO Investigaciones (Prompt, Resultado) VALUES (@p, @r)", conn);
                cmd.Parameters.AddWithValue("@p", prompt);
                cmd.Parameters.AddWithValue("@r", resultado);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar en la base de datos: " + ex.Message);
            }
        }
    }
}