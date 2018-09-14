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
    public partial class ReturnDeletedPerson : Form
    {
        
        //DataBase Connection
        public static SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Agni\Documents\MyNotesDataBase.mdf;Integrated Security=True;Connect Timeout=30");

        public ReturnDeletedPerson()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainPage mp = new MainPage();
            mp.Show();
        }

        private void ReturnDeletedPerson_Load(object sender, EventArgs e)
        {
            //Fill Combobox With Items From Database
            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Deleted1Person_Table";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                comboBox1.Items.Add(dr["Uniquename"].ToString());
            }

            con.Close();
            ////////////////////////////////////////////////////
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Selecting A Person From ComboBox
            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Deleted1Person_Table where Uniquename='" + comboBox1.SelectedItem.ToString() + "'";

            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                textBox1.Text = dr["uniquename"].ToString();
                textBox2.Text = dr["name"].ToString();
                textBox3.Text = dr["surname"].ToString();
                textBox4.Text = dr["address"].ToString();
                textBox5.Text = dr["date"].ToString();
                textBox6.Text = dr["eu"].ToString();
                textBox7.Text = dr["fr"].ToString();
                textBox8.Text = dr["den"].ToString();
            }

            con.Close();
            ////////////////////////////////////////////////////
        }
        void DeleteTheDataFromDeletedPersonDataBase()
        {
            //Delete the data to DeletedPersonDataBase
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from Deleted1Person_Table where Uniquename='" + comboBox1.SelectedItem.ToString() + "'";
            cmd.ExecuteNonQuery();
            con.Close();
            ////////////////////////////////////////////////////
        }
        void refresh_the_page()
        {
            this.Hide();
            MainPage mp = new MainPage();
            mp.Show();
            mp.Hide();
            ReturnDeletedPerson rdp = new ReturnDeletedPerson();
            rdp.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //Name, Surname, Address must be written in order to continue  
                if (textBox1.Text == "")
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
                        cmd.CommandText = "insert into Person_Table values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + textBox5.Text + "')";
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
                        textBox8.Clear();
                        //Using The Function For Deleting The Data From DeletedPersonDataBase
                        DeleteTheDataFromDeletedPersonDataBase();
                        /////////////////////////////////////////////////////
                    }
                    con.Close();
                    //Refresh The Page
                    refresh_the_page();
                    /////////////////////////////////////////////////////
                }
            }
            catch
            {
                MessageBox.Show("Select One Person");
            }          
        }
        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from Deleted1Person_Table where Uniquename='" + comboBox1.SelectedItem.ToString() + "'";
            cmd.ExecuteNonQuery();
            con.Close();
            refresh_the_page();
        }
    }
}
