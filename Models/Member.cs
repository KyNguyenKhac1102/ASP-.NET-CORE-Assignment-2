using System;

namespace ASP_CORE_MVC.Models
{
    public class Member
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string BirthPlace { get; set; }

        public bool IsGraduated { get; set; }
        public int Age
        {
            get { return calcAge(); }
            set { }
        }
        public int calcAge()
        {
            int surplus = 0;
            int years = DateTime.Now.Year - DateOfBirth.Year;
            int months = DateTime.Now.Month - DateOfBirth.Month;
            int days = DateTime.Now.Day - DateOfBirth.Day;

            if (((days == 0 || days > 0) && months == 0) || (months > 0))
            {
                surplus = 1;
            }

            return years + surplus;
        }
        

        public string FullName()
        {
            return LastName + " " + FirstName;
        }

    }
}
