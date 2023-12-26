using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingDotNetCore.ConsoleApp.AdoDotNetExamples
{
    internal class AdoDotNetExample
    {

        public void Run()
        {
            //Read();
            //Edit(1);
            //Create("World", "CKC", "about love xD");
            //Update(1,"update title", "update author", "update content");
            Delete(3);
        }

        private void Read()
        {

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
            {
                DataSource = "CKC\\SQLEXPRESS",
                InitialCatalog = "CKCDotNetCore",
                UserID = "sa",
                Password = "asd123!@#"
            };
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            Console.WriteLine("Connnection opening...");
            connection.Open();
            Console.WriteLine("Connection Opend");


            string query = @"SELECT [Blog_Id]
      ,[Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content]
  FROM [dbo].[Tbl_Blog]";

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            Console.WriteLine("Connnection close...");
            connection.Close();
            Console.WriteLine("Connection Close");

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine("Id is " + dr["Blog_Id"]);
                Console.WriteLine("Blog_Title is " + dr["Blog_Title"]);
                Console.WriteLine("Blog_Author is " + dr["Blog_Author"]);
                Console.WriteLine("Blog_Content is " + dr["Blog_Content"]);
                Console.WriteLine("-------------");

            }
        }

        private void Edit(int id)
        {

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
            {
                DataSource = "CKC\\SQLEXPRESS",
                InitialCatalog = "CKCDotNetCore",
                UserID = "sa",
                Password = "asd123!@#"
            };
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            Console.WriteLine("Connnection opening...");
            connection.Open();
            Console.WriteLine("Connection Opend");


            string query = @"SELECT [Blog_Id]
      ,[Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content]
  FROM [dbo].[Tbl_Blog] where Blog_Id = @Blog_Id" ;

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_id", id);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            Console.WriteLine("Connnection close...");
            connection.Close();
            Console.WriteLine("Connection Close");

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No data found!");
                return;
            }

            DataRow dr = dt.Rows[0];
           
                Console.WriteLine("Id is " + dr["Blog_Id"]);
                Console.WriteLine("Blog_Title is " + dr["Blog_Title"]);
                Console.WriteLine("Blog_Author is " + dr["Blog_Author"]);
                Console.WriteLine("Blog_Content is " + dr["Blog_Content"]);
                Console.WriteLine("-------------");

            
        }

        private void Create(string title, string author, string content) 
        {

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
            {
                DataSource = "CKC\\SQLEXPRESS",
                InitialCatalog = "CKCDotNetCore",
                UserID = "sa",
                Password = "asd123!@#"
            };
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            Console.WriteLine("Connnection opening...");
            connection.Open();
            Console.WriteLine("Connection Opend");


            string query = @"INSERT INTO [dbo].[Tbl_Blog]
      ([Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content])
  VALUES
        (@Blog_Title,
         @Blog_Author,
         @Blog_Content)";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Title", title);
            command.Parameters.AddWithValue("@Blog_Author", author);
            command.Parameters.AddWithValue("@Blog_Content", content);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            int result = command.ExecuteNonQuery();

            Console.WriteLine("Connnection close...");
            connection.Close();
            Console.WriteLine("Connection Close");

            string message = result > 0 ? "Saving Successful." : "Saving Failed";
           
            Console.WriteLine(message);


        }
        private void Update(int id, string title, string author, string content)
        {

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
            {
                DataSource = "CKC\\SQLEXPRESS",
                InitialCatalog = "CKCDotNetCore",
                UserID = "sa",
                Password = "asd123!@#"
            };
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            Console.WriteLine("Connnection opening...");
            connection.Open();
            Console.WriteLine("Connection Opend");


            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [Blog_Title] = @Blog_Title
      ,[Blog_Author] = @Blog_Author
      ,[Blog_Content] = @Blog_Content
	WHERE Blog_Id = @Blog_Id";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Id", id);
            command.Parameters.AddWithValue("@Blog_Title", title);
            command.Parameters.AddWithValue("@Blog_Author", author);
            command.Parameters.AddWithValue("@Blog_Content", content);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            int result = command.ExecuteNonQuery();

            Console.WriteLine("Connnection close...");
            connection.Close();
            Console.WriteLine("Connection Close");

            string message = result > 0 ? "Updating Successful." : "Updating Failed";

            Console.WriteLine(message);


        }

        private void Delete(int id)
        {

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
            {
                DataSource = "CKC\\SQLEXPRESS",
                InitialCatalog = "CKCDotNetCore",
                UserID = "sa",
                Password = "asd123!@#"
            };
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            Console.WriteLine("Connnection opening...");
            connection.Open();
            Console.WriteLine("Connection Opend");


            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE Blog_Id = @Blog_Id";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_id", id);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            int result = command.ExecuteNonQuery();

            Console.WriteLine("Connnection close...");
            connection.Close();
            Console.WriteLine("Connection Close");

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed";

            Console.WriteLine(message);



        }
    }

}
