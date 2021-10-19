using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.Cache;
using Domain;

namespace LOGIN
{
    public partial class FormUserProfile : Form
    {
        public FormUserProfile()
        {
            InitializeComponent();
        }

        private void FormUserProfile_Load(object sender, EventArgs e)
        {
            loadUserData();
            initalizePassEditControls();
        }
        private void loadUserData()
        {
            //view
            lblNombreUsuario.Text = UserLoginCache.LoginName;
            lblNombre.Text = UserLoginCache.FirstName;
            lblApellido.Text = UserLoginCache.LastName;
            lblemail.Text = UserLoginCache.Email;
            lblcargo.Text = UserLoginCache.Position;

            //Edit Panel

            txtUsername.Text = UserLoginCache.LoginName;
            txtFirstName.Text = UserLoginCache.FirstName;
            txtLastName.Text = UserLoginCache.LastName;
            txtEmail.Text = UserLoginCache.Email;
            txtPassword.Text = UserLoginCache.Password;
            txtConfirmPass.Text = UserLoginCache.Password;
            txtCurrentPassword.Text = "";
            
        }

        private void initalizePassEditControls()
        {
            LinkEditPass.Text = "Edit";
            txtPassword.Enabled = false;
            txtPassword.UseSystemPasswordChar = true;
            txtConfirmPass.Enabled = false;
            txtConfirmPass.UseSystemPasswordChar = true;
        }
        private void reset()
        {
            loadUserData();
            initalizePassEditControls();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel1.Visible = true;
            loadUserData();
        }

        private void LinkEditPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (LinkEditPass.Text == "Edit")
            {
                LinkEditPass.Text = "Cancel";
                txtPassword.Enabled = true;
                txtPassword.Text = "";
                txtConfirmPass.Enabled = true;
                txtConfirmPass.Text = "";
            }
            else if  (LinkEditPass.Text == "Cancel")
            {
                initalizePassEditControls();
                txtPassword.Text = UserLoginCache.Password;
                txtConfirmPass.Text = UserLoginCache.Password;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text.Length >= 5)
            {
                if (txtPassword.Text == txtConfirmPass.Text)
                {
                    if (txtCurrentPassword.Text == UserLoginCache.Password)
                    {
                        var userModel = new UserModel(idUser: UserLoginCache.IdUser,
                            loginName: txtUsername.Text,
                            password: txtPassword.Text,
                            nombre: txtFirstName.Text,
                            apellido: txtLastName.Text,
                            cargo: null,
                            email: txtEmail.Text
                           );
                        //var result = userModel.editarPerfilUsuario();
                        //MessageBox.Show(result);
                        //reset();
                        //panel1.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Contraseña incorrecta, intente de nuevo");
                    }
                }
                else
                    MessageBox.Show("La contraseña no coincide, intente de nuevo por favor");
            }
            else
                MessageBox.Show("La contraseña tiene que tener 5 caracteres minimo");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            reset();
        }
    }
}
