using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRG281_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            List<Student> students = new List<Student>();
            students.Add(new Student(1, "Jamie", 19, "IT"));
            students.Add(new Student(1, "Katie", 19, "IT"));
            File_Handler fh = new File_Handler();
            fh.SaveStudents(students);
            List<Student> display_students = fh.GetStudents();
            foreach (var item in display_students)
            {
                Console.WriteLine(item.ToString());
            }
            */
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DataForm());
            Console.ReadKey();
        }
    }
}
