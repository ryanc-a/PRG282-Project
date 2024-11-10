using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG281_Project
{
    internal class File_Handler
    {
        public File_Handler() { }
        public string path =@"Students.txt";
        public string path2 = @"Summary.txt";

        public List<Student> students = new List<Student>();
        public List<Student> Rstudents = new List<Student>();
        
        public List<Student> GetStudents()
        {
            List<string> studentText = new List<string>();
            Student student;
            string[] arr;
            if (File.Exists(path))
            {
                using(TextReader reader = File.OpenText(path))
                {
                    string line;
                    while ((line = reader.ReadLine() )!= null) {
                        arr = line.Split(',');
                        students.Add(new Student(int.Parse(arr[0]), arr[1],int.Parse(arr[2]), arr[3]));
                }
                }
                Console.WriteLine("File data has been loaded successfully");

            }
            else
            {
                Console.WriteLine("File does not exist");
            }
            return students;
        }
        public void SaveStudents(List<Student> students)
        {
            List<string> studentText = new List<string>();
            foreach (Student student in students)
            {
                studentText.Add(student.ToString());
            }
            using (StreamWriter writer = File.CreateText(path))
            {
                foreach (string line in studentText)
                {
                    writer.WriteLine(line);
                }
                Console.WriteLine("File has been updated");  
            }
            
        }

        public void SaveSummary(string[] text)
        {
            File.WriteAllLines(path2, text);
            Console.WriteLine("File has been updated");

        }

        public string[] LoadSummary()
        {
            string[] text;
            text = File.ReadAllLines(path2);
            Console.WriteLine("Summary data loaded from file");
            return text;
        }
    }
}
