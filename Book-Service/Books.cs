using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BookService.Models;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BookService
{
    public class Books
    {
        private string GetConnectionString()
        {
            var dbconfig = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json").Build();

            return dbconfig["ConnectionStrings:DefaultConnection"];
        }
        public Book[] GetBooks()
        {
            List<Book> books = new List<Book>();
            try
            {
                string dbconnectionStr = GetConnectionString();
                string sql = "select * from Book";
                using (SqlConnection connection = new SqlConnection(dbconnectionStr))
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    connection.Open();
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            Book book = new Book();
                            book.bookID = Convert.ToInt32(dataReader["BookID"]);
                            book.bookName = Convert.ToString(dataReader["Name"]);
                            book.author = Convert.ToString(dataReader["Author"]);
                            book.total_copies = Convert.ToInt16(dataReader["Total_Copies"]);
                            book.available_copies = Convert.ToInt16(dataReader["Available_Copies"]);
                            books.Add(book);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return books.ToArray();
        }
        public Book GetBookByID(int id)
        {
            Book book = null;
            try
            {
                string dbconnectionStr = GetConnectionString();
                string sql = "select * from Book where BookID=@bookid";
                using (SqlConnection connection = new SqlConnection(dbconnectionStr))
                {
                    SqlParameter param = new SqlParameter("@bookid", id);
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.Add(param);
                    connection.Open();
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            book = new Book();
                            book.bookID = Convert.ToInt32(dataReader["BookID"]);
                            book.bookName = Convert.ToString(dataReader["Name"]);
                            book.author = Convert.ToString(dataReader["Author"]);
                            book.total_copies = Convert.ToInt16(dataReader["Total_Copies"]);
                            book.available_copies = Convert.ToInt16(dataReader["Available_Copies"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
          
            return book;
        }

        public bool UpdateAvailability(int bookId, int increment_count)
        {
            int rowsImpacted = 0;
            try
            {
                string dbconnectionStr = GetConnectionString();
                string sql = "update book set Available_Copies= Available_Copies + @ac WHERE bookid=@bid";
                using (SqlConnection connection = new SqlConnection(dbconnectionStr))
                {
                    SqlParameter paramAC = new SqlParameter("@ac", increment_count);
                    SqlParameter paramBookId = new SqlParameter("@bid", bookId);
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.Add(paramAC);
                    command.Parameters.Add(paramBookId);
                    connection.Open();

                    rowsImpacted = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return rowsImpacted > 0 ? true : false;
        }
    }

    
}
