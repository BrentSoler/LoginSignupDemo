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

namespace LoginSignupDataBasePushTest
{
    public partial class Form2 : Form
    {
        public static string Gender,userName,passWord = string.Empty;
        public static bool GenderIsOthers = false;
        public Form2()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Gender = comboBox1.Text;
            if (comboBox1.Text == "Others")
            {
                textBox3.Visible = true;
                GenderIsOthers = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Gender = GenderIsOthers ? textBox3.Text : comboBox1.Text;
            userName = textBox1.Text;
            passWord = textBox2.Text;

            bool completeFields = userName != string.Empty & passWord != string.Empty & Gender != string.Empty;

            var func = completeFields ? (Action)CompleteField : (Action)IncompleteField;

            func();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Close();
        }

        public void CompleteField()
        {
            Console.WriteLine("true");

            SqlConnection con = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\Brent\\Desktop\\Programs\\C#\\app_data\\Database1.mdf;Integrated Security=True");
            con.Open();
            string query = "INSERT INTO users values(@un,@pw,@gen)";
            string userNameCheck = "select * from users where userName ='" +userName.Trim()+ "'";

            SqlDataAdapter adapt = new SqlDataAdapter(userNameCheck, con);
            DataTable table = new DataTable();
            adapt.Fill(table);

            if (table.Rows.Count == 1)
            {
                MessageBox.Show("Username Already exist");
                return;
            }

            SqlParameter param1 = new SqlParameter("@un", userName);
            SqlParameter param2 = new SqlParameter("@pw", passWord);
            SqlParameter param3 = new SqlParameter("@gen", Gender);

            SqlCommand comm = new SqlCommand(query, con);
            comm.Parameters.Add(param1);
            comm.Parameters.Add(param2);
            comm.Parameters.Add(param3);

            comm.ExecuteNonQuery();

            MessageBox.Show("NICE");
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";

            con.Close();
            
        }

        public void IncompleteField()
        {
            MessageBox.Show("EROR");
        }
    }
}
