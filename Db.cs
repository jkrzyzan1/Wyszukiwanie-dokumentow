using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Npgsql;
using System.Data.SqlClient;

namespace raportowanie_postgres
{
    public class Db
    {
        public static string connString = "Host=178.33.51.202:7432;Username=mastersport;Password=HYG-ied-835-FRE;Database=vjyj";

        //metoda zapisująca tabelę do csv
        public static void ToCsv(DataTable Dt, string pathInput)
        {
            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = Dt.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName);
            sb.AppendLine(string.Join(";", columnNames));

            foreach (DataRow row in Dt.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(";", fields));
            }

            try
            {
                File.WriteAllText(pathInput, sb.ToString());
                MessageBox.Show("Zapisano pomyślnie");
            }
            catch
            {
                MessageBox.Show("Wystąpił błąd");
            }
        }

        public static DataTable Table(string query)
        {
            using var conn = new NpgsqlConnection(connString);
            using var cmd = new NpgsqlCommand(query, conn);
            DataTable dt = new DataTable();
            var dataAdapter = new NpgsqlDataAdapter(cmd);
            dataAdapter.Fill(dt);
            dataAdapter.Dispose();
            conn.Close();
            return dt;
        }
        public static List<string> ListQuery(string query)
        {
            using var conn = new NpgsqlConnection(connString);
            using var cmd = new NpgsqlCommand(query, conn);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            List<string> data = new List<string>();
            while (reader.Read())
            {
                data.Add(reader.GetString(0));
            }
            conn.Close();
            return data;
        }

        /*public static string ServerQuery(string query)
        {
            string connString = "Server=94.127.105.206,21433;Database=integrator;User=kamil;Password = 6sRmAmP1";
            string result = "";
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    result = reader[0].ToString();
                }

                connection.Close();
            }

            return result;

        }
        */
    }
}
