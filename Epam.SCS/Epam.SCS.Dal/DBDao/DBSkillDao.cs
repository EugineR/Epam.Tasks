using Epam.SCS.DalContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.SCS.Entities;
using System.Configuration;
using System.Data.SqlClient;

namespace Epam.SCS.Dal.DBDao
{
    public class DBSkillDao : ISkillDao
    {
        public static string connectionStr;
        public DBSkillDao()
        {
            connectionStr = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        }
        public bool Add(Skill skill)
        {
            try
            {
                using (var connection = new SqlConnection(connectionStr))
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "INSERT INTO dbo.[Skill] (Title) VALUES (@title); SELECT scope_identity()";
                    cmd.Parameters.AddWithValue("@title", skill.Title);
                    connection.Open();

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
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
                    cmd.CommandText = "SELECT ID FROM dbo.[Skill] WHERE ID=@id";
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

        public IEnumerable<Skill> GetAll()
        {
            try
            {
                var skills = new List<Skill>();
                using (var connection = new SqlConnection(connectionStr))
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT Title, ID FROM dbo.Skill";
                    connection.Open();

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int Id = (int)reader["Id"];
                        string title = (string)reader["Title"];

                        skills.Add(new Skill { Title = title, Id = Id });
                    }
                    return skills;
                }
            }
            catch (Exception e)
            {
                Log.For(this).Error(e);
                throw e;
            }
        }

        public IEnumerable<Skill> GetSkillsByIDs(int[] IDs)
        {
            try
            {
                var skills = new List<Skill>();
                for (int i = 0; i < IDs.Length; i++)
                {
                    skills.Add(this.GetByID(IDs[i]));
                }
                return skills;
            }
            catch (Exception e)
            {
                Log.For(this).Error(e);
                throw e;
            }
        }

        public Skill GetByID(int id)
        {
            try
            {
                using (var connection = new SqlConnection(connectionStr))
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = " SELECT ID, Title FROM dbo.[Skill] WHERE ID=@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    connection.Open();

                    var reader = cmd.ExecuteReader();
                    Skill skill = null;

                    while (reader.Read())
                    {
                        int Id = (int)reader["Id"];
                        string title = (string)reader["Title"];
                        skill = new Skill { Title = title, Id = Id };
                    }
                    return skill;
                }
            }
            catch (Exception e)
            {
                Log.For(this).Error(e);
                throw e;
            }
        }

        public bool Remove(int id)
        {
            try
            {
                using (var connection = new SqlConnection(connectionStr))
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM dbo.[Skill] WHERE ID=@id";
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

        public bool Update(int id, Skill skill)
        {
            try
            {
                using (var connection = new SqlConnection(connectionStr))
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "UPDATE dbo.[Skill] SET Title=@title WHERE ID=@id";

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@title", skill.Title);

                    connection.Open();

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception e)
            {
                Log.For(this).Error(e);
                throw e;
            }
        }

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }
        public int GetRateById(int id)
        {
            try
            {
                using (var connection = new SqlConnection(connectionStr))
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = " SELECT Rate FROM dbo.[UsersSkills] WHERE ID=@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    connection.Open();

                    var reader = cmd.ExecuteReader();
                    int rate = 0;

                    while (reader.Read())
                    {
                        rate = (int)reader["Rate"];
                    }
                    return rate;
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
