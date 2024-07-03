using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ThreeWheelSpareParts
{
    class UserData
    {
        public string ID { set; get; } // 0
        public string UserName { set; get; } // 1
        public string RegDate { set; get; } // 2

        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kavee\OneDrive\Documents\DB.mdf;Integrated Security=True;Connect Timeout=30");

        public List<UserData> userListData()
        {
            List<UserData> listdata = new List<UserData>();

            if (connect.State != ConnectionState.Open)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT * FROM users";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            UserData sd = new UserData();
                            sd.ID = reader["id"].ToString();
                            sd.UserName = reader["username"].ToString();

                            // Formatting date
                            DateTime regDate;
                            if (DateTime.TryParse(reader["date_register"].ToString(), out regDate))
                            {
                                sd.RegDate = regDate.ToString("d/M/yyyy");
                            }
                            else
                            {
                                sd.RegDate = string.Empty;
                            }

                            listdata.Add(sd);
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
