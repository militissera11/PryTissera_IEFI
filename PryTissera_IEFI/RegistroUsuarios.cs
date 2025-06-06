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
    public partial class RegistroUsuarios : Form
    {
        conexion con = new conexion();
        public RegistroUsuarios()
        {
            InitializeComponent();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contraseña = txtContraseña.Text.Trim();

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contraseña))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(conexion.cadena))
                {
                    conn.Open();
                    string checkQuery = "SELECT COUNT(*) FROM Usuarios WHERE Usuario = @Usuario";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@Usuario", usuario);

                    int userCount = (int)checkCmd.ExecuteScalar();

                    if (userCount > 0)
                    {
                        MessageBox.Show("El nombre de usuario ya existe. Por favor elija otro.");
                        return;
                    }

                    string query = "INSERT INTO Usuarios (Usuario, Contraseña) VALUES (@Usuario, @Contraseña)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Usuario", usuario);
                    cmd.Parameters.AddWithValue("@Contraseña", contraseña);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Usuario creado correctamente.");
                    txtUsuario.Clear();
                    txtContraseña.Clear();
                    login loginForm = new login();
                    this.Hide();
                    loginForm.ShowDialog();
                    this.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar el usuario: " + ex.Message);
            }
        }

        private void btnTengoUsuario_Click(object sender, EventArgs e)
        {
            login loginForm = new login();
            loginForm.StartPosition = FormStartPosition.Manual;
            loginForm.Location = this.Location;
            this.Hide();
            loginForm.ShowDialog();
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtContraseña.PasswordChar = chkMostrarCon.Checked ? '\0' : '*';
        }
    }
}
