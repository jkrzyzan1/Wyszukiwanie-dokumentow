using System.Data;

namespace raportowanie_postgres
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            foreach (var x in main.Load_Warehouses())
            {
                checkedListBox1.Items.Add(x);
            }

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
           
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable table = queries.data(this);
            dataGridView1.DataSource = table;
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count >0)
            {
                int selectedRow = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selected = dataGridView1.Rows[selectedRow];
                string cell = Convert.ToString(selected.Cells[0].Value);
                Form2 f2 = new Form2();
                f2.Show();
                DataTable source = queries.dataPositions(this, cell);
                f2.dataGridView1.DataSource = source;
        
                var test = 0;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}