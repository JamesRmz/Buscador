using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LOGIN
{
    public partial class Audios : Form
    {
        //---------------Ruta de Acceso a Carpeta--------------------//
        String Path = @"\\RutaIP\Users\USUARIO\Desktop\Prueba";


        public Audios()
        {
            InitializeComponent();
        }

        private void Audios_Load(object sender, EventArgs e)
        {
            //ruta de filesystemwatcher
          
                fileSystemWatcher1.Path = Path;
                GetFiles();
         
        }
      
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = Directory.GetFiles(Path)[listBox1.SelectedIndex];
            //axWindowsMediaPlayer1.URL =  listBox1.SelectedItem.ToString();
        }
        private void GetFiles()
        {     
            string[] lst = Directory.GetFiles(Path);

            listBox1.Items.Clear();
            foreach (var sFile in lst)
            {
                listBox1.Items.Add(System.IO.Path.GetFileName(sFile));
                //listBox1.Items.Add(sFile);
            }
        }
        //cadaque cambia un archivo se refresca y muestra los archivos en tiempo real
        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            GetFiles();
        }
        private void fileSystemWatcher1_Renamed(object sender, RenamedEventArgs e)
        {
            GetFiles();
        }
        private void btnbuscar_Click(object sender, EventArgs e)
        {
            for (int i = listBox1.Items.Count - 1; i > 0; i--)
            {
                if (listBox1.Items[i].ToString().Contains(txtbusqueda.Text))
                    listBox1.SetSelected(i, true);
                else
                {
                    listBox1.Items.RemoveAt(i);
                }
            }
        
        }
    
    }
}

