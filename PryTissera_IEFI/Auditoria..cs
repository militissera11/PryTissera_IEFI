using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PryTissera_IEFI
{
    public partial class FormAuditoria : Form
    {
        public FormAuditoria()
        {
            InitializeComponent();
        }

        private void FormAuditoria_Load(object sender, EventArgs e)
        {
            CargarAuditoria();
        }




        private void CargarAuditoria()
        {
            try
            {
                using (SqlConnection con = new conexion().ObtenerConexion())
                {
                    string consulta = "SELECT Usuario, FechaInicio, FechaCierre, DuracionSesion FROM Auditoria";
                    SqlDataAdapter adaptador = new SqlDataAdapter(consulta, con);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dgvAuditoria.DataSource = tabla;

                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al cargar la auditoría: " + ex.Message);
            }

}   }   }
