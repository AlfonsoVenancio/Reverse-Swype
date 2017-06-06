using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reverse_Swype
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            
            string directoryName = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(Application.ExecutablePath)))+"\\layouts";
            Console.WriteLine(directoryName);
            string[] fileNames = System.IO.Directory.GetFiles(directoryName);
            List<string> listFileNames = new List<string>();
            foreach (string fileName in fileNames)
            {
                listFileNames.Add(fileName.Replace(directoryName+"\\", "").Replace(".txt",""));
            }
            string[] fileNamesClear = listFileNames.ToArray();
            //this.comboBox1.Items.AddRange(files);
            this.listKeyboards.Items.AddRange(fileNamesClear);
            listKeyboards.SelectedItem = 1;
        }
    }
}
