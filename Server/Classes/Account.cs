using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Lidgren.Network;

namespace Server
{
    public class Account
    {
        public int ID;
        public string Name;
        public string Password;
        public string EmailAddress;
        public string LastLogin;
        public string AccountKey;
        public NetConnection netConnection;
        private static Random RND = new Random();

        public Account() { }

        public Account(NetConnection connection)
        {
            netConnection = connection;
        }
        
        public Account(string name, string pass, string emailAddress, NetConnection connection)
        {
            Name = name;
            Password = pass;
            EmailAddress = emailAddress;
            netConnection = connection;
        }

        public Account(string name, string pass, string emailAddress, string lastLogin, string accountKey, NetConnection connection)
        {
            Name = name;
            Password = pass;
            EmailAddress = emailAddress;
            LastLogin = lastLogin;
            AccountKey = accountKey;
            netConnection = connection;
        }

        public void CreateAccountInDatabase()
        {
            string sqlCommand = "INSERT INTO ACCOUNTS (USERNAME,PASSWORD,EMAIL_ADDRESS,LAST_LOGIN,ACCOUNT_KEY) VALUES (@name,@password,@email,(SELECT FORMAT(CURRENT_TIMESTAMP, 'yyyy-dd-MM HH:mm:ss.fff', 'en-US')),@key)";
            string connection = @"Data Source=FDESKTOP-01\SFORTUNESQL;Initial Catalog=KRYPT;Integrated Security=True";
            AccountKey = Key(25);
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                {
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = Name;
                    cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = Password;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = EmailAddress;
                    cmd.Parameters.Add("@key", SqlDbType.VarChar).Value = AccountKey;
                    cmd.ExecuteNonQuery();
                }
                Logging.WriteLog("[DB Insert] : " + sqlCommand);
            }
        }

        public void LoadAccountFromDatabase(int id)
        {
            string sqlCommand = "SELECT * FROM ACCOUNTS WHERE ID = @id";
            string connection = @"Data Source=FDESKTOP-01\SFORTUNESQL;Initial Catalog=KRYPT;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                Logging.WriteLog("[DB Insert] : " + sqlCommand);
                using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ID = reader.GetInt32(0);
                            Name = reader.GetString(1);
                            Password = reader.GetString(2);
                            EmailAddress = reader.GetString(3);
                            LastLogin = reader.GetString(4);
                            AccountKey = reader.GetString(5);
                        }
                    }
                }
            }
        }

        public void UpdateAccountInDatabase()
        {
            string sqlCommand = "UPDATE ACCOUNTS SET USERNAME = @name,PASSWORD = @password,EMAIL_ADDRESS = @email,LAST_LOGIN = @lastlogin,ACCOUNT_KEY = @key WHERE ID = @id";
            string connection = @"Data Source=FDESKTOP-01\SFORTUNESQL;Initial Catalog=KRYPT;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                {
                    cmd.Parameters.Add("@name", SqlDbType.Text).Value = Name;
                    cmd.Parameters.Add("@password", SqlDbType.Text).Value = Password;
                    cmd.Parameters.Add("@email", SqlDbType.Text).Value = EmailAddress;
                    cmd.Parameters.Add("@lastlogin", SqlDbType.Text).Value = LastLogin;
                    cmd.Parameters.Add("@key", SqlDbType.Text).Value = AccountKey;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                    cmd.ExecuteNonQuery();
                }
                Logging.WriteLog("[DB Insert] : " + sqlCommand);
            }
        }

        public void GetIdFromDatabase(string name)
        {
            string sqlCommand = "SELECT * FROM ACCOUNTS WHERE USERNAME = @name";
            string connection = @"Data Source=FDESKTOP-01\SFORTUNESQL;Initial Catalog=KRYPT;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                Logging.WriteLog("[DB Insert] : " + sqlCommand);
                using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                {
                    cmd.Parameters.Add("@name", SqlDbType.Int).Value = name;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ID = reader.GetInt32(0);
                        }
                    }
                }
            }
        }

        static string Key(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[RND.Next(s.Length)]).ToArray());
        }
    }
}
