using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;


namespace Test.Database
{
    public class DataWays
    {
        private readonly IConfiguration _configuration;

        public DataWays(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DataSet Select(string mysql)
        {
            using (MySqlConnection con = new MySqlConnection(_configuration["Connecting:Mysql"]))
            {
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(mysql, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                //con.Close();
                //con.Dispose();
                return ds;
            }
        }
    }
}
