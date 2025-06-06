﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            miConexion.ProbarConexion();

            IsMdiContainer = true;

            toolStripStatusFecha.Text = "Usuario: admin"; // o el nombre logueado

            toolStripStatusUser.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
          
        }

      
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
           
        }

        private void auditoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAuditoria formAuditoria = new FormAuditoria();
            formAuditoria.Show();
        }
    }
}
