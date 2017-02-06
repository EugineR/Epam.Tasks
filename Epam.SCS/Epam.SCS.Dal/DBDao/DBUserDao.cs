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
    public class DBUserDao : IUserDao
    {
        private static string connectionStr;

        static DBUserDao()
        {
            connectionStr = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        }

        public bool Add(User user)
        {
            try
            {
                using (var connection = new SqlConnection(connectionStr))
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "AddUser";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@login", user.Acc.Login);
                    cmd.Parameters.AddWithValue("@password", user.Acc.Password);
                    cmd.Parameters.AddWithValue("@name", user.Name);
                    cmd.Parameters.AddWithValue("@surname", user.Surname);
                    if (user.Patronymic == String.Empty)
                    {
                        cmd.Parameters.AddWithValue("@patronymic", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@patronymic", user.Patronymic);
                    }
                    cmd.Parameters.AddWithValue("@dateOfBirth", user.DateOfBirth);
                    cmd.Parameters.Add(new SqlParameter("@Id", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.Output });
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

        public bool AddSkillToUser(int userID, int skillID, int rate)
        {
            try
            {
                using (var connection = new SqlConnection(connectionStr))
                {
                    try
                    {
                        SqlCommand cmd = connection.CreateCommand();
                        cmd.CommandText = "AddSkillToUser";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@userID", userID);
                        cmd.Parameters.AddWithValue("@skillID", skillID);
                        cmd.Parameters.AddWithValue("@rate", rate);
                        connection.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                    catch
                    {
                        throw new InvalidOperationException("User already has the same skill!");
                    }
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
                    cmd.CommandText = "UserContains";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
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

        public IEnumerable<User> GetAll()
        {
            try
            {
                using (var connection = new SqlConnection(connectionStr))
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "GetAllUsers";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    connection.Open();
                    var users = new List<User>();

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int Id = (int)reader["Id"];
                        string patronymic = reader["Patronymic"] is DBNull ? String.Empty : (string)reader["Patronymic"];
                        string name = (string)reader["Name"];
                        string surname = (string)reader["Surname"];
                        DateTime dateOfBirth = (DateTime)reader["DateOfBirth"];
                        int imageID = (int)reader["Image_ID"];
                        string login = (string)reader["Login"];
                        string password = (string)reader["Password"];
                        string role = (string)reader["RoleName"];
                        var acc = new Account(Id, login, password, role);
                        IDictionary<Skill, int> usersskills = GetUsersSkills(Id);

                        users.Add(new User
                        {
                            Acc = acc,
                            Name = name,
                            Surname = surname,
                            Patronymic = patronymic,
                            DateOfBirth = dateOfBirth,
                            Id = Id,
                            Photo = new Photo(imageID),
                            UsersSkills = usersskills
                        });
                    }
                    return users;
                }
            }
            catch (Exception e)
            {
                Log.For(this).Error(e);
                throw e;
            }
        }

        public User GetByID(int id)
        {
            try
            {
                using (var connection = new SqlConnection(connectionStr))
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "GetUserByID";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    connection.Open();

                    var reader = cmd.ExecuteReader();
                    User user = null;

                    while (reader.Read())
                    {
                        int Id = (int)reader["Id"];
                        string patronymic = reader["Patronymic"] is DBNull ? String.Empty : (string)reader["Patronymic"];
                        string name = (string)reader["Name"];
                        string surname = (string)reader["Surname"];
                        DateTime dateOfBirth = (DateTime)reader["DateOfBirth"];
                        int imageID = (int)reader["Image_ID"];
                        string login = (string)reader["Login"];
                        string password = (string)reader["Password"];
                        string role = (string)reader["RoleName"];
                        var acc = new Account(Id, login, password, role);
                        IDictionary<Skill, int> usersskills = GetUsersSkills(Id);
                        user = new User
                        {
                            Acc = acc,
                            Name = name,
                            Surname = surname,
                            Patronymic = patronymic,
                            DateOfBirth = dateOfBirth,
                            Id = Id,
                            Photo = new Photo(imageID),
                            UsersSkills = usersskills
                        };
                    }
                    return user;
                }
            }
            catch (Exception e)
            {
                Log.For(this).Error(e);
                throw e;
            }
        }

        public IDictionary<Skill, int> GetUsersSkills(int ID)
        {
            try
            {
                using (var connection = new SqlConnection(connectionStr))
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "GetUsersSKillsByID";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", ID);
                    connection.Open();

                    IDictionary<Skill, int> skillWithRate = new Dictionary<Skill, int>(new SkillEqualityComprarer());
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        skillWithRate.Add(new Skill()
                        {
                            Id = (int)reader["ID"],
                            Title = (string)reader["Title"]
                        },
                            (int)reader["Rate"]);

                    }
                    return skillWithRate;
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
        private string GetRole(string login)
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

        public bool RemoveUser(int id)
        {
            try
            {
                using (var connection = new SqlConnection(connectionStr))
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "RemoveUser";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    User user = GetByID(id);
                    if (GetRole(user.Acc.Login).Equals("Admin"))
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
                            throw new InvalidOperationException("Cannot remove the last admin's role");
                        }
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


        public bool RemoveUsersSkill(int userID, int skillID)
        {
            try
            {
                using (var connection = new SqlConnection(connectionStr))
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "RemoveUsersSkill";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userID", userID);
                    cmd.Parameters.AddWithValue("@skillID", skillID);
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

        public bool Update(int id, User user)
        {
            try
            {
                using (var connection = new SqlConnection(connectionStr))
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "UpdateUserInfo";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", user.Name);
                    cmd.Parameters.AddWithValue("@surname", user.Surname);
                    cmd.Parameters.AddWithValue("@dateOfBirth", user.DateOfBirth);

                    if (user.Patronymic == String.Empty)
                    {
                        cmd.Parameters.AddWithValue("@patronymic", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@patronymic", user.Patronymic);
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
    }
}
