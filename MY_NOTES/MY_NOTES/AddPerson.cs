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
    public partial class AddPerson : Form
    {
        //DataBase Connection 
        public static SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Agni\Documents\MyNotesDataBase.mdf;Integrated Security=True;Connect Timeout=30");  
        public AddPerson()
        {
            InitializeComponent();
            //TextBox8 is equal to the current date and time
            textBox8.Text = DateTime.Now.ToString("dd / M / yyyy");
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Uniquename, Name, Surname, Address must be written in order to continue  
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
                {
                    MessageBox.Show("Please write uniquename, name, surname, address of the person you want to add");
                }
                else
                {
                    //Checking If The Person Exist In DataBase
                    SqlCommand Uniquename = new SqlCommand("Select Uniquename from Person_Table where Uniquename= @Uniquename", con);
                    Uniquename.Parameters.AddWithValue("@Uniquename", this.textBox1.Text);
                    con.Open();
                    var n = Uniquename.ExecuteScalar();
                    //If Unique Name Exist Then The Person Can Not Be Saved
                    if (n != null)
                    {
                        MessageBox.Show("This person already exist / write another unique name");
                    }
                    else
                    {
                        //Save A Person To DataBase
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandText = "insert into Person_Table values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "')";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Record Inserted Successfully");
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox4.Clear();
                        textBox5.Clear();
                        textBox6.Clear();
                        textBox7.Clear();
                        ////////////////////////////
                    }
                    con.Close();
                }
            }
            catch
            {
                MessageBox.Show("Wrong Input");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainPage mp = new MainPage();
            this.Hide();
            mp.Show();
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void AddPerson_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
