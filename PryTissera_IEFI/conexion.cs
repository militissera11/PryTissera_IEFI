using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PryTissera_IEFI
{
    internal class conexion
    {
        public static string cadena = "Server=localhost;Database=Usuarios;Trusted_Connection=True;";

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadena);
        }

        public void ProbarConexion()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    connection.Open();
                    MessageBox.Show("✅ Conexión exitosa a la base de datos", "Conexión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al conectar con la base de datos:\n" + ex.Message, "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
}   }







