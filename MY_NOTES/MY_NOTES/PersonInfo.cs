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
    public partial class PersonInfo : Form
    {
        //DataBase Connection
        public static SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Agni\Documents\MyNotesDataBase.mdf;Integrated Security=True;Connect Timeout=30");

        public PersonInfo()
        {
            InitializeComponent();
        }

        private void PersonInfo_Load(object sender, EventArgs e)
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
            //Selecting A Person From ComboBox
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
                textBox4.Text = dr["eu"].ToString();
                textBox5.Text = dr["fr"].ToString();
                textBox6.Text = dr["den"].ToString();
                textBox7.Text = dr["date"].ToString();
            }

            con.Close();
            ////////////////////////////////////////////////////
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainPage mp = new MainPage();
            this.Hide();
            mp.Show();
        }

        private void moreInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void deletedPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            DeletedPersonInfo dpi = new DeletedPersonInfo();
            dpi.Show();
        }

        private void returnDeletedPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            ReturnDeletedPerson rdp = new ReturnDeletedPerson();
            rdp.Show();
        }
    }
}
