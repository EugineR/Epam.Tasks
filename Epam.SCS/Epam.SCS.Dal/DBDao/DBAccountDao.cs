using Epam.SCS.DalContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.SCS.Entities;
using System.Data.SqlClient;
using System.Configuration;


namespace Epam.SCS.Dal.DBDao
{
    public class DBAccountDao : IAccountDao
    {
        private static string connectionStr;

        static DBAccountDao()
        {
            connectionStr = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        }


        public bool CanRegister(string login)
        {

            try
            {
                using (var connection = new SqlConnection(connectionStr))
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT Login FROM dbo.Account WHERE Login=@login";
                    cmd.Parameters.AddWithValue("@login", login);
                    connection.Open();

                    var reader = cmd.ExecuteReader();

                    return !reader.Read();
                }
            }
            catch (Exception e)
            {
                Log.For(this).Error(e);
                throw e;
            }
        }

        public bool CheckUser(string login, string password)
        {
            try
            {
                using (var connection = new SqlConnection(connectionStr))
                {

                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT Login, Password FROM dbo.Account WHERE (Login=@login) AND  (Password=@password)";
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@password", password);
                    connection.Open();

                    var reader = cmd.ExecuteReader();
                    return reader.Read();
                }
            }
            catch (Exception e)
            {
                Log.For(this).Error(e);
                throw e;
            }
        }

        public string GetRole(string login)
        {
            try
            {
                using (var connection = new SqlConnection(connectionStr))
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = " SELECT RoleName FROM dbo.Role WHERE ID IN (SELECT Role_ID FROM dbo.Account WHERE Login = @login)";
                    cmd.Parameters.AddWithValue("@login", login);
                    connection.Open();

                    var reader = cmd.ExecuteReader();
                    string role = "";
                    while (reader.Read())
                    {
                        role = (string)reader["RoleName"];
                    }
                    return role;
                }
            }
            catch (Exception e)
            {
                Log.For(this).Error(e);
                throw e;
            }
        }


        public bool Contains(int id)
        {
            try
            {
                using (var connection = new SqlConnection(connectionStr))
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT ID FROM dbo.[Account] WHERE ID=@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    connection.Open();

                    var reader = cmd.ExecuteReader();

                    return reader.Read();
                }
            }
            catch (Exception e)
            {
                Log.For(this).Error(e);
                throw e;
            }
        }
        private IEnumerable<string> GetAllAccountsRoles()
        {
            try
            {
                using (var connection = new SqlConnection(connectionStr))
                {
                    var roles = new List<string>();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT dbo.Role.RoleName FROM dbo.Role INNER JOIN dbo.Account ON dbo.Role.ID=dbo.Account.Role_ID";

                    connection.Open();

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string role = (string)reader["RoleName"];
                        roles.Add(role);
                    }
                    return roles;
                }
            }
            catch (Exception e)
            {
                Log.For(this).Error(e);
                throw e;
            }
        }

        public IEnumerable<Account> GetAll()
        {
            try
            {
                using (var connection = new SqlConnection(connectionStr))
                {
                    var accounts = new List<Account>();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT dbo.Account.ID,dbo.Account.Login, dbo.Role.RoleName FROM dbo.Role INNER JOIN dbo.Account ON dbo.Role.ID=dbo.Account.Role_ID";
                    connection.Open();

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int Id = (int)reader["ID"];
                        string login = (string)reader["Login"];
                        string roleName = (string)reader["RoleName"];
                        accounts.Add(new Account(Id, login, roleName));
                    }
                    return accounts;
                }
            }
            catch (Exception e)
            {
                Log.For(this).Error(e);
                throw e;
            }
        }

        public bool ChangeRole(int id)
        {
            try
            {
                using (var connection = new SqlConnection(connectionStr))
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "UPDATE dbo.[Account] SET Role_ID=@roleId WHERE ID=@id";


                    cmd.Parameters.AddWithValue("@id", id);

                    Account account = GetByID(id);
                    if (GetRole(account.Login).Equals("Admin"))
                    {
                        int countAdmins = 0;
                        string[] allAccountsRoles = GetAllAccountsRoles().ToArray();
                        foreach (var item in allAccountsRoles)
                        {
                            if (countAdmins > 1)
                                break;
                            if (item == "Admin")
                            {
                                countAdmins++;
                            }
                            else
                                continue;
                        }
                        if (countAdmins == 1)
                        {
                            throw new InvalidOperationException("Cannot change the last admin's role");
                        }
                        cmd.Parameters.AddWithValue("@roleId", 2);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@roleId", 1);
                    }
                    connection.Open();


                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception e)
            {
                Log.For(this).Error(e);
                throw e;
            }
        }

        public Account GetByID(int id)
        {
            try
            {
                using (var connection = new SqlConnection(connectionStr))
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = " SELECT ID, Login, Role_ID FROM dbo.[Account] WHERE ID=@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    connection.Open();

                    var reader = cmd.ExecuteReader();
                    Account account = null;

                    while (reader.Read())
                    {
                        int Id = (int)reader["ID"];
                        string login = (string)reader["Login"];
                        account = new Account(Id, login, GetRole(login));
                    }
                    return account;
                }
            }
            catch (Exception e)
            {
                Log.For(this).Error(e);
                throw e;
            }
        }

        public int GetIDByLogin(string login)
        {
            try
            {
                using (var connection = new SqlConnection(connectionStr))
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "GetIDByLogin";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@login", login);
                    connection.Open();

                    var reader = cmd.ExecuteReader();

                    int id = 0;
                    while (reader.Read())
                    {
                        id = (int)reader["Acc_ID"];

                    }
                    return id;
                }
            }
            catch (Exception e)
            {
                Log.For(this).Error(e);
                throw e;
            }
        }

    }
}
