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
    public partial class Form3 : Form
    {
        public static string userName, pass = string.Empty;
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            userName = textBox1.Text;
            pass = textBox2.Text;

            bool completecheck = userName != string.Empty & pass != string.Empty;

            var func = completecheck ? (Action)Check : (Action)Error;

            func();
        }

        public void Check()
        {
            SqlConnection con = new SqlConnection("Data Source = SQL5063.site4now.net; Initial Catalog=db_a7c0e1_users;User Id=db_a7c0e1_users_admin;Password=testing123");
            con.Open();
            string query = "select * from users where userName='" + userName.Trim() + "' and userPass='" + pass.Trim() + "'";
            SqlDataAdapter adapt = new SqlDataAdapter(query,con);
            DataTable table = new DataTable();
            adapt.Fill(table);
            if (table.Rows.Count != 1)
            {
                MessageBox.Show("Incorect Credentials");
                return;
            }
            Program.name = userName;
            Form4 frm4 = new Form4();
            this.Hide();
            frm4.Show();

            con.Close();
        }

        public void Error()
        {
            MessageBox.Show("Complete Fields Please");
        }
    }
}
