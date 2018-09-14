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
    public partial class DeletePerson : Form
    {
        //Variable Declaration
        string uniquename, name, surname, address, date;
        int EU, FR, DEN;

        public static SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Agni\Documents\MyNotesDataBase.mdf;Integrated Security=True;Connect Timeout=30");
        public DeletePerson()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainPage mp = new MainPage();
            this.Hide();
            mp.Show();
        }
       

        private void DeletePerson_Load(object sender, EventArgs e)
        {
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
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Function For Selecting Item In ComboBox
       void selectPerson()
        {
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
                textBox1.Text = dr["name"].ToString();
                textBox2.Text = dr["surname"].ToString();
                textBox3.Text = dr["address"].ToString();
            }
            con.Close();
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Using The Function For Item Selection From ComboBox
            selectPerson();
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Funtion For Saving The Person To DeletedPersonDataBase
        void InsertToDeletedPersonDataBase()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Person_Table where Uniquename='" + comboBox1.SelectedItem.ToString() +"'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                uniquename = dr["uniquename"].ToString();
                name = dr["name"].ToString();
                surname = dr["surname"].ToString();
                address = dr["address"].ToString();
                EU = int.Parse(dr["EU"].ToString());
                FR = int.Parse(dr["FR"].ToString());
                DEN = int.Parse(dr["DEN"].ToString());
                date = dr["date"].ToString();
            }
            con.Close();
            con.Open();
            SqlCommand cmmd = con.CreateCommand();
            cmmd.CommandType = CommandType.Text;
            cmmd.CommandText = "insert into Deleted1Person_Table values('" + uniquename + "','" + name + "','" + surname + "','" + address + "','" + EU + "','" + FR + "','" + DEN + "','" + date + "')";
            cmmd.ExecuteNonQuery();
            con.Close();          
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Funtion For Deleting A Person From DataBase
        void deletePerson()
        {
            //Using The Function For Saving The Person To DeletedPersonDataBase
            InsertToDeletedPersonDataBase();
            ////////////////////////////////
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from Person_Table where Uniquename='" + comboBox1.SelectedItem.ToString() + "'";
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Record deleted successfully");
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Funtion For Checking The Uniquename in DeletedPersonDataBase
        private bool CU()
        {
            int a = 0;
            //Checking int If The Person Exist In DataBase
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Deleted1Person_Table ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["Uniquename"].ToString() == this.comboBox1.SelectedItem.ToString())
                {
                    a++;
                }
            }
            con.Close();
            //If Unique Name Exist Then The Person Can Not Be Saved
            if (a!=0)
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
                //If The Function Return True 
                if (CU())
                {
                    MessageBox.Show("This person can not be deleted because this uniquename exist in DeletedPersonDataBase, please change the uniquename");
                    this.Hide();
                    UpdatePerson up = new UpdatePerson();
                    up.Show();
                }
                else
                {
                    //Using The Function For Deleting A Person From DataBase
                    deletePerson();
                    ///////////////
                    //This logic is used to refresh the page
                    //Because after person is deleted its name remains in combobox 
                    this.Hide();
                    MainPage mp = new MainPage();
                    mp.Show();
                    mp.Hide();
                    DeletePerson dp = new DeletePerson();
                    dp.Show();
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("Select one person");
            }
        }
    }
}
