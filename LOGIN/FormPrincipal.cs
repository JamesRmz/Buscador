using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Common.Cache;

namespace LOGIN
{
    public partial class FormPrincipal : Form
    {

        String Path = @"\\RUTAIP\Users\USUARIO\Desktop\Prueba";
        public FormPrincipal()
        {
            InitializeComponent();
        }


        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lpram);

        //clck por error en imagen de logo Callcom
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnSlide_Click(object sender, EventArgs e)
        {
            if (MenuVertical.Width== 250)
            {
                MenuVertical.Width = 70;
            }
            else
            {
                MenuVertical.Width = 250;
            }
        }

        private void iconcerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iconmaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            iconrestaurar.Visible = true;
            iconmaximizar.Visible = false;
        }

        private void iconrestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            iconrestaurar.Visible = false;
            iconmaximizar.Visible = true;
        }

        private void iconminimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        //funcion para abrir forms dentro del panel contenedor 
        private void AbrirFormInPanel(object FormHijo)
        {
            if (this.panelContenedor.Controls.Count > 0)
                this.panelContenedor.Controls.RemoveAt(0);
            Form fh = FormHijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panelContenedor.Controls.Add(fh);
            fh.Show();
            
        }
        //
        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            //abrir formulario dentro de pabnel contenedor 
            AbrirFormInPanel(new FormUserProfile());
           // AbrirFormInPanel(new FOrnulario())
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro de cerrar sesion?", "Warning",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                this.Close();
        }
        //Cargar a formulario principal
        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            LoadUserData();
            ManejoDePermisos();
        }
        private void ManejoDePermisos()
        {
            if (UserLoginCache.Position == Positions.User)
            {
                btnUsuarios.Visible = false;
                btnMttoUsers.Visible = false;
            }
            if (UserLoginCache.Position == Positions.Administrator)
            {
                //code
                btnUsuarios.Visible = false;
                btnMttoUsers.Enabled = true;
            }
        }

        private void LoadUserData()
        {
            lblName.Text = UserLoginCache.FirstName + " " + UserLoginCache.LastName;
            lblPosition.Text = UserLoginCache.Position;
            lblEmail.Text = UserLoginCache.Email;

        }
        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MenuVertical_Paint(object sender, PaintEventArgs e)
        {

        }


        //------------BtnAudios-----------////////
        private void button2_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Path))
            {
                AbrirFormInPanel(new Audios());
            }
            else
            {
                MessageBox.Show("Verifique su conexión a la Red");
                
            }
     
            
        }

        private void btnMttoUsers_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new Usuarios());
        }
    }
}
