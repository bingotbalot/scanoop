using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using static Test2.DB;
namespace Test2
{
    public partial class Form1 : Form

    {

        List<Panel> listPanel = new List<Panel>();
        int index;
        public int login = 0;

        public Form1()
        {
            InitializeComponent();
            loadData();
            listPanel.Add(panel1);
            listPanel.Add(panel2);
            listPanel[index].BringToFront();
            timer1.Start();
            
        }

        private void buttonsave_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            String[] fieldsArgs = { "fname", "lname", "department", "time", "date" };
            Object[] valuesArgs = { textBoxfname.Text, textBoxlname.Text, comboBox1.Text, textBoxtime.Text, textBoxDate.Text };
            int res = db.insertQuery("tbl_record", fieldsArgs, valuesArgs);
            textBoxfname.Text = "";
            textBoxlname.Text = "";
            comboBox1.Text = "";
            textBoxtime.Text = "";
            textBoxDate.Text = "";
            loadData();
        }

        private void loadData()
        {
            DB db = new DB();
            db.conn.Open();
            String[] args = { "id_no", "fname", "lname", "department", "time","date" };
            NpgsqlDataReader dr = db.selectQuery("tbl_record", args);
            if (dr.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
            }
                dr.Close();
            {
                db.conn.Close();
            }
        }

        private void btnprev_Click(object sender, EventArgs e)
        {
            if (index > 0)
                listPanel[--index].BringToFront();
        }

        private void btnext_Click(object sender, EventArgs e)
        {
            if (index < listPanel.Count - 1)
                listPanel[++index].BringToFront();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DB db = new DB();
            db.conn.Open();
            NpgsqlCommand comm = new NpgsqlCommand("SELECT NOW()", db.conn);
            NpgsqlDataReader reader = comm.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                DateTime time = reader.GetDateTime(0);
                label5.Text = time.ToLongTimeString();
            }
                reader.Close();
            {
                db.conn.Close();
            }
            label6.Text = DateTime.Now.ToString("MMM dd yyyy");
            label7.Text = DateTime.Now.ToString("dddd");
        
        }
        private void buttontime_Click(object sender, EventArgs e)
        {
            int login = 0;
            while (login < 1)
            {
                label6.Text = DateTime.Now.ToString("dd-MM-yyyy");
                if (login == 0)
                {
                    textBoxtime.Text = label5.Text;
                    textBoxDate.Text = label6.Text;
                    login++;
                }
                else
                {
                    login++;
                }
                
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox4.Image = new Bitmap(open.FileName);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.conn.Open();
            NpgsqlCommand comm = new NpgsqlCommand("SELECT * FROM tbl_record WHERE id_no=" + txtSearch.Text, db.conn);
            NpgsqlDataReader dr = comm.ExecuteReader();
            if (dr.HasRows)
               {
                 DataTable dt = new DataTable();
                 dt.Load(dr);
                 dataGridView1.DataSource = dt;
                }
            else
               { 
                 MessageBox.Show("Sorry, No record Found"); 
               }
            comm.Dispose();
        }


        /*private void button1_Click(object sender, EventArgs e)
        {

            if (rtxt.Enabled)
            {
                rtxt.Enabled = false;
            }
            else
            {
                rtxt.Enabled = true;
            }
        }*/



    }
}
