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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace gas_station
{
    public partial class imports : Form
    {
        static String sql = "Data Source= ze-laptop\\sqlexpress ; Initial Catalog= gas_station ; Integrated Security = True ; User ID ='' ; Password ='' ";
        SqlConnection con = new SqlConnection(sql);
        public imports()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //load database
        private void Form4_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = LoadImports();
        }

        //function to connect database to form
        public DataTable LoadImports()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM importsTable";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            return dt;
        }

        //function to show data from database in text box
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            txtFuel80Price.Text= dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtFuel92Price.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtFuel95Price.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtDate.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        

        //button to delete rows from database
        private void btnDelete_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "DELETE FROM importsTable WHERE opration_num=" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Deleted Successfully");
        }

        //button to search in database by date
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM importsTable WHERE date LIKE '%" + txtSearch.Text + "%' ";
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
            dataGridView1.DataSource = LoadImports();
        }

        //Button to modify data from database
        private void btnModify_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "UPDATE importsTable SET fuel_80=@fuel_80 , fuel_92=@fuel_92 , fuel_95=@fuel_95 , date=@date WHERE opration_num =" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@fuel_80", txtFuel80Price.Text);
            cmd.Parameters.AddWithValue("@fuel_92", txtFuel92Price.Text);
            cmd.Parameters.AddWithValue("@fuel_95", txtFuel95Price.Text);
            cmd.Parameters.AddWithValue("@date", txtDate.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Done");
        }

        //function to calculate total price

        public class TotalPrice
        {
            private int total = 0, fuel80 = 0, fuel92 = 0, fuel95 = 0, liter80 = 0, liter92 = 0, liter95 = 0;
           
            private object txtFuel80;
            
            private object txtFuel92;
            
            private object txtFuel95;

            public TotalPrice()
            {
            }

            public void Calculate()
            {
                try
                {
                    if (int.TryParse(txtFuel80.Text, out liter80))
                    {
                        if (btnFuel80.Checked)
                        {
                            fuel80 = 7 * liter80;
                        }
                    }
                    if (int.TryParse(txtFuel92.Text, out liter92))
                    {
                        if (btnFuel92.Checked)
                        {
                            fuel92 = 9 * liter92;
                        }
                    }
                    if (int.TryParse(txtFuel95.Text, out liter95))
                    {
                        if (btnFuel95.Checked)
                        {
                            fuel95 = 10 * liter95;
                        }
                    }
                    total = fuel80 + fuel92 + fuel95;
                    MessageBox.Show("Fuel 80 =" + fuel80 + "\nFuel 92  =" + fuel92 + "\nFuel 95 =" + fuel95 + "\n total =" + total);
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }

            public int GetTotal()
            {
                return total;
            }
        }


        //button to save data in database
        private void btnSave_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "INSERT INTO importsTable ( fuel_80 , fuel_92 , fuel_95 , date ) VALUES ( @fuel_80 , @fuel_92 , @fuel_95 , @date )";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@fuel_80", fuel80);
            cmd.Parameters.AddWithValue("@fuel_92", fuel92);
            cmd.Parameters.AddWithValue("@fuel_95", fuel95);
            cmd.Parameters.AddWithValue("@date", txtDate.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Done");
            txtFuel80.Clear();
            txtFuel92.Clear();
            txtFuel95.Clear();
        }
    }
}
