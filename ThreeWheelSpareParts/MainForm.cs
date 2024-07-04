using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreeWheelSpareParts
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to sign out?"
                , "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(check == DialogResult.Yes)
            {
                Form1 loginForm = new Form1();
                loginForm.Show();
                this.Hide();
            }
        }

        private void dashboard_btn_Click(object sender, EventArgs e)
        {
            dashboard1.Visible = true;
            addPart.Visible = false;
            salary1.Visible = false;

            Dashboard dashForm = dashboard1 as Dashboard;

            if(dashForm != null)
            {
                dashForm.RefreshData();
            }

        }

        private void addPart_btn_Click(object sender, EventArgs e)
        {
            dashboard1.Visible = false;
            addPart.Visible = true;
            salary1.Visible = false;

            AddPart addPartForm = addPart as AddPart;

            if (addPartForm != null)
            {
                addPartForm.RefreshData();
            }
        }

        private void greet_user_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void user_btn_Click(object sender, EventArgs e)
        {
            dashboard1.Visible = false;
            addPart.Visible = false;
            salary1.Visible = true;

            Users salaryForm = salary1 as Users;

            if (salaryForm != null)
            {
                salaryForm.RefreshData();
            }
        }
    }
}