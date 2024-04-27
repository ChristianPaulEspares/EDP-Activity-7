using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryofAlexandria
{
    public partial class Aboutform : Form
    {
        private Form previousForm;
        public Aboutform(Form previousForm)
        {
            InitializeComponent();
            this.previousForm = previousForm;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Aboutform_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close(); 
            previousForm.Show();
        }
    }
}
