using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows.Markup;

namespace Crime_App
{
    internal class Crime_Data_Processing
    {
        private string connection_string = "Data Source = crime_db.db;version=3";

        public double CalculateAverage()
        {
            double average = 0;
            string query = "SELECT AVG(CRIMECOUNT) FROM CRIME_DATA";
            using (var connection = new SQLiteConnection(connection_string))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    average = Convert.ToDouble(command.ExecuteScalar());

                }
            }
            return average;
        }

        public double Calculate_Total()
        {
            double total = 0;
            string query = "SELECT SUM(CRIMECOUNT) FROM CRIME_DATA";
            using (var connection = new SQLiteConnection(connection_string))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    total = Convert.ToDouble(command.ExecuteScalar());

                }
            }
            return total;
        }

        public double Total_Casualties()
        {
            double total = 0;
            string query = "SELECT POPULATION FROM CRIME_DATA WHERE CRIMETYPE = 'All Reported'";
            using (var connection = new SQLiteConnection(connection_string))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    total = Convert.ToDouble(command.ExecuteScalar());

                }
            }
            return total;
        }

        public double Average_Casualties()
        {
            int count = 0;
            int population = 0;
            double total = 0;
            string query = "SELECT POPULATION,COUNT(POPULATION) AS COUNT FROM CRIME_DATA WHERE CRIMETYPE = 'All Reported'";
            using (var connection = new SQLiteConnection(connection_string))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                             population = Convert.ToInt32(reader["POPULATION"]);
                             count = Convert.ToInt32(reader["COUNT"]);
                       
                        }
                    }
                   

                }
            }
            return population/count;
        }

        public List<Tuple<int,int>> Get_Data()
        {
            List<Tuple<int, int>> Data_For_Chart = new List<Tuple<int, int>>();
            using (SQLiteConnection connection = new SQLiteConnection(connection_string))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand($"SELECT YEAR,CRIMECOUNT FROM CRIME_DATA",connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int year = Convert.ToInt32(reader["YEAR"]);
                            int crimeCount;
                            if (reader["crimecount"] != DBNull.Value)
                            {
                                crimeCount = Convert.ToInt32(reader["crimecount"]);
                            }
                            else
                            {
                                crimeCount = 0; 
                            }
                            Data_For_Chart.Add(Tuple.Create(year, crimeCount));
                        }
                    }
                }
            }
            return Data_For_Chart;
        }

        public List<Tuple<string, int>> Get_Data_For_Pie_Chart()
        {
            List<Tuple<string, int>> Data_For_Chart1 = new List<Tuple<string, int>>();
            using (SQLiteConnection connection = new SQLiteConnection(connection_string))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand($"SELECT WEAPON,COUNT(WEAPON) FROM CRIME_DATA GROUP BY WEAPON", connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                string weapon = (string)reader["WEAPON"];
                                int count = reader.GetInt32(1);
                                Data_For_Chart1.Add(Tuple.Create(weapon, count));
                            }
                            
                        }
                    }
                }
            }
            return Data_For_Chart1;
        }

        public List<Tuple<string, int>> Get_Data_For_Cartesian_Chart_1()
        {
            List<Tuple<string, int>> Data_For_Chart1 = new List<Tuple<string, int>>();
            using (SQLiteConnection connection = new SQLiteConnection(connection_string))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand($"SELECT CRIMETYPE,COUNT(CRIMETYPE) FROM CRIME_DATA GROUP BY CRIMETYPE", connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                string weapon = (string)reader["CRIMETYPE"];
                                int count = reader.GetInt32(1);
                                Data_For_Chart1.Add(Tuple.Create(weapon, count));
                            }

                        }
                    }
                }
            }
            return Data_For_Chart1;
        }

        public List<Tuple<string, int>> Get_Data_For_Cartesian_Chart_2()
        {
            List<Tuple<string, int>> Data_For_Chart1 = new List<Tuple<string, int>>();
            using (SQLiteConnection connection = new SQLiteConnection(connection_string))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand($"SELECT VICTIM_SEX,COUNT(*) FROM CRIME_DATA GROUP BY VICTIM_SEX", connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                string weapon = (string)reader["VICTIM_SEX"];
                                int count = reader.GetInt32(1);
                                Data_For_Chart1.Add(Tuple.Create(weapon, count));
                            }

                        }
                    }
                }
            }
            return Data_For_Chart1;
        }

        public List<Tuple<string, int>> Get_Data_For_Cartesian_Chart_3()
        {
            List<Tuple<string, int>> Data_For_Chart1 = new List<Tuple<string, int>>();
            using (SQLiteConnection connection = new SQLiteConnection(connection_string))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand($"SELECT AGE_GROUP,COUNT(AGE_GROUP) FROM CRIME_DATA GROUP BY AGE_GROUP", connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                string weapon = (string)reader["AGE_GROUP"];
                                int count = reader.GetInt32(1);
                                Data_For_Chart1.Add(Tuple.Create(weapon, count));
                            }

                        }
                    }
                }
            }
            return Data_For_Chart1;
        }
    }
}
