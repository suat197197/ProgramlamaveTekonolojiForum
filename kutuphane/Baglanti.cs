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
        public SqlConnection connection;
        public Baglanti()
        {
            try
            {
                string ConString = @"Server=DESKTOP-6UUIFOR\SQLEXPRESS; Database=proteknoforum; Trusted_Connection=True;";
                SqlConnection connection = new SqlConnection(ConString);
                connection.Open();
            }
            catch (Exception)
            {

                throw;
            }
        }
        ~Baglanti()
        {
            try
            {
                connection.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }



    }
}
