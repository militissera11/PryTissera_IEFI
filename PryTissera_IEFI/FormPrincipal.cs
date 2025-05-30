using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PryTissera_IEFI
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();

        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            conexion miConexion = new conexion();
           

            IsMdiContainer = true;

            toolStripStatusFecha.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            toolStripStatusUser.Text = "Usuario: " + SesionActiva.Usuario;


            if (SesionActiva.Rol == "Administrador")
            {
                btnGestionUsuarios.Visible = true;
            }
            else
            {
                btnGestionUsuarios.Visible = false;
            }


        }

      
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
           
        }

        private void auditoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAuditoria formAuditoria = new FormAuditoria();
            formAuditoria.Show();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new conexion().ObtenerConexion())
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("UPDATE Auditoria SET FechaCierre = @cierre WHERE Id = @id", con);
                cmd.Parameters.AddWithValue("@cierre", DateTime.Now);
                cmd.Parameters.AddWithValue("@id", SesionActiva.IdAuditoria);
                cmd.ExecuteNonQuery();
            }

            // Volver al login
            login loginForm = new login();
            this.Hide();
            loginForm.ShowDialog();
            this.Close();
        }

        private void btnGestionUsuarios_Click(object sender, EventArgs e)
        {
            FormGestionUsuarios form = new FormGestionUsuarios();
            form.ShowDialog();
        }
    }
}
