using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace StudentTerminal.Models
{
    public record class Student
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public int Courses { get; set; }
        public int Credits { get; set; }
        public double Average { get; set; }

        public Student(string id, string name, int age, string email, int courses, int credits, double average)
        {
            Id = id;
            Name = name;
            Age = age;
            Email = email;
            Courses = courses;
            Credits = credits;
            Average = average;
        }

        public override string? ToString()
        {
            Type studentType = this.GetType();

            PropertyInfo[] properties = studentType.GetProperties();

            string studentInfo = "";

            foreach (PropertyInfo item in properties)
            {
                studentInfo += $"[{item.GetValue(this)}]";
            }

            return studentInfo;
        }
    }
}
