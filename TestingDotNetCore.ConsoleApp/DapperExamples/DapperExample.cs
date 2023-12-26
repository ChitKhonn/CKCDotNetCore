using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TestingDotNetCore.ConsoleApp.Model;

namespace TestingDotNetCore.ConsoleApp.DapperExamples
{
    internal class DapperExample
    {

        private readonly SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
        {
            DataSource = "CKC\\SQLEXPRESS",
            InitialCatalog = "CKCDotNetCore",
            UserID = "sa",
            Password = "asd123!@#"
        };
        public void Run() 
        {
            //Read();
            //Edit(1);
            //Create("One Piece", "CKC", "about pirates");
            //Update(4,"One Piece Episode 1000", "CKC", "about pirates");
            Delete(1);
        }

        public void Read()
        {
            using IDbConnection db = new SqlConnection(builder.ConnectionString);
            
                 string query = @"SELECT [Blog_Id]
      ,[Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content]
  FROM [dbo].[Tbl_Blog]";

                List<BlogDataModel> lst = db.Query<BlogDataModel>(query).ToList();
                foreach (BlogDataModel item in lst)
                {
                    Console.WriteLine(item.Blog_Id);
                    Console.WriteLine(item.Blog_Title);
                    Console.WriteLine(item.Blog_Author);
                    Console.WriteLine(item.Blog_Content);
                }
                 
            
        }

        public void Edit(int id)
        {
            using IDbConnection db = new SqlConnection(builder.ConnectionString);

            string query = @"SELECT [Blog_Id]
      ,[Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content]
  FROM [dbo].[Tbl_Blog] where Blog_Id = @Blog_Id";

            //BlogDataModel? data = db.Query<BlogDataModel>(query, new {Blog_Id = id}).FirstOrDefault();
            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel {Blog_Id = id}).FirstOrDefault();

            if (item is null)
            {
                Console.WriteLine("No data found!");
                return;
            }

            Console.WriteLine(item.Blog_Id);
            Console.WriteLine(item.Blog_Title);
            Console.WriteLine(item.Blog_Author);
            Console.WriteLine(item.Blog_Content);

        }

        private void Create(string title, string author, string content)
        {

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
      ([Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content])
  VALUES
        (@Blog_Title,
        @Blog_Author,
        @Blog_Content)";

            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };

            using IDbConnection db = new SqlConnection(builder.ConnectionString);
            int result = db.Execute(query, blog);


            string message = result > 0 ? "Saving Successful." : "Saving Failed";

            Console.WriteLine(message);


        }
        private void Update(int id, string title, string author, string content)
        {
            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [Blog_Title] = @Blog_Title
      ,[Blog_Author] = @Blog_Author
      ,[Blog_Content] = @Blog_Content
	WHERE Blog_Id = @Blog_Id";

            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Id = id,
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };

            using IDbConnection db = new SqlConnection(builder.ConnectionString);
            int result = db.Execute(query, blog);


            string message = result > 0 ? "Updating Successful." : "Updating Failed";

            Console.WriteLine(message);


        }

        public void Delete(int id)
        {
            using IDbConnection db = new SqlConnection(builder.ConnectionString);

            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE Blog_Id = @Blog_Id";

            //BlogDataModel? data = db.Query<BlogDataModel>(query, new {Blog_Id = id}).FirstOrDefault();
            //BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel { Blog_Id = id }).FirstOrDefault();

            int result = db.Execute(query, new BlogDataModel { Blog_Id = id });

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed";

            Console.WriteLine(message);


        }


    }
}
