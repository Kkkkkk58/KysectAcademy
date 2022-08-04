﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Lab5
{
    public class Database
    {
        public SQLiteConnection myConnection;

        public Database(string path)
        {
            myConnection = new SQLiteConnection("Data Source= " + path);
        }

        public void OpenConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Open)
                myConnection.Open();
        }
        public void CloseConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Closed)
                myConnection.Close();
        }
    }
}
