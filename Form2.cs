using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gas_station
{
    public partial class systemData : Form
    {

       static String sql = "Data Source= ze-laptop\\sqlexpress ; Initial Catalog= gas_station ; Integrated Security = True ; User ID ='' ; Password ='' ";
        SqlConnection con = new SqlConnection(sql);

        public systemData()
        {
            InitializeComponent();
        }

        //button to go to imports form
        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            imports form4 = new imports();
            form4.ShowDialog();
            form4 = null;
            this.Show();
        }

        private void txtDate_TextChanged(object sender, EventArgs e)
        {

        }

        //reload form
        private void Form2_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = LoadSales();

        }

        //button to go to sales form
        private void btnSale_Click(object sender, EventArgs e)
        {
            this.Hide();
            sales form3 = new sales();
            form3.ShowDialog();
            form3 = null;
            this.Show();
        }

        //function to connect database to form
        public DataTable LoadSales()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM salesTable" ;
            con.Open();
            SqlCommand cmd = new SqlCommand(query,con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            return dt;
        }

        //button to search in database by date
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM salesTable WHERE date LIKE '%"+txtSearch.Text+ "%' ";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            dataGridView1.DataSource = dt;
        }

        //button to refresh 
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource=LoadSales();
        }

        //Button to delete rows
        private void btnDelete_Click(object sender, EventArgs e)
        {
            
            con.Open();
            string query = "DELETE FROM salesTable WHERE operation_num=" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Deleted Successfully");
        }

        //Button to modify data from database
        private void btnModify_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "UPDATE salesTable SET fuel_cost=@fuel_cost , car_service_cost=@car_service_cost , car_wash_cost=@car_wash_cost , date=@date WHERE operation_num =" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@fuel_cost", txtFuel.Text);
            cmd.Parameters.AddWithValue("@car_service_cost", txtCarService.Text);
            cmd.Parameters.AddWithValue("@car_wash_cost", txtCarWash.Text);
            cmd.Parameters.AddWithValue("@date", txtDate.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Done");
        }

        //function to show data from database in text box
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            txtDate.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtFuel.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtCarService.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtCarWash.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }
    }
}
