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
using System.IO;

namespace ThreeWheelSpareParts
{
    public partial class AddPart : UserControl
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kavee\OneDrive\Documents\DB.mdf;Integrated Security=True;Connect Timeout=30");

        public int selectedPartId = 0;

        public AddPart()
        {
            InitializeComponent();

            // TO DISPLAY THE DATA FROM DATABASE TO YOUR DATA GRID VIEW
            displayPartData();
        }

        public void RefreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)RefreshData);
                return;
            }
            displayPartData();
        }

        public void displayPartData()
        {
            PartData ed = new PartData();
            List<PartData> listData = ed.partListData();

            dataGridView1.DataSource = listData;
        }

        private void addPart_btn_Click(object sender, EventArgs e)
        {
            if (part_id.Text == ""
                || product_name.Text == ""
                || part_category.Text == ""
                || qunatity.Text == ""
                || selling_price.Text == ""
                || cost_price.Text == "")
            {
                MessageBox.Show("Please fill all blank fields"
                    , "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (connect.State == ConnectionState.Closed)
                {
                    try
                    {
                        connect.Open();
                        string checkEmID = "SELECT COUNT(*) FROM parts WHERE product_id = @productID AND delete_date IS NULL";

                        using (SqlCommand checkEm = new SqlCommand(checkEmID, connect))
                        {
                            checkEm.Parameters.AddWithValue("@productID", part_id.Text.Trim());
                            int count = (int)checkEm.ExecuteScalar();

                            if (count >= 1)
                            {
                                MessageBox.Show(part_id.Text.Trim() + " is already taken"
                                    , "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                DateTime today = DateTime.Today;
                                string insertData = "INSERT INTO parts " +
                                    "(product_id, product_name, category, quantity" +
                                    ", selling_price, cost_price, insert_date) " +
                                    "VALUES(@productID, @productName, @category, @quantity" +
                                    ", @sellingPrice, @costPrice, @insertDate)";


                                using (SqlCommand cmd = new SqlCommand(insertData, connect))
                                {
                                    cmd.Parameters.AddWithValue("@productID", part_id.Text.Trim());
                                    cmd.Parameters.AddWithValue("@productName", product_name.Text.Trim());
                                    cmd.Parameters.AddWithValue("@category", part_category.Text.Trim());
                                    cmd.Parameters.AddWithValue("@quantity", qunatity.Text.Trim());
                                    cmd.Parameters.AddWithValue("@sellingPrice", selling_price.Text.Trim());
                                    cmd.Parameters.AddWithValue("@costPrice", cost_price.Text.Trim());
                                    cmd.Parameters.AddWithValue("@insertDate", today);

                                    cmd.ExecuteNonQuery();

                                    displayPartData();

                                    MessageBox.Show("Added successfully!"
                                        , "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    clearFields();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex
                    , "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                part_id.Text = row.Cells[1].Value.ToString();
                product_name.Text = row.Cells[2].Value.ToString();
                part_category.Text = row.Cells[3].Value.ToString();
                cost_price.Text = row.Cells[4].Value.ToString();
                selling_price.Text = row.Cells[5].Value.ToString();
                qunatity.Text = row.Cells[6].Value.ToString();
                selectedPartId = Convert.ToInt32(row.Cells[0].Value);
            }
        }

        public void clearFields()
        {
            part_id.Text = "";
            product_name.Text = "";
            part_category.SelectedIndex = -1;
            qunatity.Text = "";
            cost_price.Text = "";
            selling_price.Text = "";
        }

        private void part_updateBtn_Click(object sender, EventArgs e)
        {
            if (part_id.Text == ""
                || product_name.Text == ""
                || part_category.Text == ""
                || qunatity.Text == ""
                || cost_price.Text == ""
                || selling_price.Text == "")
            {
                MessageBox.Show("Please fill all blank fields"
                    , "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult check = MessageBox.Show("Are you sure you want to UPDATE " +
                    "Product ID: " + part_id.Text.Trim() + "?", "Confirmation Message"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (check == DialogResult.Yes)
                {
                    try
                    {
                        connect.Open();
                        DateTime today = DateTime.Today;

                        string updateData = "UPDATE parts SET product_name = @productName" +
                            ", category = @category,quantity = @quantity,product_id = @productId, selling_price = @sellingPrice" +
                            ", cost_price = @costPrice, update_date = @updateDate WHERE id = @id";

                        using (SqlCommand cmd = new SqlCommand(updateData, connect))
                        {
                            cmd.Parameters.AddWithValue("@id", selectedPartId);
                            cmd.Parameters.AddWithValue("@productName", product_name.Text.Trim());
                            cmd.Parameters.AddWithValue("@category", part_category.Text.Trim());
                            cmd.Parameters.AddWithValue("@quantity", qunatity.Text.Trim());
                            cmd.Parameters.AddWithValue("@sellingPrice", selling_price.Text.Trim());
                            cmd.Parameters.AddWithValue("@costPrice", cost_price.Text.Trim());
                            cmd.Parameters.AddWithValue("@productId", part_id.Text.Trim());
                            cmd.Parameters.AddWithValue("@updateDate", today);

                            cmd.ExecuteNonQuery();

                            displayPartData();

                            MessageBox.Show("Update successfully!"
                                , "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                            clearFields();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex
                        , "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Cancelled."
                        , "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        public void displayFilteredPartData(string searchText)
        {
            PartData ed = new PartData();
            List<PartData> listData = ed.partListData(searchText);
            dataGridView1.DataSource = listData;
        }

        private void part_deleteBtn_Click(object sender, EventArgs e)
        {
            if (part_id.Text == ""
                || product_name.Text == ""
                || part_category.Text == ""
                || qunatity.Text == ""
                || cost_price.Text == ""
                || selling_price.Text == "")
            {
                MessageBox.Show("Please fill all blank fields"
                    , "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult check = MessageBox.Show("Are you sure you want to DELETE " +
                    "Product ID: " + part_id.Text.Trim() + "?", "Confirmation Message"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (check == DialogResult.Yes)
                {
                    try
                    {
                        connect.Open();
                        DateTime today = DateTime.Today;

                        string updateData = "UPDATE parts SET delete_date = @deleteDate " +
                            "WHERE product_id = @productID";

                        using (SqlCommand cmd = new SqlCommand(updateData, connect))
                        {
                            cmd.Parameters.AddWithValue("@deleteDate", today);
                            cmd.Parameters.AddWithValue("@productID", part_id.Text.Trim());

                            cmd.ExecuteNonQuery();

                            displayPartData();

                            MessageBox.Show("Update successfully!"
                                , "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            clearFields();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex
                        , "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Cancelled."
                        , "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void addEmployee_fullName_TextChanged(object sender, EventArgs e)
        {

        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void search_text_input_OnChnage(object sender, EventArgs e)
        {

        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            displayFilteredPartData(searchInput.Text.Trim());
        }
    }
}
