using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gas_station
{
    public partial class sales : Form
    {

        static String sql = "Data Source= ze-laptop\\sqlexpress ; Initial Catalog= gas_station ; Integrated Security = True ; User ID ='' ; Password ='' ";
        SqlConnection con = new SqlConnection(sql);


        private object result2;
        private object result1;
        private object result3;

        public sales()
        {

            InitializeComponent();
        }

        

        //reset data
        private void btnReset_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;
            radioButton6.Checked = false;
            txtLiter.Text = string.Empty;
            txtLiter.Focus();
            Motor.Checked = false;
            Cooling_system.Checked = false;
            Tires.Checked = false;
            Air_filter.Checked = false;
            Differential.Checked = false;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        //button to save data in database
        private void btnSave_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "INSERT INTO salesTable ( fuel_cost , car_service_cost , car_wash_cost , date ) VALUES ( @fuel_cost , @car_service_cost , @car_wash_cost , @date )";
            SqlCommand cmd = new SqlCommand(query,con);
            cmd.Parameters.AddWithValue("@fuel_cost", result2);
            cmd.Parameters.AddWithValue("@car_service_cost", result1);
            cmd.Parameters.AddWithValue("@car_wash_cost", result3);
            cmd.Parameters.AddWithValue("@date", txtDate.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Done");
            
            
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {

        }

        //function to calculate total price

        public class TotalPrice
        {
            private int total = 0, result1 = 0, result2 = 0, liter = 0, result3 = 0;
            private object txtLiter;
            private object radioButton1;
            private object radioButton2;
            private object radioButton3;
            private object radioButton4;
            private object radioButton5;
            private object radioButton6;

            public TotalPrice()
            {
            }

            public object Motor { get; private set; }
            public object Cooling_system { get; private set; }
            public object Tires { get; private set; }
            public object Air_filter { get; private set; }
            public object Differential { get; private set; }

            public void Calculate()
            {
                if (Motor.Checked)
                {
                    result1 = result1 + 4000;
                }
                if (Cooling_system.Checked)
                {
                    result1 = result1 + 1000;
                }
                if (Tires.Checked)
                {
                    result1 = result1 + 1500;
                }
                if (Air_filter.Checked)
                {
                    result1 = result1 + 800;
                }
                if (Differential.Checked)
                {
                    result1 = result1 + 1500;
                }

                try
                {
                    if (int.TryParse(txtLiter.Text, out liter))
                    {
                        if (radioButton1.Checked)
                        {
                            result2 = 9 * liter;
                        }
                        else if (radioButton2.Checked)
                        {
                            result2 = 11 * liter;
                        }
                        else if (radioButton3.Checked)
                        {
                            result2 = 12 * liter;
                        }

                        if (radioButton4.Checked)
                        {
                            result3 = result3 + 300;
                        }
                        else if (radioButton5.Checked)
                        {
                            result3 = result3 + 500;
                        }
                        else if (radioButton6.Checked)
                        {
                            result3 = result3 + 100;
                        }
                        total = result1 + result2;
                        MessageBox.Show("Car Service Cost =" + result1 + "\n fuel Cost =" + result2 + "\n Car Wash Cost =" + result3 + "\n Total price =" + total);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    txtLiter.Clear();
                    txtLiter.Focus();
                }
            }

            public int GetTotal()
            {
                return total;
            }
        }

    
        ///function to connect database to form
        public DataTable LoadSales()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM salesTable";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            return dt;
        }
    }
}
