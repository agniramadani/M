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
    public partial class UpdatePerson : Form
    {
        //Database Connection
        public static SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Agni\Documents\MyNotesDataBase.mdf;Integrated Security=True;Connect Timeout=30");

        public UpdatePerson()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            MainPage mp = new MainPage();
            this.Hide();
            mp.Show();

        }

        private void UpdatePerson_Load(object sender, EventArgs e)
        {
            //Fill Combobox With Items From Database
            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Person_Table";
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
            //Selecting From Person_Table A Person
            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Person_Table where Uniquename='" + comboBox1.SelectedItem.ToString() + "'";

            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                textBox7.Text = dr["name"].ToString();
                textBox8.Text = dr["surname"].ToString();
                textBox9.Text = dr["address"].ToString();
                textBox10.Text = dr["uniquename"].ToString();
                
            }
            con.Close();
            ////////////////////////////////////////////////////
        }
        //Funtion For Checking The Uniquename in DataBase
        private bool CU()
        {
            int a=0;
            //Checking int If The Person Exist In DataBase           
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Person_Table ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["Uniquename"].ToString() == textBox11.Text && textBox11.Text != "")
                {
                    a++;
                }
            }         
            //If Unique Name Exist Then The Person Can Not Be Saved
            if (a != 0)
            {
                return true;
            }
            return false;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Variable declaration
                string uniquename, date;
                double eu, fr, den;
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Person_Table where Uniquename='" + comboBox1.SelectedItem.ToString() + "'";

                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    uniquename = dr["uniquename"].ToString();
                    eu = int.Parse(dr["eu"].ToString());
                    fr = int.Parse(dr["fr"].ToString());
                    den = int.Parse(dr["den"].ToString());
                    date = dr["date"].ToString();

                    //UPDATE UNIQUENAME
                    //If Unique Name Exist Then The Person Can Not Be Saved
                    if (CU())
                    {
                        MessageBox.Show("This person already exist / write another uniquename");
                        break;
                    }
                    else if(textBox11.Text == "")
                    {
                        SqlCommand u = con.CreateCommand();
                        u.CommandText = "update Person_Table set uniquename='" + textBox10.Text + "' where Uniquename='" + uniquename + "'";
                        u.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand u = con.CreateCommand();
                        u.CommandText = "update Person_Table set uniquename='" + textBox11.Text + "' where Uniquename='" + uniquename + "'";
                        u.ExecuteNonQuery();
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////////

                    //UPDATE NAME   
                    SqlCommand n = con.CreateCommand();
                    n.CommandText = "update Person_Table set name='" + textBox7.Text + "' where Uniquename='" + uniquename + "'";
                    n.ExecuteNonQuery();

                    //UPDATE SURNAME
                    SqlCommand s = con.CreateCommand();
                    s.CommandText = "update Person_Table set surname='" + textBox8.Text + "' where Uniquename='" + uniquename + "'";
                    s.ExecuteNonQuery();

                    //UPDATE ADDRESS    
                    SqlCommand a = con.CreateCommand();
                    a.CommandText = "update Person_Table set address='" + textBox9.Text + "' where Uniquename='" + uniquename + "'";
                    a.ExecuteNonQuery();

                    //UPDATE EU
                    ///////////////////////////////////////////////////////
                    if(textBox1.Text != "")
                    {
                        eu = eu + int.Parse(textBox1.Text);
                    }
                    if(textBox4.Text != "")
                    {
                        eu = eu - int.Parse(textBox4.Text);
                    }
                    SqlCommand EU = con.CreateCommand();
                    EU.CommandText = "update Person_Table set eu='" + eu + "' where Uniquename='" + uniquename + "'";
                    EU.ExecuteNonQuery();
                    ///////////////////////////////////////////////////////

                    //UPDATE FR
                    ///////////////////////////////////////////////////////
                    if (textBox2.Text != "")
                    {
                        fr = fr + int.Parse(textBox2.Text);
                    }
                    if(textBox5.Text != "")
                    {
                        fr = fr - int.Parse(textBox5.Text);
                    }
                    SqlCommand FR = con.CreateCommand();
                    FR.CommandText = "update Person_Table set fr='" + fr + "' where Uniquename='" + uniquename + "'";
                    FR.ExecuteNonQuery();
                    ///////////////////////////////////////////////////////

                    //UPDATE DEN
                    ///////////////////////////////////////////////////////
                    if (textBox3.Text != "")
                    {
                        den = den + int.Parse(textBox3.Text);
                    }
                    if(textBox6.Text != "")
                    {
                        den = den - int.Parse(textBox6.Text);
                    }
                    SqlCommand DEN = con.CreateCommand();
                    DEN.CommandText = "update Person_Table set den='" + den + "' where Uniquename='" + uniquename + "'";
                    DEN.ExecuteNonQuery();
                    ///////////////////////////////////////////////////////

                    MessageBox.Show("updated");
                }

                //connection close
                con.Close();
                
                //Refresh the page
                this.Hide();
                MainPage mp = new MainPage();
                mp.Show();
                mp.Hide();
                UpdatePerson up = new UpdatePerson();
                up.Show();
            }
            catch
            {
                MessageBox.Show("Please select a person");
            }
        }    
    }
}
