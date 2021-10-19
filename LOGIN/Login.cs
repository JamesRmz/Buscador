using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//PARA PODER UTILIZAR EL DLL//
using System.Runtime.InteropServices;
using Domain;

namespace LOGIN
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
     

        //PODER CAMBIAR LOGIN DE LUGAR EN PANTALLA//
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lpram);
       //HASTA AQUI//

        //EVENTO DE ESCRITURA CAMBIAR COLOR DE TEXTO DE TEXTBOX DE USUARIO CUANDO SE TIENE LA PALABRA USUARIO//
        private void txtuser_Enter(object sender, EventArgs e)
        {
            if (txtuser.Text == "USUARIO")
            {
                txtuser.Text = "";
                txtuser.ForeColor = Color.LightGray;
            }
        }
        //EVENTO DE ESCRITURA CAMBIAR COLOR DE TEXTO DE TEXTBOX DE USUARIO CUANDO NO HAY NADA ESCRITO//
        private void txtuser_Leave(object sender, EventArgs e)
        {
            if (txtuser.Text=="")
            {
                txtuser.Text = "USUARIO";
                txtuser.ForeColor = Color.DimGray;

            }
        }
        //EVENTO DE ESCRITURA CAMBIAR COLOR DE TEXTO DE TEXTBOX DE CONTRASEÑA CUANDO SE TIENE LA PALABRA USUARIO//
        private void txtpass_Enter(object sender, EventArgs e)
        {
            if (txtpass.Text == "CONTRASEÑA")
            {
                txtpass.Text = "";
                txtpass.ForeColor = Color.LightGray;
                txtpass.UseSystemPasswordChar = true;
            }
        }
        //EVENTO DE ESCRITURA CAMBIAR COLOR DE TEXTO DE TEXTBOX DE CONTRASEÑA CUANDO NO HAY NADA ESCRITO//
        private void txtpass_Leave(object sender, EventArgs e)
        {
            if (txtpass.Text=="")
            {
                txtpass.Text = "CONTRASEÑA";
                txtpass.ForeColor = Color.DimGray;
                txtpass.UseSystemPasswordChar = false;
            }
        }
        //EVENTO DE BOTON CERRAR LOGIN//
        private void btncerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //EVENTO MINIMIZAR LOGIN//
        private void btnminimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        //EVENTO MOUSE MOVER VENTANA DESDE FORM//
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        //EVENTO MOUSE MOVER VENTANA DESDE PANEL//
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        //Evento Click de boton Login 
        private void btnlogin_Click(object sender, EventArgs e)
        {
            if (txtuser.Text != "USUARIO")
            {
                if (txtpass.Text != "CONTRASEÑA")
                {
                    //INSTANCIA A CLASE USERMODEL
                    UserModel user = new UserModel();

                    //SE ENVIA EL TEXTO INTRODUCIDO EN LOS TEXTBOX A LA VALIDACION 
                    var validLogin = user.LoginUser(txtuser.Text, txtpass.Text);

                    //SI LA VALIDACION ES VERDADERA MOSTRARA EL FORMULARIO PRINCIPAL 
                    if (validLogin==true)
                    {

                        this.Hide();
                        FormWelcome welcome = new FormWelcome();
                        welcome.ShowDialog();
                        FormPrincipal mainMenu = new FormPrincipal();
                        mainMenu.Show();
                        mainMenu.FormClosed += Logout;
                        
                    }

                    //SI LA VALIDACION ES FALSA MOSTRARA EL MENSAJE DE ERROR EN VALIDACION
                    else
                    {
                        msgError("Usuario o contraseña incorrecta.  \n  Por favor intente nuevamente.");
                        txtpass.Text= "CONTRASEÑA";
                        txtuser.Focus();
                    }
                }
                //SI EL TEXTBOX DE CONTRASEÑA ESTA VACIO MUESTRA ESTE MENSAJE
                else
                {
                    msgError("Por favor ingrese su contraseña");
                }
            }
            //SI EL TEXTBOX ESTA VACIO MUESTRA ESTE MENSAJE 
            else msgError("Por favor ingrese su nombre de usuario");

        }
        //AQUI SE MOSTRARA EL MENSAJE QUE SE INCLUIRA EN EL LABEL DE ERROR
        private void msgError(string msg)
        {
            lblErrorMessage.Text = "      " + msg;
            lblErrorMessage.Visible = true;
        }
        //OBJETO PARA DESLOGUEARº
        private void Logout(Object sender, FormClosedEventArgs e)
        {
            txtpass.Text= "CONTRASEÑA";
            txtpass.UseSystemPasswordChar = false;
            txtuser.Text = "USUARIO";
            lblErrorMessage.Visible = false;
            this.Show();
            //txtuser.Focus();
        }
        //Linklabel de recuperar contraseña abre el form de recovery
        private void linkpass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var recoverPassword = new FormRecoveryPassword();
            recoverPassword.ShowDialog();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
