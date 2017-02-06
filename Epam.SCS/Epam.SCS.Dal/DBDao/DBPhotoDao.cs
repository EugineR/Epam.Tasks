using Epam.SCS.DalContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.SCS.Entities;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace Epam.SCS.Dal.DBDao
{
    public class DBPhotoDao : IPhotoDao
    {
        private static string connectionStr;
        public DBPhotoDao()
        {
            connectionStr = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        }
        public bool AddUsersPhoto(int ID, Photo photo)
        {
            try
            {
                int newImageID = SavePhoto(photo);
                using (var connection = new SqlConnection(connectionStr))
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "AddUserPhoto";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@imageID", newImageID);
                    cmd.Parameters.AddWithValue("@id", ID);

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

        public Photo GetFullImage(int ID)
        {
            try
            {
                using (var connection = new SqlConnection(connectionStr))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "GetFullImage";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", ID);

                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return new Photo((byte[])reader["Image"], (string)reader["ContentType"]);
                    }
                    return null;
                }
            }
            catch (Exception e)
            {
                Log.For(this).Error(e);
                throw e;
            }
        }

        public Photo GetSmallPhoto(int ID)
        {
            var photo = GetFullImage(ID);
            try
            {
                var image = ImageHelper.Resize(Image.FromStream(new MemoryStream(photo.Data)), 32, 32, reduceOnly: true);
                var memStr = new MemoryStream();
                image.Save(memStr, GetFormat(photo.ContentType));

                photo.Data = memStr.ToArray();
                return photo;
            }
            catch
            {
                return photo;
            }
        }
        public Photo GetNormalPhoto(int ID)
        {
            var photo = GetFullImage(ID);
            try
            {
                var image = ImageHelper.Resize(Image.FromStream(new MemoryStream(photo.Data)), 100, 100, reduceOnly: true);
                var memStr = new MemoryStream();
                image.Save(memStr, GetFormat(photo.ContentType));

                photo.Data = memStr.ToArray();
                return photo;
            }
            catch
            {
                return photo;
            }
        }

        private static ImageFormat GetFormat(string format)
        {

            var extension = format.Split('/').Last();
            switch (extension)
            {
                case "jpeg":
                    return ImageFormat.Jpeg;
                case "png":
                    return ImageFormat.Png;
                case "bmp":
                    return ImageFormat.Bmp;
                default:
                    return ImageFormat.Png;
            }
        }

        private static int SavePhoto(Photo photo)
        {
            try
            {
                using (var connection = new SqlConnection(connectionStr))
                {
                    MemoryStream ms = new MemoryStream(photo.Data);
                    Image image = Image.FromStream(ms);

                    var memStr = new MemoryStream();
                    image.Save(memStr, GetFormat(photo.ContentType));
                    var data = memStr.ToArray();

                    var command = connection.CreateCommand();
                    command.CommandText = "SavePhoto";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@image", data);
                    command.Parameters.AddWithValue("@contentType", photo.ContentType);

                    connection.Open();
                    return (int)(decimal)command.ExecuteScalar();
                }
            }

            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
