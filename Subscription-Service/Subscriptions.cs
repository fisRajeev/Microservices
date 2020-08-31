using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SubscriptionService.Models;

namespace SubscriptionService
{
    public class Subscriptions
    {

        private string GetConnectionString()
        {
            var dbconfig = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json").Build();

            return dbconfig["ConnectionStrings:DefaultConnection"];
        }
        public Subscription[] GetSubscriptions()
        {
            List<Subscription> subs = new List<Subscription>();
            try
            {
                string dbconnectionStr = GetConnectionString();
                string sql = "select * from Subscription";
                using (SqlConnection connection = new SqlConnection(dbconnectionStr))
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    connection.Open();
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            Subscription sub = new Subscription();
                            sub.subscriberName = Convert.ToString(dataReader["Subscriber_Name"]);
                            sub.bookId = Convert.ToInt32(dataReader["BookID"]);
                            sub.dateSubscribed = Convert.ToDateTime(dataReader["Date_Subscribed"]);
                            sub.dateReturned = Convert.ToDateTime(dataReader["Date_Returned"]);
  
                            subs.Add(sub);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return subs.ToArray();
        }
        public Subscription GetSubscriptionByName(string name)
        {
            Subscription sub = null;
            try
            {
                string dbconnectionStr = GetConnectionString();
                string sql = "select * from Subscription where Subscriber_Name=@sname";
                using (SqlConnection connection = new SqlConnection(dbconnectionStr))
                {
                    SqlParameter param = new SqlParameter("@sname", name);
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.Add(param);
                    connection.Open();
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            sub = new Subscription();
                            sub.subscriberName = Convert.ToString(dataReader["Subscriber_Name"]);
                            sub.bookId = Convert.ToInt32(dataReader["BookID"]);
                            sub.dateSubscribed = Convert.ToDateTime(dataReader["Date_Subscribed"]);
                            sub.dateReturned = Convert.ToDateTime(dataReader["Date_Returned"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return sub;
        }
    }
}
