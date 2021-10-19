using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain;

namespace LOGIN
{
    public partial class FormRecoveryPassword : Form
    {
        public FormRecoveryPassword()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            var user = new UserModel();
            var result = user.recoverPassword(txtUserRequest.Text);
            lblResultado.Text = result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormRecoveryPassword_Load(object sender, EventArgs e)
        {

        }
    }
}
