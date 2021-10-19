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
    public partial class Usuarios : Form
    {
        UserModel ObjetoModel = new UserModel();

        private string IdUsuario = null;
        private bool Editar = false;
        

        public Usuarios()
        {
            InitializeComponent();
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            MostrarUsuarios();
            MostrarPerfiles();
            btnGuardar.Enabled = false;
        }

        private void MostrarUsuarios()
        {
            UserModel ObjetoModel = new UserModel();
            dataGridView1.DataSource = ObjetoModel.MostrarUsuarios();
        }

        private void MostrarPerfiles()
        {

            UserModel ObjetoModel = new UserModel();
            cmbPerfiles.DataSource = ObjetoModel.MostrarPerfiles();
            cmbPerfiles.DisplayMember = "Perfil";
            cmbPerfiles.ValueMember = "IdPerfil";
        }
        
        private void Validar()
        {
            var vr = !string.IsNullOrEmpty(txtLoginName.Text) && !string.IsNullOrEmpty(txtNombre.Text) && !string.IsNullOrEmpty(txtContraseña.Text)
                && !string.IsNullOrEmpty(txtConfirmarContra.Text) && !string.IsNullOrEmpty(txtApellido.Text) && !string.IsNullOrEmpty(txtEmail.Text)&& !string.IsNullOrEmpty(cmbPerfiles.Text);
            btnGuardar.Enabled = vr;
        }
       
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Editar == false)
            {
               
                    if (txtContraseña.Text == txtConfirmarContra.Text )
                {
                    
                        try
                        {

                            //ObjetoModel.InsertUser(txtLoginName.Text,txtContraseña.Text,txtNombre.Text,txtApellido.Text,txtPerfil.Text,txtEmail.Text);
                            ObjetoModel.InsertUser(txtLoginName.Text, txtContraseña.Text, txtNombre.Text, txtApellido.Text, cmbPerfiles.Text, txtEmail.Text);
                            MessageBox.Show("se inserto correctamente");

                            LimpiarForm();
                            MostrarUsuarios();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("no se pudo insertar los datos por: " + ex);

                        }   
                  
                }
                else
                {
                    MessageBox.Show("Contraseña incorrecta, intente de nuevo");
                }
            }

            //EDITAR

            if (Editar== true)
            {
                try
                {
                    //ObjetoModel.EditarUsuarios(txtLoginName.Text,txtContraseña.Text,txtNombre.Text,txtApellido.Text,txtPerfil.Text,txtEmail.Text, IdUsuario);
                    ObjetoModel.EditarUsuarios(txtLoginName.Text, txtContraseña.Text, txtNombre.Text, txtApellido.Text, cmbPerfiles.Text, txtEmail.Text, IdUsuario);
                    MessageBox.Show("Se edito correctmanete");
                    
                    LimpiarForm();
                    
                    MostrarUsuarios();
                    Editar = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se puede editar loa datos por" + ex);
                    
                }
            }
        }
        private void LimpiarForm()
        {
            txtLoginName.Clear();
            txtNombre.Text=" ";
            txtApellido.Clear();
            txtContraseña.Text=" ";
            txtConfirmarContra.Text = " ";
            txtPerfil.Clear();
            txtEmail.Clear();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Editar = true;

                txtLoginName.Text = dataGridView1.CurrentRow.Cells["LoginName"].Value.ToString();
                txtContraseña.Text = dataGridView1.CurrentRow.Cells["Password"].Value.ToString();
                txtConfirmarContra.Text = dataGridView1.CurrentRow.Cells["Password"].Value.ToString();
                txtConfirmarContra.Enabled = false;
                txtNombre.Text = dataGridView1.CurrentRow.Cells["FirstName"].Value.ToString();
                txtApellido.Text = dataGridView1.CurrentRow.Cells["LastName"].Value.ToString();
                //txtPerfil.Text = dataGridView1.CurrentRow.Cells["Position"].Value.ToString();
                cmbPerfiles.Text = dataGridView1.CurrentRow.Cells["Position"].Value.ToString();

                txtEmail.Text = dataGridView1.CurrentRow.Cells["Email"].Value.ToString();

                IdUsuario = dataGridView1.CurrentRow.Cells["IdUser"].Value.ToString();

            }
            else
                MessageBox.Show("Seleccione una fila por favor");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                IdUsuario = dataGridView1.CurrentRow.Cells["IdUser"].Value.ToString();
                ObjetoModel.EliminarUsuario(IdUsuario);
                MessageBox.Show("Eliminado Correctamente");
                MostrarUsuarios();
            }
            else
            {
                MessageBox.Show("Seleccione una fila por favor");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbPerfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            Validar();
        }

        private void txtLoginName_TextChanged(object sender, EventArgs e)
        {
            Validar();
        }

        private void txtContraseña_TextChanged(object sender, EventArgs e)
        {
            Validar();
        }

        private void txtConfirmarContra_TextChanged(object sender, EventArgs e)
        {
            Validar();
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            Validar();
        }

        private void txtApellido_TextChanged(object sender, EventArgs e)
        {
            Validar();
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            Validar();
        }
    }
}
