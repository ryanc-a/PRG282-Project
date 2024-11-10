using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRG281_Project
{
    public partial class DataForm : Form
    {
        public DataForm()
        {
            InitializeComponent();
            //students = handler.GetStudents();
        }
        File_Handler handler = new File_Handler();
        List<Student> students = new List<Student>();

        BindingSource source = new BindingSource();

        private void AddStudent(List<Student> students, Student newStudent)
        {
            Console.WriteLine("Adding new student....");
            students.Add(newStudent);
            Console.WriteLine("Saving new student to storage...");
            handler.SaveStudents(students);
            DataGrid.DataSource = students;
        }
        private void UpdateStudent(List<Student> students, Student oldStudent,Student updatedStudent)
        {
            /*
            List<Student> newStudents =new List<Student>();
            foreach (Student student in students)
            {
                if (student != oldStudent)
                {
                    newStudents.Add(student); 
                }
                else 
                { 
                    newStudents.Add(updatedStudent);
                    Console.WriteLine("Students have been updated");
                }
            }
            if(students == newStudents)
            {
                Console.WriteLine("No matching student found");
            }
            students = newStudents;
            */
            //better method
            int i =students.LastIndexOf(oldStudent);
            if (i != -1) { 
            students[i] = updatedStudent;
            handler.SaveStudents(students);
            }
            else
            {
            Console.WriteLine("No matching student found");
            }
            handler.SaveStudents(students);
            DataGrid.DataSource = students;
        }

        private void DeleteStudent(List<Student> students, Student deleteStudent)
        {
            /*
            List<Student> newStudents = new List<Student>();
            foreach (Student student in students)
            {
                if (student != deleteStudent)
                {
                    newStudents.Add(student);
                }
                else
                {
                    Console.WriteLine("Student has been deleted successfully");
                }
            }
            students = newStudents;
            */
            students.Remove(deleteStudent);
            handler.SaveStudents(students);
            DataGrid.DataSource = students;
        }

        private void GenerateSummary(List<Student> students)
        {
            string[] summary = new string[2];
            int totalStudents;
            double totalAge = 0;
            double averageAge;
            //getting student count
            totalStudents = students.Count();
            //getting the thotal age of students
            foreach (Student student in students)
            {
                totalAge += student.Student_Age;
            }
            //getting the average age of students rouded to 2 decimal places
            averageAge =Math.Round((totalAge / totalStudents),2);
            summary[0] = totalStudents.ToString();
            summary[1] = averageAge.ToString();
            //saves updated students list to text file
            handler.SaveSummary(summary);
            //Display summary
            TotalStudents.Text = summary[0];
            AverageAge.Text = summary[1];
        }
        private void Add_Student_Click(object sender, EventArgs e)
        {
            int id;
            string name;
            int age;
            string course;
            //getting values from form
            id = int.Parse(ID.Text);
            name = sName.Text;
            age = int.Parse(Age.Text);
            course = Course.Text;

            AddStudent(students, new Student(id, name, age, course));
            //Update display
            DataGrid.DataSource = students;
        }

        private void View_Student_Click(object sender, EventArgs e)
        {
            students.Clear();
            students = handler.GetStudents();
            source.DataSource = students;
            DataGrid.DataSource = source;

        }

        private void Update_Student_Click(object sender, EventArgs e)
        {
            int searchID = 0;
            string name;
            int age;
            string course;
            Student oldStudent = null;
            //getting values from form
            searchID = int.Parse(UpdateID.Text);
            name = UpdateName.Text;
            age = int.Parse(UpdateAge.Text);
            course = UpdateCourse.Text;
            foreach (Student student in students)
            {
                if (student.Student_ID == searchID)
                {
                    oldStudent = student;
                    break;
                }
            }
            if(oldStudent == null) 
            {
                Console.WriteLine("Invalid ID");
            }
            UpdateStudent(students, oldStudent, new Student(searchID, name, age, course));
        }

        private void Generate_Summary_Click(object sender, EventArgs e)
        {
            GenerateSummary(students);
        }

        private void Delete_Student_Click(object sender, EventArgs e)
        {
            int searchID = 0;
            Student deleteStudent = null;
            searchID = int.Parse(DeleteID.Text);

            foreach (Student student in students)
            {
                if (student.Student_ID == searchID)
                {
                    deleteStudent = student;
                    break;
                }
            }
            if (deleteStudent == null)
            {
                Console.WriteLine("Invalid ID");
            }

            DeleteStudent(students,deleteStudent);
        }
    }
}
