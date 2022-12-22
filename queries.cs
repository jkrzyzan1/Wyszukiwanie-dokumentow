using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raportowanie_postgres
{
    public class queries
    {
        public static string warehouses()
        {
            string query = @"select magazyn_symbol
from dm01
where usuniety = 0";
            return query;
        }

        public static DataTable data(Form1 form)
        {
            DataTable dt;
            DateTime startDate = form.dateTimePicker1.Value;
            DateTime endDate = form.dateTimePicker2.Value;
            string startDateTW = "'" + startDate.ToString() + "'";
            string endDateTW = "'" + endDate.ToString() + "'";  
            string typesTW = "";
            string warehousesTW = "";

            List<string> types = new List<string>();
            foreach(object itemChecked in form.checkedListBox2.CheckedItems)
            {
                types.Add(itemChecked.ToString().Substring(1,2));
            }

            foreach(string type in types)
            {
                typesTW += "'" + type + "',";
            }
            typesTW = typesTW.Substring(0,typesTW.Length - 1);


            List<string> warehouses = new List<string>();
            foreach(object itemChecked in form.checkedListBox1.CheckedItems)
            {
                warehouses.Add(itemChecked.ToString());
            }

            foreach(string warehouse in warehouses)
            {
                warehousesTW += "'" + warehouse + "',";
            }
            warehousesTW = warehousesTW.Substring(0, warehousesTW.Length - 1);

            string query = @$"select id_dokumentu,
numer_dokumentu,
data_dokumentu,
dk03.symbol_dokumentu,
dk01.ilosc,
magazyn_symbol,
wartosc_koncowa,
rabat
from dk01
left join dk03 on dk01.id_typu_dokumentu = dk03.id_typu_dokumentu
left join dm01 on dk01.id_magazynu = dm01.id_magazynu
where data_dokumentu between {startDateTW} and {endDateTW}
and symbol_dokumentu in ({typesTW})
and magazyn_symbol in ({warehousesTW})";

            dt = Db.Table(query);
            return dt;
        }

        public static DataTable dataPositions(Form1 form, string cell)
        {
            DataTable dt;
            string id = cell;
            string query = $@"select tw10.id_zewnetrzny_wariantu_towaru,
tw01.kod_producenta,
dk02.ilosc,
dk02.wartosc_koncowa,
tw10.kod_kreskowy
from dk01
left join dk02 on dk01.id_dokumentu = dk02.id_dokumentu
left join tw10 on tw10.id_wariantu_towaru = dk02.id_wariantu_towaru
left join tw01 on tw10.id_towaru = tw01.id_towaru
where dk01.id_dokumentu = {id}";
            dt = Db.Table(query);
            return dt;



        }
    }
}
