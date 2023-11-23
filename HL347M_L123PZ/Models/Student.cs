using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTerminal.Models
{
    public record class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int Courses { get; set; }
        public int Credits { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public double Average { get; set; }

        public Student(string name, int age, int courses, int credits, string email, string id, double average)
        {
            Name = name;
            Age = age;
            Courses = courses;
            Credits = credits;
            Email = email;
            Id = id;
            Average = average;
        }

        public override string? ToString()
        {
            return $"[{this.Id}][{this.Name}][{this.Age}][{this.Email}][{this.Courses}][{this.Credits}][{this.Average}]";
        }
    }
}
