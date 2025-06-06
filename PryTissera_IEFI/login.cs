using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PryTissera_IEFI
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
           
        }

      
        private void btnIniciar_Click(object sender, EventArgs e)
        {
            string usuario = txtUser.Text;
            string contrasena = txtPass.Text;

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
            {
                MessageBox.Show("Completar usuario y contraseña");
                return;
            }

            using (SqlConnection con = new SqlConnection(conexion.cadena))
            {
                con.Open();

                // Verificar usuario, contraseña y rol
                string query = "SELECT Rol FROM Usuarios WHERE Usuario = @usuario AND Contraseña = @contraseña";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@contraseña", contrasena);

                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    string rol = result.ToString();
                    DateTime inicioSesion = DateTime.Now;

                    // Insertar en auditoría
                    string insertAudit = "INSERT INTO Auditoria (Usuario, FechaInicio) OUTPUT INSERTED.Id VALUES (@usuario, @fechaInicio)";
                    SqlCommand cmdAudit = new SqlCommand(insertAudit, con);
                    cmdAudit.Parameters.AddWithValue("@usuario", usuario);
                    cmdAudit.Parameters.AddWithValue("@fechaInicio", inicioSesion);

                    int idAuditoria = (int)cmdAudit.ExecuteScalar();

                    // Guardar datos en sesión activa
                    SesionActiva.Usuario = usuario;
                    SesionActiva.FechaInicio = inicioSesion;
                    SesionActiva.IdAuditoria = idAuditoria;
                    SesionActiva.Rol = rol;

                    // Abrir pantalla principal
                    this.Hide();
                    FormPrincipal principal = new FormPrincipal();
                    principal.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos");
                }




            }

      
        
                                                                                                      
            
            
            
         
        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            RegistroUsuarios RegistroUsuariosForm = new RegistroUsuarios();
            RegistroUsuariosForm.StartPosition = FormStartPosition.Manual;
            RegistroUsuariosForm.Location = this.Location;

            this.Hide();
            RegistroUsuariosForm.ShowDialog();
            this.Close();
        }
    } 

}