using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace ThreeWheelSpareParts
{
    public partial class Users : UserControl
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kavee\OneDrive\Documents\DB.mdf;Integrated Security=True;Connect Timeout=30");

        public Users()
        {
            InitializeComponent();
            user_password.PasswordChar = showPwd_checkBox.Checked ? '\0' : '*';
            displayUsers();
        }

        public void RefreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)RefreshData);
                return;
            }

            displayUsers();
        }

        public void displayUsers()
        {
            UserData ed = new UserData();
            List<UserData> listData = ed.userListData();

            dataGridView1.DataSource = listData;
        }
        
        private void addUserBtn_Click(object sender, EventArgs e)
        {
            if( user_username.Text == ""
                || user_password.Text == "")
            {
                MessageBox.Show("Please fill all blank fields", "Error Message"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult check = MessageBox.Show("Are you sure do you want to add this user " 
                    + "?", "Confirmation Message"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if(check == DialogResult.Yes)
                {
                    if(connect.State == ConnectionState.Closed)
                    {
                        try
                        {
                            connect.Open();
                            DateTime today = DateTime.Today;

                            string updateData = "INSERT INTO users(username,password,date_register) VALUES(@userName, @password, @regDate)";

                            using(SqlCommand cmd = new SqlCommand(updateData, connect))
                            {
                                cmd.Parameters.AddWithValue("@userName", user_username.Text.Trim());
                                cmd.Parameters.AddWithValue("@password", user_password.Text.Trim());
                                cmd.Parameters.AddWithValue("@regDate", today);

                                cmd.ExecuteNonQuery();

                                displayUsers();

                                MessageBox.Show("User Added sucessfully!"
                                    , "Information Message", MessageBoxButtons.OK
                                    , MessageBoxIcon.Information);

                                clearFields();

                            }
                        }catch(Exception ex)
                        {
                            MessageBox.Show("Error: " + ex, "Error Message"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            connect.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Cancelled", "Information Message"
                    , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        public void clearFields()
        {
            user_username.Text = "";
            user_password.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                user_username.Text = row.Cells[1].Value.ToString();
                user_password.Text = row.Cells[2].Value.ToString();
            }
        }

        private void salary_clearBtn_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void showPwd_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            user_password.PasswordChar = showPwd_checkBox.Checked ? '\0' : '*';
        }
    }
}
