using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
namespace kutuphane
{
    public class Baglanti
    {
        public Baglanti()
        {
            ConString = @"Server=DESKTOP-6UUIFOR\SQLEXPRESS; Database=proteknoforum; Trusted_Connection=True;";
        }
        public static string ConString;
        public SqlConnection BaglantiGetir()
        {
            SqlConnection connection = new SqlConnection(ConString);
            connection.Open();
            return connection;
        }
    }
}
