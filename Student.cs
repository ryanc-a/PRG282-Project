using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG281_Project
{
    internal class Student
    {
        private int student_ID;
        private string student_Name;
        private int student_Age;
        private string course;

        public Student(int student_ID, string student_Name, int student_Age, string course)
        {
            this.Student_ID = student_ID;
            this.Student_Name = student_Name;
            this.Student_Age = student_Age;
            this.Course = course;
        }

        public int Student_ID { get => student_ID; set => student_ID = value; }
        public string Student_Name { get => student_Name; set => student_Name = value; }
        public int Student_Age { get => student_Age; set => student_Age = value; }
        public string Course { get => course; set => course = value; }

        public override string ToString()
        {
            return Student_ID+"," + Student_Name + "," + Student_Age + "," + Course;
        }
    }
}
