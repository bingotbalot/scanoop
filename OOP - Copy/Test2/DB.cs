// This is our database
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test2
{
    internal class DB
    {
        public NpgsqlConnection conn = new NpgsqlConnection("Host = localhost; Port = 5433; Database =postgres ;User id =postgres; Password = password;");

        
        public NpgsqlDataReader selectQuery(String tableName, String[] fields = null)
        {
            String query = "";
            if(fields == null) {
                query = "*";
            } else
            {
 
                for(int i = 0; i <= fields.Length - 1; i++) 
                {
                    if (i == fields.Length - 1)
                    {
                        query += fields[i];
                    } else {
                        query += fields[i] + ", ";
                    }
                }
            }
            Debug.WriteLine(query);

            if (conn.State != ConnectionState.Open) {
                conn.Open();
            }

            NpgsqlCommand comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "SELECT CONCAT(fname, ' ', lname) AS Fullname, department, date, STRING_AGG(DISTINCT time, ' - ') AS attendance FROM tbl_record GROUP BY Fullname , department , date ORDER BY date DESC limit 10";
            NpgsqlDataReader dr = comm.ExecuteReader();
            comm.Dispose();
            return dr;
        }

        public int insertQuery(String tableName, String[] fields = null, Object[] values = null) {
            
            if(conn.State!= ConnectionState.Open) {
                conn.Open();
            }

            String fieldQuery = "";
            String valuesQuery = "";

            if(fields.Length != values.Length)
            {
                return 0;
            }

            if (fields == null)
            {
                return 0;
            } else {
                for (int i = 0; i <= fields.Length - 1; i++)
                {
                    if (i == fields.Length - 1)
                    {
                        fieldQuery += fields[i];
                        valuesQuery += "@"+fields[i];
                    }
                    else
                    {
                        fieldQuery += fields[i] + ", ";
                        valuesQuery += "@"+fields[i] + ", ";
                    }
                }
            }


            NpgsqlCommand comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "INSERT INTO " + tableName + "("+ fieldQuery + ") VALUES (" + valuesQuery + ")";
            


            for (int i = 0; i <= fields.Length - 1; i++)
            {
                comm.Parameters.AddWithValue("@" + fields[i], values[i]);
                

            }

            int res = comm.ExecuteNonQuery();

            comm.Dispose();
            conn.Close();

            return res;
        }




    }
}
