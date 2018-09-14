using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MY_NOTES
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                //DataBase Connection
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Agni\Documents\Login_Data.mdf;Integrated Security=True;Connect Timeout=30");
                //From Login_Table Checking If Texbox1 Is Equal To Username And TextBox2 Is Equal To Password 
                SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From [Login_Table] where UserName = '" + textBox1.Text + "' and Password = '" + textBox2.Text + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                //If username and password correct 
                if (dt.Rows[0][0].ToString() == "1")
                {
                    this.Hide();
                    MainPage mp = new MainPage();
                    mp.Show();
                }
                //If username and password !correct
                else
                {
                    MessageBox.Show("Wrong Input");
                }
            }
            else if (textBox1.Text == "" && textBox2.Text != "")
            {
                MessageBox.Show("Please write the username");
            }
            else if (textBox1.Text != "" && textBox2.Text == "")
            {
                MessageBox.Show("Please write the password");
            }
            else
            {
                MessageBox.Show("Please write the username and the password");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
