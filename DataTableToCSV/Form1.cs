using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace DataTableToCSV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //start here
            callSP1();
            callSP2();
            GetCSV2();
            button1.Text = "GENERATE";
            MessageBox.Show("YOUR CSV Generated Successfully!");

        }

        private string GetCSV2()
        {
            using (SqlConnection cn = new SqlConnection(GetConnectionString()))
            {
                cn.Open();

                SqlCommand cmd = new SqlCommand("abc_sbc", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {

                    //string file = "C:\\Xero\\ExportedDataSB.csv";
                    string file = @"\\192.168.1.20\D$\CSV1\ExportedDataSB.csv";
                    List<string> lines = new List<string>();

                    while (rdr.Read())
                    {
                        object[] values = new object[rdr.FieldCount];
                        rdr.GetValues(values);
                        lines.Add(string.Join(",", values));
                    }

                    //create file
                    System.IO.File.WriteAllLines(file, lines);
                    return file;
                }
            }

        }

        private void callSP1()
        {
            using (SqlConnection cn = new SqlConnection(GetConnectionString()))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("abc_trey", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

            }
        }

        private void callSP2()
        {
            using (SqlConnection cn = new SqlConnection(GetConnectionString()))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("abc_andres", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

            }
        }


        private string CreateCSV(IDataReader reader)
        {
            string file = "C:\\Xero\\ExportedData.csv";
            List<string> lines = new List<string>();

            //data
            while (reader.Read())
            {
                object[] values = new object[reader.FieldCount];
                reader.GetValues(values);
                lines.Add(string.Join(",", values));
            }

            //create file
            System.IO.File.WriteAllLines(file, lines);

            return file;
        }

        private string GetConnectionString()
        {
            return "Data Source=DEVPC-MON;Initial Catalog=SydCosmetics1;Integrated Security=True;User ID=;Password=";
        }

    }
}