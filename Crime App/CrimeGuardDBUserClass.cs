using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Packaging;
using System.Windows;
using System.Xml.Serialization;


namespace Crime_App
{
    internal class SQLDB
    {
        private string Connection_String = "Data Source=CrimeGuardUsers.sqlite;Version=3;";

        public void Initialize_Database()
        {
            Connection_String = "Data Source=CrimeGuardUsers.sqlite;Version=3;";
            SQLiteConnection.CreateFile("CrimeGuardUsers.sqlite");
        }
        public void Create_Tables()
        {
            using(SQLiteConnection connection =  new SQLiteConnection(Connection_String))
            {
                connection.Open();
                string UserTable1 = @"
                     CREATE TABLE IF NOT EXISTS USERS(
                      USERNAME VARCHAR(255) PRIMARY KEY,
                      EMAIL VARCHAR(255) NOT NULL,
                      PASSWORD VARCHAR(255) NOT NULL
                      );";
                using (SQLiteCommand command = new SQLiteCommand(UserTable1,connection))
                {
                    command.ExecuteNonQuery();

                }
                connection.Close();
            }
        }
        public void InsertValues(string username,string email,string password)
        {
            using (SQLiteConnection connection = new SQLiteConnection(Connection_String))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand("INSERT INTO USERS(USERNAME,EMAIL,PASSWORD) VALUES (@username,@email,@password)", connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);
                    command.ExecuteNonQuery();
                }
            }
        }

        public bool Username_Check(string username)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(Connection_String))
                {
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand("SELECT COUNT(*) FROM USERS AS U WHERE U.USERNAME=@username", connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        int count_username = Convert.ToInt32(command.ExecuteScalar());
                        if (count_username == 0)
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show("An error occured while checking for the availability of username" + ex.Message);
                return false;
            }
        }

        public bool Password_check(string username,string password)
        {
            using (SQLiteConnection connection = new SQLiteConnection(Connection_String))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand("SELECT COUNT(*) FROM USERS AS U WHERE U.PASSWORD=@password AND U.USERNAME=@username", connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password",password);
                    int return_value = Convert.ToInt32(command.ExecuteScalar());
                    if (return_value == 1)
                    {
                        return true;
                    }
                    else
                    return false;
                    
                } 
            }
        }
    }
}
