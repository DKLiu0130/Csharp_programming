using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace windows_app_practice
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public class DatabaseHelper
    {
        private string connectionString = "Server=localhost;Database=UserDatabase;User ID=root;Password=112233aabb;";

        // 插入数据到数据库
        public void InsertUser(string name, string email)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Users (Name, Email) VALUES (@Name, @Email)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("User inserted successfully.");
        }

        // 从数据库中显示所有用户
        public bool CheckUsers(string name, string email)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // SQL query to check if the user exists
                    string query = "SELECT COUNT(*) FROM users WHERE name = @name AND email = @email";

                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        // Define parameters to avoid SQL injection
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@email", email);

                        // Execute the query and get the count of matching records
                        int userCount = Convert.ToInt32(cmd.ExecuteScalar());

                        // Output based on the result
                        if (userCount > 0)
                        {
                            MessageBox.Show("Login successfully!");
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Name and email failed to match! Try again!");
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                    return false; 
                }
            }
        }
    }
}
