using Epam.SCS.LogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.SCS.Entities;
using Epam.SCS.DalContracts;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Epam.SCS.Logic
{
    public class UserLogic : IUserLogic
    {
        private IUserDao userDao;
        private ISkillDao skillDao;
        public UserLogic()
        {
            userDao = DaoProvider.UserDao;
            skillDao = DaoProvider.SkillDao;
        }

        public void Add(User user)
        {
            user.Acc.Password = GetPasswordsHash(user.Acc.Password);
            userDao.Add(user);
        }

        public bool AddSkillToUser(int userID, int skillID, int rate)
        {
            return userDao.AddSkillToUser(userID, skillID, rate);
        }

        public bool Contains(int id)
        {
            return userDao.Contains(id);
        }

        public User GetByID(int id)
        {
            return userDao.GetByID(id);
        }

        public bool Update(int id, User user)
        {
            if (user.DateOfBirth > DateTime.Today)
            {
                throw new ArgumentException("Date of Birth can't be above than current date");
            }

            if (user.Age > 150)
            {
                throw new ArgumentException("User's age cant' be above than 150 years");
            }
            return userDao.Update(user.Id, user);
        }

        private static string GetPasswordsHash(string input)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }

        public IEnumerable<User> GetAll()
        {
            return userDao.GetAll().ToArray();
        }

        public bool RemoveUsersSkill(int userID, int skillID)
        {
            return userDao.RemoveUsersSkill(userID, skillID);
        }

        public bool RemoveUser(int id)
        {
            return userDao.RemoveUser(id);
        }
        public int GetIDByAccID(int ID)
        {
            return userDao.GetIDByAccID(ID);
        }

        public void CreateReportAboutAllUsers()
        {
            var doc = new Document();
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PDFs");
            PdfWriter.GetInstance(doc, new FileStream(Path.Combine(path, "Order_about_all_users.pdf"), FileMode.Create));
            PdfPTable table = new PdfPTable(3);


            string ttf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");
            var baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            var font = new Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);

            table.TotalWidth = 510f;
            //fix the absolute width of the table
            table.LockedWidth = true;

            //relative col widths in proportions - 1/3 and 2/3
            float[] widths = new float[] { 6f, 1f, 3f };
            table.SetWidths(widths);
            table.HorizontalAlignment = 0;
            //leave a gap before and after the table
            table.SpacingBefore = 20f;
            table.SpacingAfter = 30f;

            PdfPCell cell = new PdfPCell(new Phrase(new Chunk("Information about all the users knowledge of accounting systems.", font)));
            cell.Colspan = 3;
            cell.Border = 0;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);


            table.AddCell("Name");
            table.AddCell("Age");
            table.AddCell("Skills");

            foreach (var user in this.GetAll())
            {
                table.AddCell(new Phrase(new Chunk($"{user.Name}  {user.Surname}  {user.Patronymic}", font)));
                table.AddCell(user.Age.ToString());
                StringBuilder skillsStr = new StringBuilder();
                if (user.UsersSkills.Count == 0)
                {
                    skillsStr.Append("The user has no skills");
                }
                else
                {
                    foreach (var skill in user.UsersSkills)
                    {
                        skillsStr.Append($"{skill.Key.Title} - {skill.Value} \r");
                    }
                }
                table.AddCell(new Phrase(new Chunk(skillsStr.ToString(), font)));
            }
            doc.Open();
            doc.Add(table);
            doc.Close();
        }
    }
}
