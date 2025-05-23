using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            if (txtUser.Text == "" & txtPass.Text == "")
            {
                FormPrincipal principal = new FormPrincipal();
                this.Hide();
                principal.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Completar usuario y contraseña");
            }
        }

        private void login_Load(object sender, EventArgs e)
        {
            conexion miConexion = new conexion();
            miConexion.ProbarConexion();
        }
    }

}
