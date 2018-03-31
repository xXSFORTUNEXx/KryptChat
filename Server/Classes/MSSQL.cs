using System;
using System.Data.SqlClient;
using System.IO;
using static System.Convert;

namespace Server
{
    public static class MSSQL
    {
        public static void DatabaseExists()
        {
            string connection = @"Data Source=FDESKTOP-01\SFORTUNESQL;Integrated Security=True";
            int result;
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                var sqlCFile = File.ReadAllText("SQL Scripts/Exists.sql");
                using (SqlCommand cmd = new SqlCommand(sqlCFile, conn))
                {
                    result = (int)cmd.ExecuteScalar();
                    Logging.WriteLog("[DB Query] : " + sqlCFile, "SQL");
                }

                if (result == Globals.NO)
                {
                    var sqlFile = File.ReadAllText("SQL Scripts/Database.sql");
                    var sqlQueries = sqlFile.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);
                    var sqlFile2 = File.ReadAllText("SQL Scripts/Tables.sql");
                    var sqlQueries2 = sqlFile2.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);
                    var sqlFile3 = File.ReadAllText("SQL Scripts/Version.sql");
                    var sqlQueries3 = sqlFile3.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);

                    using (SqlCommand cmd = new SqlCommand("query", conn))
                    {
                        foreach (var query in sqlQueries)
                        {
                            cmd.CommandText = query;
                            cmd.ExecuteNonQuery();
                            Logging.WriteLog("[DB Query] : " + query, "SQL");
                        }

                        foreach (var query in sqlQueries2)
                        {
                            cmd.CommandText = query;
                            cmd.ExecuteNonQuery();
                            Logging.WriteLog("[DB Query] : " + query, "SQL");
                        }

                        foreach (var query in sqlQueries3)
                        {
                            cmd.CommandText = query;
                            cmd.ExecuteNonQuery();
                            Logging.WriteLog("[DB Query] : " + query, "SQL");
                        }
                    }
                }
            }
        }
        public static bool AccountExist(string name)
        {
            string sqlCommand = "SELECT * FROM ACCOUNTS WHERE USERNAME=@name";
            string connection = @"Data Source=FDESKTOP-01\SFORTUNESQL;Initial Catalog=KRYPT;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                {
                    cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar).Value = name;
                    Logging.WriteLog("[DB Query] : " + sqlCommand);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string sql_Name = reader.GetString(1);
                            if (sql_Name == name)
                            {
                                return true;
                            }
                        }
                        return false;
                    }
                }
            }
        }
        public static bool CheckPassword(string name, string pass)
        {
            string sqlCommand = "SELECT * FROM ACCOUNTS WHERE USERNAME=@name";
            string connection = @"Data Source=FDESKTOP-01\SFORTUNESQL;Initial Catalog=Scorched;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                {
                    cmd.Parameters.Add("@name", System.Data.SqlDbType.Text).Value = name;
                    Logging.WriteLog("[DB Query] : " + sqlCommand);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string sql_Pass = reader.GetString(2);
                            if (sql_Pass == pass)
                            {
                                return true;
                            }
                        }
                        return false;
                    }
                }
            }
        }
    }
}
