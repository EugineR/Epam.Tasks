using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.SCS.Entities
{
    public class User
    {
        public int Id { get; set; }
        public Account Acc { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Photo Photo { get; set; }

        public int Age
        {
            get
            {
                DateTime dateNow = DateTime.Now;
                int age = dateNow.Year - this.DateOfBirth.Year;
                if (dateNow.Month < this.DateOfBirth.Month ||
                    (dateNow.Month == this.DateOfBirth.Month && dateNow.Day < this.DateOfBirth.Day)) age--;
                return age;
            }
        }

        public IDictionary<Skill,int> UsersSkills { get; set; }
        
    }
}