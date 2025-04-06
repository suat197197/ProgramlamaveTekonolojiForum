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
        public SqlConnection BaglantiGetir()
        {
            try
            {
                string ConString = @"Server=DESKTOP-6UUIFOR\SQLEXPRESS; Database=proteknoforum; Trusted_Connection=True;TrustServerCertificate=True;";
                SqlConnection connection = new SqlConnection(ConString);
                connection.Open();
                return connection;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
