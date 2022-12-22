using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raportowanie_postgres
{
    public class main : Form1
    {
        public void Generate()
        {
            var start = dateTimePicker1.Value;
            var end = dateTimePicker2.Value;


        }

        public static List <string> Load_Warehouses()
        {
            string inputQuery = queries.warehouses();
            List <string> warehouses = Db.ListQuery(inputQuery);
            return warehouses;

        }
    }
}
