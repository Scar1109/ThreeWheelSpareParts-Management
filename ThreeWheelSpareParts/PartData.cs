using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ThreeWheelSpareParts
{
    class PartData
    {

        public int ID { set; get; } // 0
        public string ProductID { set; get; } // 1
        public string ProductName { set; get; } // 2
        public string Category { set; get; } // 3
        public string SellingPrice { set; get; } // 4
        public string CostPrice { set; get; } // 5
        public string Quantity { set; get; } // 6
        public string AddedDate { set; get; } // 7


        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kavee\OneDrive\Documents\DB.mdf;Integrated Security=True;Connect Timeout=30");


        public List<PartData> partListData()
        {
            return partListData(null);
        }

        public List<PartData> partListData(string searchText)
        {
            List<PartData> listdata = new List<PartData>();

            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();
                    string selectData;

                    if (string.IsNullOrEmpty(searchText))
                    {
                        selectData = "SELECT * FROM parts WHERE delete_date IS NULL";
                    }
                    else
                    {
                        selectData = "SELECT * FROM parts WHERE (product_id LIKE @searchText OR product_name LIKE @searchText) AND delete_date IS NULL";
                    }

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        if (!string.IsNullOrEmpty(searchText))
                        {
                            cmd.Parameters.AddWithValue("@searchText", "%" + searchText + "%");
                        }

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            PartData ed = new PartData();
                            ed.ID = (int)reader["id"];
                            ed.ProductID = reader["product_id"].ToString();
                            ed.ProductName = reader["product_name"].ToString();
                            ed.Category = reader["category"].ToString();
                            ed.SellingPrice = reader["selling_price"].ToString();
                            ed.CostPrice = reader["cost_price"].ToString();
                            ed.Quantity = reader["quantity"].ToString();
                            ed.AddedDate = reader["insert_date"].ToString();

                            listdata.Add(ed);
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                }
                finally
                {
                    connect.Close();
                }
            }
            return listdata;
        }
    }
}
