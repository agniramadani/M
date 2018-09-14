using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MY_NOTES
{
    public partial class MainPage : Form
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddPerson ap = new AddPerson();
            this.Hide();
            ap.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PersonInfo pi = new PersonInfo();
            this.Hide();
            pi.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DeletePerson dp = new DeletePerson();
            this.Hide();
            dp.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdatePerson up = new UpdatePerson();
            this.Hide();
            up.Show();
        }
    }
}
