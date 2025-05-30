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
    public partial class FormGestionUsuarios : Form
    {
        public FormGestionUsuarios()
        {
            InitializeComponent();
            CargarUsuarios();
        }
        private void CargarUsuarios()
        {
            using (SqlConnection con = new SqlConnection(conexion.cadena))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT Id, Usuario, Contraseña, Rol FROM Usuarios", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvUsuarios.DataSource = dt;
            }
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "" || txtContraseña.Text == "" || txtRol.Text == "")
            {
                MessageBox.Show("Completar todos los campos.");
                return;
            }

            using (SqlConnection con = new SqlConnection(conexion.cadena))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Usuarios (Usuario, Contraseña, Rol) VALUES (@u, @c, @r)", con);
                cmd.Parameters.AddWithValue("@u", txtUsuario.Text);
                cmd.Parameters.AddWithValue("@c", txtContraseña.Text);
                cmd.Parameters.AddWithValue("@r", txtRol.Text);
                cmd.ExecuteNonQuery();
            }

            CargarUsuarios();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null || txtUsuario.Text == "" || txtContraseña.Text == "" || txtRol.Text == "")
            {
                MessageBox.Show("Seleccioná un usuario de la tabla y completá todos los campos.");
                return;
            }

            int id = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["Id"].Value);

            using (SqlConnection con = new SqlConnection(conexion.cadena))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Usuarios SET Usuario = @u, Contraseña = @c, Rol = @r WHERE Id = @id", con);
                cmd.Parameters.AddWithValue("@u", txtUsuario.Text);
                cmd.Parameters.AddWithValue("@c", txtContraseña.Text);
                cmd.Parameters.AddWithValue("@r", txtRol.Text);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

            CargarUsuarios();



        }

         private void btnEliminar_Click(object sender, EventArgs e)
         {
            if (dgvUsuarios.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná un usuario de la tabla para eliminar.");
                return;
            }
            int id = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["Id"].Value);

            var confirm = MessageBox.Show("¿Estás seguro que querés eliminar este usuario?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(conexion.cadena))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Usuarios WHERE Id = @id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }

                CargarUsuarios();
            }

         }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtUsuario.Text = dgvUsuarios.CurrentRow.Cells["Usuario"].Value.ToString();
                txtContraseña.Text = dgvUsuarios.CurrentRow.Cells["Contraseña"].Value.ToString();
                txtRol.Text = dgvUsuarios.CurrentRow.Cells["Rol"].Value.ToString();
            }
        }

        private void FormGestionUsuarios_Load(object sender, EventArgs e)
        {

        }
    }   }   
