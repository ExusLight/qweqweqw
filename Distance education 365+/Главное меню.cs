using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Distance_education_365_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {         
            Form2 spisok = new Form2();
            spisok.Show();
        }
        private void button3_Click(object sender, EventArgs e)
        {         
            Cоздание create = new Cоздание();
            create.Show();
        }

       

        
    }
}
